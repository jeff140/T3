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
        private datProducto datos = new datProducto();


        // LOGPRODUCTO.CS (Añadir dentro de la región #region metodos)

        public void DeshabilitarProducto(entProducto prod)
        {
            // Llama directamente a la Capa de Datos
            datProducto.Instancia.DeshabilitarProducto(prod);
        }

        public entProducto ObtenerProductoPorId(int idProducto)
        {
            return datos.ObtenerProductoPorId(idProducto);
        }
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
       
        #endregion metodos

        #region metodos
        
        public List<entProducto> BuscarProducto(string textoBusqueda)
        {
            return datProducto.Instancia.BuscarProducto(textoBusqueda);
        }
        #endregion metodos

    }
}