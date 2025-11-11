using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logProforma
    {
        #region singleton
        private static readonly logProforma _instancia = new logProforma();
        public static logProforma Instancia { get { return _instancia; } }
        #endregion singleton

        // Inserta proforma después de validaciones
        public int InsertarProforma(entProforma pro, out string numero)
        {
            numero = null;

            // --- Validaciones de Negocio ---
            if (pro == null) throw new ArgumentNullException(nameof(pro));
            if (pro.IdCliente <= 0) throw new Exception("Seleccione un cliente válido.");
            if (pro.Items == null || pro.Items.Count == 0) throw new Exception("La proforma debe contener al menos un ítem.");
            if (pro.FechaVencimiento != DateTime.MinValue && pro.FechaVencimiento.Date < pro.FechaEmision.Date)
                throw new Exception("La fecha de vencimiento no puede ser anterior a la de emisión.");

            // --- Cálculo de Subtotales (La lógica lo hace, no la UI) ---
            decimal subtotal = 0m;
            foreach (var it in pro.Items)
            {
                if (it.IdProducto <= 0) throw new Exception("Producto inválido en ítems.");
                if (it.Cantidad <= 0) throw new Exception("La cantidad debe ser mayor que cero.");
                if (it.PrecioUnitario < 0) throw new Exception("Precio unitario inválido.");

                it.Subtotal = Math.Round(it.Cantidad * it.PrecioUnitario, 2);
                subtotal += it.Subtotal;
            }

            pro.Subtotal = subtotal; // Asigna el subtotal total calculado

            // Llamada a la capa de datos
            return datProforma.Instancia.InsertarProforma(pro, out numero);
        }

        public entProforma ObtenerPorNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return null;
            return datProforma.Instancia.GetProformaByNumero(numero);
        }
    }
}
