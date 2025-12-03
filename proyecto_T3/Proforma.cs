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
    public partial class Proforma : Form
    {
        private List<entProformaItem> detalleItems;

        private int _idClienteSeleccionado = 0;

        public Proforma()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Proforma_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla();
            InicializarFormulario();
        }

        private void ConfigurarGrilla()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            // Columna Producto
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Columna Cantidad
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cant.",
                DataPropertyName = "Cantidad",
                Width = 60
            });

            // Columna Precio
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                DataPropertyName = "PrecioUnitario",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });

            // Columna Subtotal
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });
        }

        private void InicializarFormulario()
        {
            detalleItems = new List<entProformaItem>();

            txtNumeroProforma.Text = "[Autogenerado]";

            // --- NUEVO: Limpiamos la selección de cliente ---
            _idClienteSeleccionado = 0;
            txtClienteNombre.Text = ""; // Limpiamos la caja de texto

            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaVencimiento.Value = DateTime.Now.AddDays(15);
            txtComentarios.Clear();
            txtDescuento.Text = "0";

            ActualizarGrilla();
            CalcularTotales();
        }

        private void ActualizarGrilla()
        {
            dgvItems.DataSource = null;

            if (detalleItems.Any())
            {
                dgvItems.DataSource = detalleItems;
            }
        }

        private void CalcularTotales()
        {
            decimal subtotal = detalleItems.Sum(item => item.Subtotal);

            decimal descuentoPorcentaje = 0;
            // Validamos que el texto sea un número
            if (!decimal.TryParse(txtDescuento.Text, out descuentoPorcentaje) && !string.IsNullOrEmpty(txtDescuento.Text))
            {
                // Ignoramos si no es número
            }

            decimal total = Math.Round(subtotal - (subtotal * (descuentoPorcentaje / 100m)), 2);

            lblSubtotal.Text = subtotal.ToString("C2");
            lblTotal.Text = total.ToString("C2");
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            using (BuscarProductoSimple frmBuscar = new BuscarProductoSimple())
            {
                // 1. Pasamos la lista para que el buscador sepa qué stock restar visualmente
                frmBuscar.ItemsEnProformaActual = detalleItems;

                if (frmBuscar.ShowDialog() == DialogResult.OK)
                {
                    entProformaItem nuevoItem = frmBuscar.ItemSeleccionado;

                    // --- AQUÍ ESTÁ LA SOLUCIÓN ---

                    // 2. Buscamos si el producto ya existe en nuestra lista 'detalleItems'
                    // Usamos una expresión Lambda para buscar por ID
                    var itemExistente = detalleItems.FirstOrDefault(x => x.IdProducto == nuevoItem.IdProducto);

                    if (itemExistente != null)
                    {
                        // CASO A: EL PRODUCTO YA EXISTE
                        // En lugar de agregar uno nuevo, sumamos la cantidad al existente
                        itemExistente.Cantidad += nuevoItem.Cantidad;

                        // Recalculamos el subtotal de esa fila (Precio * Nueva Cantidad)
                        itemExistente.Subtotal = Math.Round(itemExistente.Cantidad * itemExistente.PrecioUnitario, 2);
                    }
                    else
                    {
                        // CASO B: ES NUEVO
                        // Calculamos su subtotal inicial
                        nuevoItem.Subtotal = Math.Round(nuevoItem.Cantidad * nuevoItem.PrecioUnitario, 2);

                        // Lo agregamos a la lista
                        detalleItems.Add(nuevoItem);
                    }

                    // 3. Refrescamos la pantalla
                    ActualizarGrilla();
                    CalcularTotales();
                }
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un ítem de la grilla para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            entProformaItem itemParaQuitar = (entProformaItem)dgvItems.CurrentRow.DataBoundItem;
            detalleItems.Remove(itemParaQuitar);

            ActualizarGrilla();
            CalcularTotales();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIONES MODIFICADAS ---

            // Validamos la variable _idClienteSeleccionado en lugar del Combo
            if (_idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe buscar y seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnBuscarCliente.Focus();
                return;
            }

            if (detalleItems.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la proforma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAgregarProducto.Focus();
                return;
            }

            // --- 2. CREAR EL OBJETO A ENVIAR ---
            entProforma proforma = new entProforma();

            // Usamos el ID de la variable
            proforma.IdCliente = _idClienteSeleccionado;

            proforma.FechaEmision = dtpFechaEmision.Value.Date;
            proforma.FechaVencimiento = dtpFechaVencimiento.Value.Date;
            proforma.Comentarios = txtComentarios.Text.Trim();

            decimal descuento = 0;
            decimal.TryParse(txtDescuento.Text, out descuento);
            proforma.Descuento = descuento;

            proforma.Items = detalleItems;

            // --- 5. LLAMAR A LA CAPA LÓGICA ---
            try
            {
                string numeroProformaGenerado;

                int idProformaGenerada = logProforma.Instancia.InsertarProforma(proforma, out numeroProformaGenerado);

                if (idProformaGenerada > 0)
                {
                    MessageBox.Show($"¡Proforma guardada exitosamente!\nNúmero: {numeroProformaGenerado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtNumeroProforma.Text = numeroProformaGenerado;
                    InicializarFormulario();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar la proforma. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (BuscarClienteSimple frm = new BuscarClienteSimple())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Obtenemos el cliente seleccionado
                    entCliente cliente = frm.ClienteSeleccionado;

                    // Guardamos el ID en la variable
                    _idClienteSeleccionado = cliente.idCliente;

                    // Mostramos el nombre en la caja de texto visual
                    txtClienteNombre.Text = cliente.Nom_razonSocial;
                }
            }
        }
    }
}
