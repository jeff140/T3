using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entDetalleGuiaRemision
    {
        public int idDetalleGuia { get; set; }
        public int idGuiaRemision { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public string descripcion { get; set; }

        // Propiedades adicionales para mostrar información
        public string nombreProducto { get; set; }

        // Constructor por defecto
        public entDetalleGuiaRemision()
        {
            descripcion = string.Empty;
            nombreProducto = string.Empty;
            cantidad = 0;
        }
    }
}