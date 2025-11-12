using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logDetalleOrdenCompra
    {

        #region singleton
        private static readonly logDetalleOrdenCompra _instancia = new logDetalleOrdenCompra();
        public static logDetalleOrdenCompra Instancia
        {
            get
            {
                return logDetalleOrdenCompra._instancia;
            }
        }
        #endregion singleton

        #region metodos

        // Listado
        public List<entDetalleOrdenCompra> ListarDetalleOrdenCompra(int idOrdenCompra)
        {
            return datDetalleOrdenCompra.Instancia.ListarDetalleOrdenCompra(idOrdenCompra);
        }

        // Inserta
        public void InsertarDetalleOrdenCompra(entDetalleOrdenCompra det)
        {
            datDetalleOrdenCompra.Instancia.InsertarDetalleOrdenCompra(det);
        }

        // Elimina
        public void EliminarDetalleOrdenCompra(int idDetalleOrden)
        {
            datDetalleOrdenCompra.Instancia.EliminarDetalleOrdenCompra(idDetalleOrden);
        }

        // Elimina todos los detalles de una orden
        public void EliminarDetallesPorOrden(int idOrdenCompra)
        {
            datDetalleOrdenCompra.Instancia.EliminarDetallesPorOrden(idOrdenCompra);
        }

        #endregion metodos
    }
}
