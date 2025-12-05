using System;
using System.Collections.Generic;
using CapaDatos;   // Importante para conectar con los datos
using CapaEntidad; // Importante para usar entProveedor

namespace CapaLogica
{
    public class logProveedor
    {
        #region Singleton
        // Esta es la parte que te falta y causa el error:
        private static readonly logProveedor _instancia = new logProveedor();

        // Esta es la propiedad 'Instancia' que buscan tus formularios
        public static logProveedor Instancia
        {
            get { return logProveedor._instancia; }
        }
        #endregion Singleton

        #region Metodos

        // Listar todos
        public List<entProveedor> ListarProveedores()
        {
            return datProveedor.Instancia.ListarProveedores();
        }

        // Nuevo método para filtrar Activos/Inactivos (que agregamos antes)
        public List<entProveedor> ListarPorEstado(bool estado)
        {
            return datProveedor.Instancia.ListarPorEstado(estado);
        }

        // Insertar
        public void InsertarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.InsertarProveedor(prov);
        }

        // Actualizar
        public void ActualizarProveedor(entProveedor prov)
        {
            datProveedor.Instancia.ActualizarProveedor(prov);
        }

        // Eliminar
        public void EliminarProveedor(int idProveedor)
        {
            datProveedor.Instancia.EliminarProveedor(idProveedor);
        }

        #endregion Metodos
    }
}