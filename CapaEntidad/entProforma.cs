using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entProforma
    {
        public int IdProforma { get; set; }
        public string NumeroProforma { get; set; }
        public int IdCliente { get; set; } // (ID del cliente)
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; } = DateTime.MinValue;
        public decimal Subtotal { get; set; } = 0m;
        public decimal Descuento { get; set; } = 0m;

        // Propiedad calculada
        public decimal Total
        {
            get
            {
                return Math.Round(Subtotal - (Subtotal * (Descuento / 100m)), 2);
            }
        }
        public string Estado { get; set; } = "Vigente";
        public int? IdUsuario { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con el detalle
        public List<entProformaItem> Items { get; set; } = new List<entProformaItem>();
    }
}
