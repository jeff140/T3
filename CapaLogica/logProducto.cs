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
        // Listado (El que usan tus compañeros - INTACTO)
        public List<entProducto> ListarProducto()
        {
            return datProducto.Instancia.ListarProducto();
        }

        // Inserta (INTACTO)
        public void InsertarProducto(entProducto prod)
        {
            datProducto.Instancia.InsertarProducto(prod);
        }

        // Edita (INTACTO)
        public void EditarProducto(entProducto prod)
        {
            datProducto.Instancia.EditarProducto(prod);
        }

        // Deshabilita (INTACTO)
        public void DeshabilitarProducto(entProducto prod)
        {
            datProducto.Instancia.DeshabilitarProducto(prod);
        }

        // ---------------------------------------------------------
        // NUEVO MÉTODO (SOLO PARA TU MÓDULO DE PROFORMA)
        // ---------------------------------------------------------
        public List<entProducto> ListarProductoDisponible(List<entProformaItem> itemsTemporales)
        {
            // 1. Traemos la lista original (reutilizamos el método existente)
            List<entProducto> listaBase = datProducto.Instancia.ListarProducto();

            // 2. Si no hay items en la proforma, devolvemos la lista normal
            if (itemsTemporales == null || itemsTemporales.Count == 0)
            {
                return listaBase;
            }

            // 3. Aplicamos la resta visual sin tocar la base de datos
            foreach (var prod in listaBase)
            {
                // Buscamos si el producto está en tu lista temporal
                var itemTemp = itemsTemporales.FirstOrDefault(x => x.IdProducto == prod.idProducto);

                if (itemTemp != null)
                {
                    // Restamos la cantidad visualmente
                    prod.stock = prod.stock - itemTemp.Cantidad;
                }
            }

            return listaBase;
        }
        #endregion metodos
    }
}