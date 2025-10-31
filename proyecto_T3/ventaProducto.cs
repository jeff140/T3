using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaLogica;

namespace proyecto_T3
{
    public partial class ventaProducto : Form
    {
        public ventaProducto()
        {
            InitializeComponent();
            configurarcomboboxproducto();
            TipoMaderaOpciones();
            cboPagotipopago();
        }
        private void configurarcomboboxproducto()
        {
            cboProducto.Items.Clear();
            cboProducto.Items.Add("selecciona producto");
            cboProducto.Items.Add("VIGA SUPER 8 m.t.");
            cboProducto.Items.Add("MANDALLON 8 m.t.");
            cboProducto.Items.Add("VIGA 7 m.t.");
            cboProducto.Items.Add("MANDALLON 7 m.t.");
            cboProducto.Items.Add("POSTE 3 m.t.");
            cboProducto.Items.Add("PARADOR NORMAL 2.5 m.t.");
            cboProducto.Items.Add("LEÑA DE RAMAZON");
            cboProducto.Items.Add("PELO");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validación de campos obligatorios
            if (cboProducto.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) || cboPago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar todos los campos de producto, cantidad, precio y tipo de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Creación y llenado del objeto Entidad
                // Reemplaza 'Venta' con el nombre de tu clase de entidad (e.g., VentaProducto, DetalleVenta)
                CapaEntidad.EntVenta objVenta = new CapaEntidad.EntVenta();

                objVenta.ProductoNombre = cboProducto.Text;
                objVenta.Cantidad = int.Parse(txtCantidad.Text);
                objVenta.PrecioUnitario =   decimal.Parse(txtPrecio.Text);
                objVenta.TipoPago = cboPago.Text;
                objVenta.FechaVenta = dtpFecha.Value.Date; // Obtiene solo la fecha

                // Intenta convertir a numérico y lanza excepción si falla
                objVenta.Cantidad = int.Parse(txtCantidad.Text);
                objVenta.PrecioUnitario = decimal.Parse(txtPrecio.Text);

                // 3. Llamada a la Capa de Lógica para Inserción
                // Reemplaza 'LogicaNegocio' con el nombre de tu clase de lógica
                CapaLogica.LogVenta logica = new CapaLogica.LogVenta();
                bool exito = logica.InsertarNuevaVenta(objVenta);

                if (exito)
                {
                    MessageBox.Show("Venta agregada y registrada en la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Opcional: Actualizar el DataGridView (dgvVenta) y limpiar campos
                    CargarDatosVenta(); // Asume que tienes un método para recargar el DGV
                    LimpiarCamposIngreso();
                }
                else
                {
                    MessageBox.Show("La operación falló al intentar guardar la venta.", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("La cantidad y el precio deben ser valores numéricos válidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otra excepción (ej. error de conexión a BD)
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // *** Función auxiliar recomendada para limpiar campos ***
        private void LimpiarCamposIngreso()
        {
            cboProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtPrecio.Clear();
            cboPago.SelectedIndex = -1;
            cboProducto.Focus();
        }

        // *** Función auxiliar recomendada para recargar el DGV ***
        // (Necesitarías el código de tu capa de datos para implementarla completamente)
        private void CargarDatosVenta()
        {
            // Ejemplo: dgvVenta.DataSource = new CapaLogica.LogicaNegocio().ObtenerTodasLasVentas();
        }

        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Validar que el clic fue en la columna de botones y no en el encabezado
            // Reemplaza "btnEliminar" con el nombre exacto de la columna de tu botón.
            if (dgvVenta.Columns[e.ColumnIndex].Name == "btnEliminar" && e.RowIndex >= 0)
            {
                // 2. Preguntar confirmación antes de eliminar
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro que desea eliminar este registro de venta?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Obtener el ID de la fila
                        // Se asume que la columna 0 (o la que tú uses) contiene el ID de la venta (IdVenta)
                        int idVenta = Convert.ToInt32(dgvVenta.Rows[e.RowIndex].Cells["IdVenta"].Value);

                        // 4. Llamar a la Capa de Lógica para ejecutar la eliminación en BD
                        CapaLogica.LogVenta logica = new CapaLogica.LogVenta();
                        bool exito = logica.EliminarVenta(idVenta);

                        if (exito)
                        {
                            MessageBox.Show("Venta eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Opcional: Refrescar el DataGridView para reflejar el cambio
                            CargarDatosVenta();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al procesar la eliminación: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void TipoMaderaOpciones()
        {
            cboTipoMadera.Items.Clear();
            cboTipoMadera.Items.Add("selecciona producto");
            cboTipoMadera.Items.Add("pino/madera blanda");
            cboTipoMadera.Items.Add("maderas estructurales");
            cboTipoMadera.Items.Add("tableros");
            cboTipoMadera.Items.Add("elementos varios(leña)");
        }
        private void cboPagotipopago()
        {
            cboPago.Items.Clear();
            cboPago.Items.Add("selecciona producto");
            cboPago.Items.Add("Efectivo");
            cboPago.Items.Add("Transferencia");
            cboPago.Items.Add("Tarjeta");
        }

        private void cboPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
