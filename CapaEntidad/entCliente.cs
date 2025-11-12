using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entCliente
    {

        public int idCliente { get; set; }
        public string DniRucCli { get; set; }
        public string Nom_razonSocial { get; set; }
        public DateTime fecRegCliente { get; set; }
        public int idDepartamento { get; set; }
        public string Telefono { get; set; }

        // Constructor por defecto
        public entCliente()
        {
            DniRucCli = string.Empty;
            Nom_razonSocial = string.Empty;
            Telefono = string.Empty;
            fecRegCliente = DateTime.Now;
            idDepartamento = 0;
        }
    }
}