using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logProveedor
    { 
        #region Singleton
        private static readonly logProveedor _instancia = new logProveedor();
        public static logProveedor Instancia
        {
            get
            {
                return logProveedor._instancia;
            }
        }
#endregion Singleton

        #region Métodos

        // ✅ Listar Proveedores
        public List<entProveedor> ListarProveedor()
        {
            return datProveedor.Instancia.ListarProveedor();
        }

        // ✅ Insertar Proveedor
        public void InsertarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.InsertarProveedor(prov);
        }

        // ✅ Editar Proveedor
        public void EditarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.EditarProveedor(prov);
        }

        // ✅ Deshabilitar Proveedor
        public void DeshabilitarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.DeshabilitarProveedor(prov);
        }

        #endregion Métodos
    }
}