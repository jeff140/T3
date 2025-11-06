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
    public class datCategoria
    {
        #region singleton
        private static readonly datCategoria _instancia = new datCategoria();
        public static datCategoria Instancia
        {
            get { return datCategoria._instancia; }
        }
        #endregion singleton

        #region metodos
        // Listar Categorias
        public List<entCategoria> ListarCategoria()
        {
            SqlCommand cmd = null;
            List<entCategoria> lista = new List<entCategoria>();
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCategoria cat = new entCategoria();
                    cat.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    cat.Descripcion = Convert.ToString(dr["Descripcion"]);
                    cat.estCategoria = Convert.ToBoolean(dr["estCategoria"]);
                    lista.Add(cat);
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

        // Insertar Categoria
        public Boolean InsertarCategoria(entCategoria cat)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", cat.Descripcion);
                cmd.Parameters.AddWithValue("@estCategoria", cat.estCategoria);
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

        // Editar Categoria
        public Boolean EditarCategoria(entCategoria cat)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", cat.idCategoria);
                cmd.Parameters.AddWithValue("@Descripcion", cat.Descripcion);
                cmd.Parameters.AddWithValue("@estCategoria", cat.estCategoria);
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

        // Deshabilitar Categoria
        public Boolean DeshabilitarCategoria(entCategoria cat)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", cat.idCategoria);
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
            finally { cmd.Connection.Close(); }
            return delete;
        }
        #endregion metodos
    }
}