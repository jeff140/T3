using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logGuiaRemision
    {
        #region Singleton
        private static readonly logGuiaRemision _instancia = new logGuiaRemision();
        public static logGuiaRemision Instancia => _instancia;
        #endregion

        #region Métodos

        public List<entGuiaRemision> ListarGuiaRemision()
        {
            try
            {
                return datGuiaRemision.Instancia.ListarGuiaRemision();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar guías de remisión: " + ex.Message, ex);
            }
        }

        public int InsertarGuiaRemision(entGuiaRemision guia)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(guia.numeroGuia))
                throw new ArgumentException("El número de guía es obligatorio.");

            if (guia.numeroGuia.Length > 50)
                throw new ArgumentException("El número de guía no puede superar los 50 caracteres.");

            if (guia.fechaEmision == DateTime.MinValue)
                throw new ArgumentException("La fecha de emisión es obligatoria.");

            if (guia.fechaTransporte == DateTime.MinValue)
                throw new ArgumentException("La fecha de transporte es obligatoria.");

            if (guia.fechaTransporte < guia.fechaEmision)
                throw new ArgumentException("La fecha de transporte no puede ser anterior a la fecha de emisión.");

            if (guia.idVenta <= 0)
                throw new ArgumentException("Debe especificar una venta válida.");

            if (guia.idCliente <= 0)
                throw new ArgumentException("Debe especificar un cliente válido.");

            if (string.IsNullOrWhiteSpace(guia.direccionLlegada))
                throw new ArgumentException("La dirección de llegada es obligatoria.");

            if (guia.direccionLlegada.Length > 150)
                throw new ArgumentException("La dirección de llegada no puede superar los 150 caracteres.");

            if (string.IsNullOrWhiteSpace(guia.motivoTraslado))
                throw new ArgumentException("El motivo de traslado es obligatorio.");

            if (guia.motivoTraslado.Length > 100)
                throw new ArgumentException("El motivo de traslado no puede superar los 100 caracteres.");

            try
            {
                return datGuiaRemision.Instancia.InsertarGuiaRemision(guia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar guía de remisión: " + ex.Message, ex);
            }
        }

        public bool EditarGuiaRemision(entGuiaRemision guia)
        {
            // Validaciones
            if (guia.idGuiaRemision <= 0)
                throw new ArgumentException("El ID de la guía de remisión no es válido.");

            if (string.IsNullOrWhiteSpace(guia.numeroGuia))
                throw new ArgumentException("El número de guía es obligatorio.");

            if (guia.numeroGuia.Length > 50)
                throw new ArgumentException("El número de guía no puede superar los 50 caracteres.");

            if (guia.fechaEmision == DateTime.MinValue)
                throw new ArgumentException("La fecha de emisión es obligatoria.");

            if (guia.fechaTransporte == DateTime.MinValue)
                throw new ArgumentException("La fecha de transporte es obligatoria.");

            if (guia.fechaTransporte < guia.fechaEmision)
                throw new ArgumentException("La fecha de transporte no puede ser anterior a la fecha de emisión.");

            if (guia.idVenta <= 0)
                throw new ArgumentException("Debe especificar una venta válida.");

            if (guia.idCliente <= 0)
                throw new ArgumentException("Debe especificar un cliente válido.");

            if (string.IsNullOrWhiteSpace(guia.direccionLlegada))
                throw new ArgumentException("La dirección de llegada es obligatoria.");

            if (string.IsNullOrWhiteSpace(guia.motivoTraslado))
                throw new ArgumentException("El motivo de traslado es obligatorio.");

            try
            {
                return datGuiaRemision.Instancia.EditarGuiaRemision(guia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar guía de remisión: " + ex.Message, ex);
            }
        }

        public bool DeshabilitarGuiaRemision(int idGuiaRemision)
        {
            if (idGuiaRemision <= 0)
                throw new ArgumentException("El ID de la guía de remisión no es válido.");

            try
            {
                return datGuiaRemision.Instancia.DeshabilitarGuiaRemision(idGuiaRemision);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al deshabilitar guía de remisión: " + ex.Message, ex);
            }
        }

        #endregion
    }
}