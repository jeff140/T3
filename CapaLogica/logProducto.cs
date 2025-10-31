using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logProducto
    {
        #region singleton
        private static readonly logProducto _instancia = new logProducto();
        public static logProducto Instancia
        {
            get
            {
                return logProducto._instancia;
            }
        }
        #endregion singleton

        #region metodos
        // Listado
        public List<entProducto> ListarProducto()
        {
            return datProducto.Instancia.ListarProducto();
        }

        // Inserta
        public void InsertarProducto(entProducto prod)
        {
            datProducto.Instancia.InsertarProducto(prod);
        }

        // Edita
        public void EditarProducto(entProducto prod)
        {
            datProducto.Instancia.EditarProducto(prod);
        }

        // Deshabilita
        public void DeshabilitarProducto(entProducto prod)
        {
            datProducto.Instancia.DeshabilitarProducto(prod);
        }
        #endregion metodos
    }
}