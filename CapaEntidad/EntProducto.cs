using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entProducto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string unidadMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public int stock { get; set; }
        public Boolean estProducto { get; set; }
        public int idCategoria { get; set; }

        public string nombreCategoria { get; set; }
    }
}