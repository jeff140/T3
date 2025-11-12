using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;

namespace CapaDatos
{
    public class DatVenta
    {
        public bool EliminarVenta(int idVenta)
        {
            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                // Asumimos que tienes un Stored Procedure llamado SP_EliminarVenta
                SqlCommand cmd = new SqlCommand("SP_EliminarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetro para identificar la fila a eliminar
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                try
                {
                    cn.Open();
                    // ExecuteNonQuery devuelve 1 si se eliminó una fila, o 0 si no se encontró
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
                catch (SqlException ex)
                {
                    // Lanza una excepción si hay un error de base de datos
                    throw new Exception("Error al ejecutar la eliminación en la BD: " + ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
        }
        public DataTable ListarVentas()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                string query = @"
                SELECT 
                V.idVenta AS [ID Venta],
                V.fechaVenta AS [Fecha],
                V.subtotal AS [Subtotal],
                V.Cantidad AS [Cantidad],
                V.descuentoTotal AS [Descuento],
                V.total AS [Total],
                V.metodoPago AS [Método de Pago]
                FROM Venta V
                ORDER BY V.idVenta DESC;";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Venta", cn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public int InsertarVenta(EntVenta venta)
        {
            // La línea clave para usar el Singleton:
            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // 1. Definición y asignación de Parámetros SQL
                cmd.Parameters.AddWithValue("@subtotal", venta.Subtotal);
                cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                cmd.Parameters.AddWithValue("@descuentoTotal", venta.DescuentoTotal);
                cmd.Parameters.AddWithValue("@total", venta.Total);
                cmd.Parameters.AddWithValue("@metodoPago", venta.MetodoPago ?? (object)DBNull.Value);

                // 2. Ejecución del Comando
                try
                {
                    cn.Open();
                    int idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                    return idVenta;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al ejecutar la inserción en la BD: " + ex.Message, ex);
                }
                finally
                {
                    cn.Close();
                }
            }
        }
        public bool InsertarDetalleVenta(EntidadDetalleVenta detalle)
        {
            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertarDetalleVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idVenta", detalle.IdVenta);
                cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                cmd.Parameters.AddWithValue("@idDescuento", detalle.IdDescuento ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                cmd.Parameters.AddWithValue("@total", detalle.Total);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public decimal ObtenerDescuentoDesdeBD(string descripcion)
        {
            decimal porcentaje = 0;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                cn.Open();
                string query = "SELECT porcentaje FROM Descuento WHERE descripcion = @descripcion";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        porcentaje = Convert.ToDecimal(resultado);
                    }
                }
            }

            return porcentaje;
        }

        private string ObtenerDescuentoPorMonto(decimal total)
        {
            string descripcion = "";

            string query = @"
        SELECT TOP 1 Descripcion 
        FROM Descuento
        WHERE 
            (porcentaje = 10.00 AND @total >= 2500 AND @total < 5000) OR
            (porcentaje = 15.00 AND @total >= 5000 AND @total < 10000) OR
            (porcentaje = 20.00 AND @total >= 10000)
        ORDER BY porcentaje ASC;";

            using (SqlConnection conn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@total", total);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    descripcion = reader["Descripcion"].ToString();
                }
            }

            return descripcion;
        }
    }
}