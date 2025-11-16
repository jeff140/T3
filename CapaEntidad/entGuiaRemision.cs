using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entGuiaRemision
    {
        public int idGuiaRemision { get; set; }
        public string numeroGuia { get; set; }
        public DateTime fechaEmision { get; set; }
        public DateTime fechaTransporte { get; set; }
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public string numeroComprobante { get; set; }
        public string direccionPartida { get; set; }
        public string direccionLlegada { get; set; }
        public string motivoTraslado { get; set; }
        public string transporte { get; set; }
        public string placaVehiculo { get; set; }
        public bool estado { get; set; }
        public entGuiaRemision()
        {
            numeroGuia = string.Empty;
            fechaEmision = DateTime.Now;
            fechaTransporte = DateTime.Now;
            numeroComprobante = string.Empty;
            direccionPartida = string.Empty;
            direccionLlegada = string.Empty;
            motivoTraslado = string.Empty;
            transporte = string.Empty;
            placaVehiculo = string.Empty;
            estado = true;
        }
    }
}