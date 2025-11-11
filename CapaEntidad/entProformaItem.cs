using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entProformaItem
    {
        public int IdItem { get; set; }
        public int IdProforma { get; set; }
        public int IdProducto { get; set; }

        public int Cantidad { get; set; } = 1;

        public decimal PrecioUnitario { get; set; } = 0m;
        public decimal Subtotal { get; set; } = 0m;

        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
    }
}
