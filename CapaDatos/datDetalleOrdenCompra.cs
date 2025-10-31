using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datDetalleOrdenCompra
    {
        #region singleton
        private static readonly datDetalleOrdenCompra _instancia = new datDetalleOrdenCompra();
        public static datDetalleOrdenCompra Instancia
        {
            get { return datDetalleOrdenCompra._instancia; }
        }
        #endregion singleton

        #region metodos
        // Listar Detalle de una Orden
        public List<entDetalleOrdenCompra> ListarDetalleOrdenCompra(int idOrdenCompra)
        {
            SqlCommand cmd = null;
            List<entDetalleOrdenCompra> lista = new List<entDetalleOrdenCompra>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarDetalleOrdenCompra", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOrdenCompra", idOrdenCompra);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entDetalleOrdenCompra det = new entDetalleOrdenCompra();
                    det.idDetalleOrden = Convert.ToInt32(dr["idDetalleOrden"]);
                    det.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"]);
                    det.idProducto = Convert.ToInt32(dr["idProducto"]);
                    det.cantidad = Convert.ToInt32(dr["cantidad"]);
                    det.precioUnitario = Convert.ToDecimal(dr["precioUnitario"]);
                    det.subtotal = Convert.ToDecimal(dr["subtotal"]);
                    lista.Add(det);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }

        // Insertar Detalle
        public Boolean InsertarDetalleOrdenCompra(entDetalleOrdenCompra det)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarDetalleOrdenCompra", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOrdenCompra", det.idOrdenCompra);
                cmd.Parameters.AddWithValue("@idProducto", det.idProducto);
                cmd.Parameters.AddWithValue("@cantidad", det.cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", det.precioUnitario);
                cmd.Parameters.AddWithValue("@subtotal", det.subtotal);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    inserta = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return inserta;
        }

        // Eliminar Detalle
        public Boolean EliminarDetalleOrdenCompra(int idDetalleOrden)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarDetalleOrdenCompra", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDetalleOrden", idDetalleOrden);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    elimina = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return elimina;
        }

        // Eliminar todos los detalles de una orden
        public Boolean EliminarDetallesPorOrden(int idOrdenCompra)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarDetallesPorOrden", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idOrdenCompra", idOrdenCompra);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                elimina = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return elimina;
        }
        #endregion metodos
    }
}