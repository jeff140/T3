using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class datProforma
    {
        #region singleton
        private static readonly datProforma _instancia = new datProforma();
        public static datProforma Instancia
        {
            get { return datProforma._instancia; }
        }
        #endregion singleton

        #region metodos

        /// Inserta proforma (cabecera + items). Devuelve id generado y out numeroProforma.
        public int InsertarProforma(entProforma pro, out string numeroProforma)
        {
            SqlCommand cmd = null;
            int idGenerado = 0;
            numeroProforma = null;
            SqlConnection cn = null; // Declarar fuera del try para usar en finally

            try
            {
                cn = Conexion.Instancia.Conectar(); // Obtener la conexión
                cmd = new SqlCommand("sp_InsertarProformaCabecera", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros de Cabecera
                cmd.Parameters.AddWithValue("@IdCliente", pro.IdCliente);
                cmd.Parameters.AddWithValue("@FechaEmision", (object)pro.FechaEmision ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaVencimiento", (object)pro.FechaVencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Subtotal", pro.Subtotal);
                cmd.Parameters.AddWithValue("@Descuento", pro.Descuento);
                cmd.Parameters.AddWithValue("@IdUsuario", (object)pro.IdUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Comentarios", (object)pro.Comentarios ?? DBNull.Value);

                cn.Open();

                // El SP devuelve IdProforma y NumeroProforma en un resultset
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        idGenerado = Convert.ToInt32(dr["IdProforma"]);
                        numeroProforma = dr["NumeroProforma"].ToString();
                    }
                }

                if (idGenerado <= 0)
                {
                    throw new Exception("No se obtuvo IdProforma al insertar la cabecera.");
                }

                // --- OPTIMIZACIÓN: Bucle de Inserción de Items ---
                foreach (var it in pro.Items)
                {
                    // Reutiliza la conexión 'cn' ya abierta
                    using (SqlCommand cmdIt = new SqlCommand("sp_InsertarProformaItem", cn))
                    {
                        cmdIt.CommandType = CommandType.StoredProcedure;
                        cmdIt.Parameters.AddWithValue("@IdProforma", idGenerado);
                        cmdIt.Parameters.AddWithValue("@IdProducto", it.IdProducto);

                        cmdIt.Parameters.AddWithValue("@Cantidad", it.Cantidad);

                        cmdIt.Parameters.AddWithValue("@PrecioUnitario", it.PrecioUnitario);

                        cmdIt.ExecuteNonQuery(); // Ejecuta el SP de item
                    }
                }

                // El subtotal ya fue calculado en la lógica y enviado al sp_InsertarProformaCabecera.
            }
            catch (Exception e)
            {
                // Relanza la excepción para que la capa de lógica (y UI) la maneje
                throw e;
            }
            finally
            {
                // Asegura que la conexión siempre se cierre
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }

            return idGenerado; // Devuelve el ID de la cabecera
        }

        /// Obtener proforma completa (cabecera + items) por su número.
        public entProforma GetProformaByNumero(string numero)
        {
            SqlCommand cmd = null;
            entProforma ent = null;
            SqlConnection cn = null;

            try
            {
                cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("sp_GetProformaPorNumero", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NumeroProforma", numero);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    // 1. Leer la Cabecera (Primer resultset)
                    if (dr.Read())
                    {
                        ent = new entProforma
                        {
                            IdProforma = Convert.ToInt32(dr["IdProforma"]),
                            NumeroProforma = dr["NumeroProforma"].ToString(),
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            FechaEmision = Convert.ToDateTime(dr["FechaEmision"]),
                            FechaVencimiento = dr["FechaVencimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaVencimiento"]),
                            Subtotal = dr["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Subtotal"]),
                            Descuento = dr["Descuento"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Descuento"]),
                            // 'Total' es calculado en la entidad, pero lo leemos de la BD (PERSISTED)
                            // Total = dr["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Total"]), 
                            Estado = dr["Estado"] == DBNull.Value ? null : dr["Estado"].ToString(),
                            IdUsuario = dr["IdUsuario"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["IdUsuario"]),
                            Comentarios = dr["Comentarios"] == DBNull.Value ? null : dr["Comentarios"].ToString()
                        };
                    }

                    // 2. Leer el Detalle (Segundo resultset)
                    if (ent != null && dr.NextResult())
                    {
                        while (dr.Read())
                        {
                            ent.Items.Add(new entProformaItem
                            {
                                IdItem = dr["IdItem"] == DBNull.Value ? 0 : Convert.ToInt32(dr["IdItem"]),
                                IdProforma = ent.IdProforma,
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                NombreProducto = dr["nombreProducto"] == DBNull.Value ? null : dr["nombreProducto"].ToString(),

                                // OPCIÓN A: Lee la Cantidad como INT
                                Cantidad = dr["Cantidad"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cantidad"]),

                                PrecioUnitario = dr["PrecioUnitario"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PrecioUnitario"]),
                                Subtotal = dr["Subtotal"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Subtotal"])
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cn != null && cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                }
            }

            return ent;
        }

        #endregion metodos
    }
}