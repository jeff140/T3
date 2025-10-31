using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logCliente
    {

        #region Singleton
        private static readonly logCliente _instancia = new logCliente();
        public static logCliente Instancia => _instancia;
        #endregion

        #region Métodos

        public List<entCliente> ListarCliente()
        {
            try
            {
                return datCliente.Instancia.ListarCliente();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes: " + ex.Message, ex);
            }
        }

        public bool InsertaCliente(entCliente cli)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(cli.DniRucCli))
                throw new ArgumentException("El DNI/RUC del cliente es obligatorio.");

            if (cli.DniRucCli.Length < 8 || cli.DniRucCli.Length > 20)
                throw new ArgumentException("El DNI/RUC debe tener entre 8 y 20 caracteres.");

            if (string.IsNullOrWhiteSpace(cli.Nom_razonSocial))
                throw new ArgumentException("El nombre/razón social es obligatorio.");

            if (cli.idDepartamento <= 0)
                throw new ArgumentException("Debe seleccionar un departamento válido.");

            try
            {
                return datCliente.Instancia.InsertarCliente(cli);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar cliente: " + ex.Message, ex);
            }
        }

        public bool EditaCliente(entCliente cli)
        {
            if (cli.idCliente <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            if (string.IsNullOrWhiteSpace(cli.DniRucCli))
                throw new ArgumentException("El DNI/RUC del cliente es obligatorio.");

            if (cli.DniRucCli.Length < 8 || cli.DniRucCli.Length > 20)
                throw new ArgumentException("El DNI/RUC debe tener entre 8 y 20 caracteres.");

            if (string.IsNullOrWhiteSpace(cli.Nom_razonSocial))
                throw new ArgumentException("El nombre/razón social es obligatorio.");

            if (cli.idDepartamento <= 0)
                throw new ArgumentException("Debe seleccionar un departamento válido.");

            try
            {
                return datCliente.Instancia.EditarCliente(cli);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar cliente: " + ex.Message, ex);
            }
        }

        #endregion
    }
}