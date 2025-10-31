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
        public bool InsertarVenta(EntVenta venta)
        {
            // La línea clave para usar el Singleton:
            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarVenta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // 1. Definición y asignación de Parámetros SQL
                cmd.Parameters.AddWithValue("@ProductoNombre", venta.ProductoNombre);
                cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", venta.PrecioUnitario);
                cmd.Parameters.AddWithValue("@TipoPago", venta.TipoPago);
                cmd.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                cmd.Parameters.AddWithValue("@Total", venta.Total);

                // 2. Ejecución del Comando
                try
                {
                    cn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
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
    }
}