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
    public class datDetalleGuiaRemision
    {
        #region Singleton
        private static readonly datDetalleGuiaRemision _instancia = new datDetalleGuiaRemision();
        public static datDetalleGuiaRemision Instancia
        {
            get { return datDetalleGuiaRemision._instancia; }
        }
        #endregion

        #region Métodos

        // ===== LISTAR DETALLES POR GUÍA DE REMISIÓN =====
        public List<entDetalleGuiaRemision> ListarDetallePorGuia(int idGuiaRemision)
        {
            List<entDetalleGuiaRemision> lista = new List<entDetalleGuiaRemision>();

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spListarDetalleGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGuiaRemision", idGuiaRemision);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        entDetalleGuiaRemision detalle = new entDetalleGuiaRemision
                        {
                            idDetalleGuia = Convert.ToInt32(dr["idDetalleGuia"]),
                            idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"]),
                            idProducto = Convert.ToInt32(dr["idProducto"]),
                            cantidad = Convert.ToInt32(dr["cantidad"]),
                            descripcion = dr["descripcion"] == DBNull.Value ? "" : dr["descripcion"].ToString()
                        };
                        lista.Add(detalle);
                    }
                }
            }

            return lista;
        }

        // ===== INSERTAR DETALLE GUÍA DE REMISIÓN =====
        public bool InsertarDetalleGuia(entDetalleGuiaRemision detalle)
        {
            bool inserta = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spInsertarDetalleGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@idGuiaRemision", detalle.idGuiaRemision);
                cmd.Parameters.AddWithValue("@idProducto", detalle.idProducto);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@descripcion", (object)detalle.descripcion ?? DBNull.Value);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = (i > 0);
            }

            return inserta;
        }

        // ===== EDITAR DETALLE GUÍA DE REMISIÓN =====
        public bool EditarDetalleGuia(entDetalleGuiaRemision detalle)
        {
            bool edita = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spEditarDetalleGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idDetalleGuia", detalle.idDetalleGuia);
                cmd.Parameters.AddWithValue("@idProducto", detalle.idProducto);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@descripcion", (object)detalle.descripcion ?? DBNull.Value);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                edita = (i > 0);
            }

            return edita;
        }

        // ===== ELIMINAR DETALLES POR GUÍA DE REMISIÓN =====
        public bool EliminarDetallesPorGuia(int idGuiaRemision)
        {
            bool elimina = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spEliminarDetallesPorGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGuiaRemision", idGuiaRemision);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                elimina = (i >= 0); // Puede ser 0 si no hay detalles
            }

            return elimina;
        }

        #endregion
    }
}