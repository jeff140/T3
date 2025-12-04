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
    public class datProducto
    {
        #region singleton
        private static readonly datProducto _instancia = new datProducto();
        public static datProducto Instancia
        {
            get { return datProducto._instancia; }
        }
        #endregion singleton

        #region metodos
        // Listar Productos
        public List<entProducto> ListarProducto()
        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto prod = new entProducto();
                    prod.idProducto = Convert.ToInt32(dr["idProducto"]);
                    prod.nombreProducto = Convert.ToString(dr["nombreProducto"]);
                    prod.unidadMedida = Convert.ToString(dr["unidadMedida"]);
                    prod.precioUnitario = Convert.ToDecimal(dr["precioUnitario"]);
                    prod.stock = Convert.ToInt32(dr["stock"]);
                    prod.estProducto = Convert.ToBoolean(dr["estProducto"]);
                    prod.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    //prod.nombreCategoria = Convert.ToString(dr["nombreCategoria"]);
                    lista.Add(prod);
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

        // Insertar Producto (✅ CÓDIGO CORRECTO - ENVÍA 6 PARÁMETROS)
        public Boolean InsertarProducto(entProducto prod)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_InsertarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Envía 6 parámetros:
                cmd.Parameters.AddWithValue("@nombreProducto", prod.nombreProducto);
                cmd.Parameters.AddWithValue("@unidadMedida", prod.unidadMedida);
                cmd.Parameters.AddWithValue("@precioUnitario", prod.precioUnitario);
                cmd.Parameters.AddWithValue("@stock", prod.stock);
                cmd.Parameters.AddWithValue("@estProducto", prod.estProducto);
                cmd.Parameters.AddWithValue("@idCategoria", prod.idCategoria);
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

        // Editar Producto (✅ CORREGIDO: Se añadió @idCategoria)
        public Boolean EditarProducto(entProducto prod)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_ActualizarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // 🚨 CAMBIO CRÍTICO: Aseguramos que @idProducto se envía primero.
                cmd.Parameters.AddWithValue("@idProducto", prod.idProducto);

                cmd.Parameters.AddWithValue("@nombreProducto", prod.nombreProducto);
                cmd.Parameters.AddWithValue("@unidadMedida", prod.unidadMedida);
                cmd.Parameters.AddWithValue("@precioUnitario", prod.precioUnitario);
                cmd.Parameters.AddWithValue("@stock", prod.stock);
                cmd.Parameters.AddWithValue("@estProducto", prod.estProducto);
                cmd.Parameters.AddWithValue("@idCategoria", prod.idCategoria);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    edita = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return edita;
        }

        // Deshabilitar Producto
        public Boolean DeshabilitarProducto(entProducto prod)
        {
        SqlCommand cmd = null;
    Boolean delete = false;
    try
    {
        SqlConnection cn = Conexion.Instancia.Conectar();
        // Usamos el Stored Procedure que acabamos de crear
        cmd = new SqlCommand("sp_EliminarProducto", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        
        // Solo necesitamos enviar el ID para saber qué producto deshabilitar
        cmd.Parameters.AddWithValue("@idProducto", prod.idProducto);
        
        cn.Open();
        int i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            delete = true;
        }
    }
    catch (Exception e)
    {
        throw e;
    }
    finally 
    { 
        if (cmd.Connection != null) 
        {
            cmd.Connection.Close(); 
        }
    }
    return delete;
        }
        public entProducto ObtenerProductoPorId(int idProducto)
        {
            entProducto producto = null;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SELECT idProducto, nombreProducto, precioUnitario FROM Producto WHERE idProducto = @idProducto", cn);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    producto = new entProducto
                    {
                        idProducto = Convert.ToInt32(dr["idProducto"]),
                        nombreProducto = dr["nombreProducto"].ToString(),
                        precioUnitario = Convert.ToDecimal(dr["precioUnitario"])
                    };
                }
            }

            return producto;
        }
        #endregion metodos

        #region metodos
        // ... otros metodos ...

        // Nuevo método para buscar productos
        public List<entProducto> BuscarProducto(string textoBusqueda)
        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("SELECT * FROM Producto WHERE nombreProducto LIKE @texto OR idProducto LIKE @texto", cn);
                cmd.CommandType = CommandType.Text;
                // Se usa '%' para que busque el texto en cualquier parte del nombre.
                cmd.Parameters.AddWithValue("@texto", "%" + textoBusqueda + "%");

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto prod = new entProducto();
                    prod.idProducto = Convert.ToInt32(dr["idProducto"]);
                    prod.nombreProducto = Convert.ToString(dr["nombreProducto"]);
                    prod.unidadMedida = Convert.ToString(dr["unidadMedida"]);
                    prod.precioUnitario = Convert.ToDecimal(dr["precioUnitario"]);
                    prod.stock = Convert.ToInt32(dr["stock"]);
                    prod.estProducto = Convert.ToBoolean(dr["estProducto"]);
                    prod.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    lista.Add(prod);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                {
                    cmd.Connection.Close();
                }
            }
            return lista;
        }
        #endregion metodos
    }

}
