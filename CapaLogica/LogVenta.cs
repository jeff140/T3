using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaLogica
{
    public class LogVenta
    {
        // Instancia para acceder a la capa de datos
        private DatVenta objDatVenta = new DatVenta();
        public bool EliminarVenta(int idVenta)
        {
            try
            {
                // Validación básica: Asegurarse de que el ID es válido
                if (idVenta <= 0)
                {
                    throw new ArgumentException("ID de venta inválido para eliminar.");
                }

                // Llamada a la Capa de Datos
                return objDatVenta.EliminarVenta(idVenta);
            }
            catch (Exception ex)
            {
                // Aquí podrías loggear el error
                throw new Exception("Error en la Capa Lógica al intentar eliminar la venta.", ex);
            }
        }
        public DataTable MostrarVentas()
        {
            DatVenta datVenta = new DatVenta();
            return datVenta.ListarVentas();
        }

        // Método de Lógica para la Inserción
        public bool InsertarNuevaVenta(EntVenta venta)
        {
            // 1. **Regla de Negocio / Validación:** // Asegurarse de que la cantidad sea positiva antes de intentar guardar
            if (venta.Cantidad <= 0)
            {
                // Podrías lanzar una excepción o devolver false
                throw new ArgumentException("La cantidad de productos debe ser mayor a cero.");
            }

            // 2. **Cálculo de Propiedades (Opcional):**
            // Calcular el total antes de enviar a la BD
            venta.Total = venta.Cantidad * venta.PrecioUnitario;

            // 3. Llamada a la Capa de Datos
            try
            {
                int idVenta = objDatVenta.InsertarVenta(venta); // retorna el idVenta (int)
                return idVenta > 0; // lo convertimos en bool sin romper el resto del código
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Capa Lógica al insertar la venta.", ex);
            }

        }
        public bool RegistrarVenta(EntVenta venta, List<EntidadDetalleVenta> detalles)
        {
            try
            {
                DatVenta datVenta = new DatVenta();
                int idVenta = datVenta.InsertarVenta(venta); // Inserta cabecera

                foreach (var det in detalles)
                {
                    det.IdVenta = idVenta;                   // Asocia detalle con venta
                    datVenta.InsertarDetalleVenta(det);     // Inserta detalle
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Capa Lógica al insertar la venta.", ex);
            }
        }
        public decimal ObtenerDescuento(string descripcion)
        {
            DatVenta datos = new DatVenta();
            return datos.ObtenerDescuentoDesdeBD(descripcion);
        }
        public DataTable ObtenerVentas()
        {
            DatVenta datVenta = new DatVenta();
            return datVenta.ListarVentas();
        }
    }
}
 