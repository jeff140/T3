using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logCategoria
    {  
        #region singleton
        private static readonly logCategoria _instancia = new logCategoria();
        public static logCategoria Instancia
        {
            get
            {
                return logCategoria._instancia;
            }
        }
#endregion singleton

        #region metodos
        // Listado
        public List<entCategoria> ListarCategoria()
        {
            return datCategoria.Instancia.ListarCategoria();
        }

        // Inserta
        public void InsertarCategoria(entCategoria cat)
        {
            datCategoria.Instancia.InsertarCategoria(cat);
        }

        // Edita
        public void EditarCategoria(entCategoria cat)
        {
            datCategoria.Instancia.EditarCategoria(cat);
        }

        // Deshabilita
        public void DeshabilitarCategoria(entCategoria cat)
        {
            datCategoria.Instancia.DeshabilitarCategoria(cat);
        }
        #endregion metodos
    }
}