using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaEntidad
{
    public class entProveedor
    {
        public int idProveedor { get; set; }
        public string razonSocial { get; set; }
        public string ruc { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public bool estProveedor { get; set; }

        // Propiedad adicional opcional para mostrar estado como texto
        public string estadoDescripcion
        {
            get
            {
                return estProveedor ? "Activo" : "Inactivo";
            }
        }
    }
}
