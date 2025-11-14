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
        private readonly LogVenta logica = new LogVenta();

        public ventaProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            TipoMaderaOpciones();
            cboPagotipopago();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Validar campos obligatorios
                if (cboProducto.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    cboPago.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe completar los campos de producto, cantidad, precio y tipo de pago.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Validar que el producto tenga un ID válido
                if (cboProducto.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un producto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idProducto = 0;
                if (!int.TryParse(cboProducto.SelectedValue.ToString(), out idProducto))
                {
                    MessageBox.Show("El ID del producto no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3️⃣ Validar y convertir cantidad y precio
                if (!int.TryParse(txtCantidad.Text, out int cantidad) ||
                    !decimal.TryParse(txtPrecio.Text, out decimal precioUnitario))
                {
                    MessageBox.Show("Ingrese valores numéricos válidos para cantidad y precio.",
                                    "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4️⃣ Calcular descuento según precio o volumen
                LogVenta logicaVenta = new LogVenta();
                decimal descuento = 0;

                if (precioUnitario >= 100)
                {
                    descuento = logicaVenta.ObtenerDescuento("Descuento por volumen");
                    MessageBox.Show($"Se aplicó un descuento del {descuento}% al producto seleccionado.",
                                    "Descuento aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 5️⃣ Calcular subtotal, descuento y total
                decimal subtotal = precioUnitario * cantidad;
                decimal descuentoTotal = subtotal * (descuento / 100);
                decimal total = subtotal - descuentoTotal;

                // 6️⃣ Crear detalle de la venta
                List<EntidadDetalleVenta> detalles = new List<EntidadDetalleVenta>
        {
            new EntidadDetalleVenta
            {
                IdProducto = idProducto,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario,
                IdDescuento = (descuento > 0) ? 2 : (int?)null,
                Total = total
            }
        };

                // 7️⃣ Crear la cabecera de venta
                EntVenta venta = new EntVenta
                {
                    Subtotal = subtotal,
                    Cantidad = cantidad,
                    DescuentoTotal = descuentoTotal,
                    Total = total,
                    MetodoPago = cboPago.Text,
                    FechaVenta = dtpFecha.Value.Date
                };

                // 8️⃣ Registrar venta completa
                bool exito = logicaVenta.RegistrarVenta(venta, detalles);

                // 9️⃣ Mostrar resultado
                if (exito)
                {
                    MessageBox.Show("✅ Venta registrada correctamente en la base de datos.",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    cboProducto.SelectedIndex = -1;
                    txtCantidad.Clear();
                    txtPrecio.Clear();
                    cboPago.SelectedIndex = -1;

                    // 🔄 Refrescar DataGridView (si tienes método para ello)
                    CargarDatosVenta();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la venta. Revise la conexión o los datos.",
                                    "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Los valores de cantidad y precio deben ser numéricos.",
                                "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ConfigurarDataGridView()
        {
            if (dgvVenta.Columns.Count > 0)
            {
                dgvVenta.Columns["idVenta"].HeaderText = "ID";
                dgvVenta.Columns["fechaVenta"].HeaderText = "fecha";
                dgvVenta.Columns["subtotal"].HeaderText = "sub total";
                dgvVenta.Columns["Cantidad"].HeaderText = "cantidad";
                dgvVenta.Columns["descuentoTotal"].HeaderText = "DescTotal";
                dgvVenta.Columns["total"].HeaderText = "Total";
                dgvVenta.Columns["metodoPago"].HeaderText = "MetodoPago";

                dgvVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvVenta.MultiSelect = false;
                dgvVenta.ReadOnly = true;
            }
        }

        private void TipoMaderaOpciones()
        {
            cboCategoria.Items.Clear();
            cboCategoria.Items.Add("selecciona producto");
            cboCategoria.Items.Add("pino/madera blanda");
            cboCategoria.Items.Add("maderas estructurales");
            cboCategoria.Items.Add("tableros (MDF/Melamina)");
        }
        private void cboPagotipopago()
        {
            cboPago.Items.Clear();
            cboPago.Items.Add("selecciona metodo");
            cboPago.Items.Add("Efectivo");
            cboPago.Items.Add("Transferencia");
            cboPago.Items.Add("Tarjeta");
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {
            cboProducto.SelectedIndexChanged -= cboProducto_SelectedIndexChanged;

            try
            {
                logProducto logicaProducto = new logProducto();
                var lista = logicaProducto.ListarProducto();

                cboProducto.DataSource = lista;
                cboProducto.DisplayMember = "nombreProducto";
                cboProducto.ValueMember = "idProducto";

                cboProducto.SelectedIndex = -1;
            }
            finally
            {
                cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
            }
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProducto.SelectedValue != null && cboProducto.SelectedIndex != -1)
            {
                try
                {
                    int idProducto = Convert.ToInt32(cboProducto.SelectedValue);
                    logProducto logica = new logProducto();
                    var producto = logica.ObtenerProductoPorId(idProducto);

                    txtPrecio.Text = producto?.precioUnitario.ToString("F2") ?? "";
                }
                catch (Exception ex)
                {
                    // Manejo de errores más específico
                    MessageBox.Show("Error al cargar el precio (Consulte el log de errores): " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Clear(); // Limpiar en caso de error
                }
            }
            else
            {
                txtPrecio.Clear();
            }
        }


        private decimal ObtenerDescuentoDesdeBD(string nombreDescuento)
        {
            // Simulación: Retorna un descuento fijo del 10%
            // En una aplicación real, aquí consultarías la base de datos.
            if (nombreDescuento == "Descuento por precio")
                return 10m;
            return 0m;
        }
    }
}
