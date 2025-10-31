using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaLogica
{
    public class LogicaProducto
    {
        private DatosProducto datos = new DatosProducto();

        public List<EntidadProducto> ObtenerProductos()
        {
            return datos.ListarProductos();
        }
    }
}
