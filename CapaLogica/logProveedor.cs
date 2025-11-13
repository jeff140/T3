using System;
using System.Collections.Generic;
using CapaDatos;  
using CapaEntidad; 

namespace CapaLogica
{
    public class logProveedor
    {
        #region Singleton
        // Patrón Singleton
        private static readonly logProveedor _instancia = new logProveedor();
        public static logProveedor Instancia
        {
            get { return logProveedor._instancia; }
        }
        #endregion Singleton

        #region Metodos

       
        public List<entProveedor> ListarProveedores()
        {
            return datProveedor.Instancia.ListarProveedores();
        }

        
        public void InsertarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.InsertarProveedor(prov);
        }

        /// <summary>
        /// Llama a CapaDatos para actualizar proveedor
        /// </summary>
        public void ActualizarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.ActualizarProveedor(prov);
        }

        /// <summary>
        /// Llama a CapaDatos para eliminar (deshabilitar) proveedor
        /// </summary>
        public void EliminarProveedor(int idProveedor)
        {
            datProveedor.Instancia.EliminarProveedor(idProveedor);
        }

        #endregion Metodos
    }
}