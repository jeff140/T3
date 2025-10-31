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
    public class datProveedor
    {
        #region Singleton
        private static readonly datProveedor _instancia = new datProveedor();
        public static datProveedor Instancia
        {
            get { return datProveedor._instancia; }
        }
        #endregion Singleton

        #region Métodos

        // ✅ Listar Proveedores
        public List<entProveedor> ListarProveedor()
        {
            SqlCommand cmd = null;
            List<entProveedor> lista = new List<entProveedor>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarProveedor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProveedor prov = new entProveedor();
                    prov.idProveedor = Convert.ToInt32(dr["idProveedor"]);
                    prov.razonSocial = Convert.ToString(dr["razonSocial"]);
                    prov.ruc = Convert.ToString(dr["ruc"]);
                    prov.telefono = Convert.ToString(dr["telefono"]);
                    prov.direccion = Convert.ToString(dr["direccion"]);
                    prov.estProveedor = Convert.ToBoolean(dr["estProveedor"]);
                    lista.Add(prov);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return lista;
        }

        // ✅ Insertar Proveedor
        public bool InsertarProveedor(entProveedor prov)
        {
            SqlCommand cmd = null;
            bool inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarProveedor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@razonSocial", prov.razonSocial);
                cmd.Parameters.AddWithValue("@ruc", prov.ruc);
                cmd.Parameters.AddWithValue("@telefono", prov.telefono);
                cmd.Parameters.AddWithValue("@direccion", prov.direccion);
                cmd.Parameters.AddWithValue("@estProveedor", prov.estProveedor);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    inserta = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return inserta;
        }

        // ✅ Editar Proveedor
        public bool EditarProveedor(entProveedor prov)
        {
            SqlCommand cmd = null;
            bool edita = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarProveedor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProveedor", prov.idProveedor);
                cmd.Parameters.AddWithValue("@razonSocial", prov.razonSocial);
                cmd.Parameters.AddWithValue("@ruc", prov.ruc);
                cmd.Parameters.AddWithValue("@telefono", prov.telefono);
                cmd.Parameters.AddWithValue("@direccion", prov.direccion);
                cmd.Parameters.AddWithValue("@estProveedor", prov.estProveedor);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    edita = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return edita;
        }

        // ✅ Deshabilitar Proveedor
        public bool DeshabilitarProveedor(entProveedor prov)
        {
            SqlCommand cmd = null;
            bool desactiva = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarProveedor", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idProveedor", prov.idProveedor);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    desactiva = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return desactiva;
        }

        #endregion Métodos
    }
}
