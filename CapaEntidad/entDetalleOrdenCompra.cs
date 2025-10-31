using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entDetalleOrdenCompra
    {
        public int idDetalleOrden { get; set; }
        public int idOrdenCompra { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }
    }
}