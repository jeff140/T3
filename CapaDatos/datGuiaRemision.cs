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
    public class datGuiaRemision
    {
        #region Singleton
        private static readonly datGuiaRemision _instancia = new datGuiaRemision();
        public static datGuiaRemision Instancia
        {
            get { return datGuiaRemision._instancia; }
        }
        #endregion

        #region Métodos

        // ===== LISTAR GUÍAS DE REMISIÓN =====
        public List<entGuiaRemision> ListarGuiaRemision()
        {
            List<entGuiaRemision> lista = new List<entGuiaRemision>();

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spListarGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        entGuiaRemision guia = new entGuiaRemision
                        {
                            idGuiaRemision = Convert.ToInt32(dr["idGuiaRemision"]),
                            numeroGuia = dr["numeroGuia"].ToString(),
                            fechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                            fechaTransporte = Convert.ToDateTime(dr["fechaTransporte"]),
                            idVenta = Convert.ToInt32(dr["idVenta"]),
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            numeroComprobante = dr["numeroComprobante"] == DBNull.Value ? "" : dr["numeroComprobante"].ToString(),
                            direccionPartida = dr["direccionPartida"] == DBNull.Value ? "" : dr["direccionPartida"].ToString(),
                            direccionLlegada = dr["direccionLlegada"].ToString(),
                            motivoTraslado = dr["motivoTraslado"].ToString(),
                            transporte = dr["transporte"] == DBNull.Value ? "" : dr["transporte"].ToString(),
                            placaVehiculo = dr["placaVehiculo"] == DBNull.Value ? "" : dr["placaVehiculo"].ToString(),
                            estado = Convert.ToBoolean(dr["estado"])
                        };
                        lista.Add(guia);
                    }
                }
            }

            return lista;
        }

        // ===== INSERTAR GUÍA DE REMISIÓN =====
        public int InsertarGuiaRemision(entGuiaRemision guia)
        {
            int idGenerado = 0;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spInsertarGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@numeroGuia", guia.numeroGuia);
                cmd.Parameters.AddWithValue("@fechaEmision", guia.fechaEmision);
                cmd.Parameters.AddWithValue("@fechaTransporte", guia.fechaTransporte);
                cmd.Parameters.AddWithValue("@idVenta", guia.idVenta);
                cmd.Parameters.AddWithValue("@idCliente", guia.idCliente);
                cmd.Parameters.AddWithValue("@numeroComprobante", (object)guia.numeroComprobante ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@direccionPartida", (object)guia.direccionPartida ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@direccionLlegada", guia.direccionLlegada);
                cmd.Parameters.AddWithValue("@motivoTraslado", guia.motivoTraslado);
                cmd.Parameters.AddWithValue("@transporte", (object)guia.transporte ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@placaVehiculo", (object)guia.placaVehiculo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", guia.estado);

                cn.Open();

                // El SP retorna el ID generado
                object resultado = cmd.ExecuteScalar();
                if (resultado != null)
                {
                    idGenerado = Convert.ToInt32(resultado);
                }
            }

            return idGenerado;
        }

        // ===== EDITAR GUÍA DE REMISIÓN =====
        public bool EditarGuiaRemision(entGuiaRemision guia)
        {
            bool edita = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spEditarGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idGuiaRemision", guia.idGuiaRemision);
                cmd.Parameters.AddWithValue("@numeroGuia", guia.numeroGuia);
                cmd.Parameters.AddWithValue("@fechaEmision", guia.fechaEmision);
                cmd.Parameters.AddWithValue("@fechaTransporte", guia.fechaTransporte);
                cmd.Parameters.AddWithValue("@idVenta", guia.idVenta);
                cmd.Parameters.AddWithValue("@idCliente", guia.idCliente);
                cmd.Parameters.AddWithValue("@numeroComprobante", (object)guia.numeroComprobante ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@direccionPartida", (object)guia.direccionPartida ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@direccionLlegada", guia.direccionLlegada);
                cmd.Parameters.AddWithValue("@motivoTraslado", guia.motivoTraslado);
                cmd.Parameters.AddWithValue("@transporte", (object)guia.transporte ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@placaVehiculo", (object)guia.placaVehiculo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@estado", guia.estado);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                edita = (i > 0);
            }

            return edita;
        }

        // ===== DESHABILITAR GUÍA DE REMISIÓN =====
        public bool DeshabilitarGuiaRemision(int idGuiaRemision)
        {
            bool deshabilita = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spDeshabilitarGuiaRemision", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idGuiaRemision", idGuiaRemision);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                deshabilita = (i > 0);
            }

            return deshabilita;
        }

        #endregion
    }
}