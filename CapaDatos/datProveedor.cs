using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad; 

namespace CapaDatos
{
    public class datProveedor
    {
        #region Singleton
        // Patrón Singleton
        private static readonly datProveedor _instancia = new datProveedor();
        public static datProveedor Instancia
        {
            get { return datProveedor._instancia; }
        }
        #endregion Singleton

        #region Metodos

        // --- MÉTODO PARA LISTAR ---
        public List<entProveedor> ListarProveedores()
        {
            SqlCommand cmd = null;
            List<entProveedor> lista = new List<entProveedor>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar(); 
                cmd = new SqlCommand("sp_ListarProveedores", cn); 
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    entProveedor prov = new entProveedor();
                    prov.idProveedor = Convert.ToInt32(dr["idProveedor"]);
                    prov.razonSocial = dr["razonSocial"].ToString();
                    prov.ruc = dr["ruc"].ToString();
                    prov.telefono = dr["telefono"].ToString();
                    prov.direccion = dr["direccion"].ToString();
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
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return lista;
        }

        // --- MÉTODO PARA INSERTAR ---
        public void InsertarProveedor(entProveedor prov)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_InsertarProveedor", cn); 
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los parámetros
                cmd.Parameters.AddWithValue("@razonSocial", prov.razonSocial);
                cmd.Parameters.AddWithValue("@ruc", prov.ruc);
                cmd.Parameters.AddWithValue("@telefono", prov.telefono);
                cmd.Parameters.AddWithValue("@direccion", prov.direccion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        // --- MÉTODO PARA ACTUALIZAR ---
        public void ActualizarProveedor(entProveedor prov)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_ActualizarProveedor", cn); // Llama al SP
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos los parámetros
                cmd.Parameters.AddWithValue("@idProveedor", prov.idProveedor);
                cmd.Parameters.AddWithValue("@razonSocial", prov.razonSocial);
                cmd.Parameters.AddWithValue("@telefono", prov.telefono);
                cmd.Parameters.AddWithValue("@direccion", prov.direccion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        // --- MÉTODO PARA ELIMINAR (LÓGICO) ---
        public void EliminarProveedor(int idProveedor)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_EliminarProveedor", cn); // Llama al SP
                cmd.CommandType = CommandType.StoredProcedure;

                // Pasamos el ID del proveedor
                cmd.Parameters.AddWithValue("@idProveedor", idProveedor);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        #endregion Metodos
    }
}