using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logOrden_Compra
    {
        #region singleton
        private static readonly logOrden_Compra _instancia = new logOrden_Compra();
        public static logOrden_Compra Instancia
        {
            get
            {
                return logOrden_Compra._instancia;
            }
        }
        #endregion singleton

        #region metodos
        // Listado
        public List<entOrden_Compra> ListarOrdenCompra()
        {
            return datOrden_Compra.Instancia.ListarOrdenCompra();
        }

        // Inserta (retorna ID generado)
        public int InsertarOrdenCompra(entOrden_Compra oc)
        {
            return datOrden_Compra.Instancia.InsertarOrdenCompra(oc);
        }

        // Edita
        public void EditarOrdenCompra(entOrden_Compra oc)
        {
            datOrden_Compra.Instancia.EditarOrdenCompra(oc);
        }

        // Deshabilita
        public void DeshabilitarOrdenCompra(entOrden_Compra oc)
        {
            datOrden_Compra.Instancia.DeshabilitarOrdenCompra(oc);
        }
        // Agregar este método en la clase logOrdenCompra
        public int ObtenerSiguienteIdOrden()
        {
            return datOrden_Compra.Instancia.ObtenerSiguienteIdOrden();
        }
        #endregion metodos

    }
}
