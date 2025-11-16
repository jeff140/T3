using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logDetalleGuiaRemision
    {
        #region Singleton
        private static readonly logDetalleGuiaRemision _instancia = new logDetalleGuiaRemision();
        public static logDetalleGuiaRemision Instancia => _instancia;
        #endregion

        #region Métodos

        public List<entDetalleGuiaRemision> ListarDetallePorGuia(int idGuiaRemision)
        {
            if (idGuiaRemision <= 0)
                throw new ArgumentException("El ID de la guía de remisión no es válido.");

            try
            {
                return datDetalleGuiaRemision.Instancia.ListarDetallePorGuia(idGuiaRemision);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar detalles de guía de remisión: " + ex.Message, ex);
            }
        }

        public bool InsertarDetalleGuia(entDetalleGuiaRemision detalle)
        {
            // Validaciones
            if (detalle.idGuiaRemision <= 0)
                throw new ArgumentException("El ID de la guía de remisión no es válido.");

            if (detalle.idProducto <= 0)
                throw new ArgumentException("Debe especificar un producto válido.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            if (!string.IsNullOrWhiteSpace(detalle.descripcion) && detalle.descripcion.Length > 200)
                throw new ArgumentException("La descripción no puede superar los 200 caracteres.");

            try
            {
                return datDetalleGuiaRemision.Instancia.InsertarDetalleGuia(detalle);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar detalle de guía de remisión: " + ex.Message, ex);
            }
        }

        public bool EditarDetalleGuia(entDetalleGuiaRemision detalle)
        {
            // Validaciones
            if (detalle.idDetalleGuia <= 0)
                throw new ArgumentException("El ID del detalle no es válido.");

            if (detalle.idProducto <= 0)
                throw new ArgumentException("Debe especificar un producto válido.");

            if (detalle.cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            if (!string.IsNullOrWhiteSpace(detalle.descripcion) && detalle.descripcion.Length > 200)
                throw new ArgumentException("La descripción no puede superar los 200 caracteres.");

            try
            {
                return datDetalleGuiaRemision.Instancia.EditarDetalleGuia(detalle);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar detalle de guía de remisión: " + ex.Message, ex);
            }
        }

        public bool EliminarDetallesPorGuia(int idGuiaRemision)
        {
            if (idGuiaRemision <= 0)
                throw new ArgumentException("El ID de la guía de remisión no es válido.");

            try
            {
                return datDetalleGuiaRemision.Instancia.EliminarDetallesPorGuia(idGuiaRemision);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar detalles de guía de remisión: " + ex.Message, ex);
            }
        }

        #endregion
    }
}