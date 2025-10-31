using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entOrden_Compra
    {
        public int idOrdenCompra { get; set; }
        public DateTime fechaEmision { get; set; }
        public int idProveedor { get; set; }
        public decimal total { get; set; }
        public Boolean estado { get; set; }


    }
}
