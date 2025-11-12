using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EntVenta
    {
        // Propiedades de la Venta (ajusta los tipos de datos según tu BD)
        public int IdVenta { get; set; }        // ID autoincrementable
        public string ProductoNombre { get; set; } // Nombre del producto (ejemplo simple)
        public int Cantidad { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; } // Propiedad calculada o guardada
    }
}
