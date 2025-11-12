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
        public Proforma()
        {
            InitializeComponent();
        }

        private void Proforma_Load(object sender, EventArgs e)
        {
            CargarClientes();
            ConfigurarGrilla();
            InicializarFormulario();
        }

        private void CargarClientes()
        {
            try
            {
                // --- CORRECCIÓN APLICADA ---
                // Usamos el Singleton de la capa LÓGICA
                List<entCliente> listaClientes = logCliente.Instancia.ListarCliente();

                cmbCliente.DataSource = listaClientes;
                cmbCliente.DisplayMember = "Nom_razonSocial"; // Propiedad de EntCliente
                cmbCliente.ValueMember = "idCliente";       // Propiedad de EntCliente
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            // Columna Producto (usa la propiedad 'NombreProducto' de EntProformaItem)
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Columna Cantidad (usa la propiedad 'Cantidad' de EntProformaItem)
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cant.",
                DataPropertyName = "Cantidad",
                Width = 60
            });

            // Columna Precio (usa la propiedad 'PrecioUnitario' de EntProformaItem)
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                DataPropertyName = "PrecioUnitario",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, // Moneda
                Width = 100
            });

            // Columna Subtotal (usa la propiedad 'Subtotal' de EntProformaItem)
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, // Moneda
                Width = 100
            });
        }

        private void InicializarFormulario()
        {
            detalleItems = new List<entProformaItem>();

            txtNumeroProforma.Text = "[Autogenerado]";
            cmbCliente.SelectedIndex = -1;
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
                // Si el usuario escribe "abc", lo ignoramos temporalmente
                // Opcional: podrías mostrar un error
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
            // 1. Crea una instancia del formulario buscador
            using (BuscarProductoSimple frmBuscar = new BuscarProductoSimple())
            {
                // 2. Muestra el formulario como un diálogo modal
                //    (El código se detiene aquí hasta que 'frmBuscar' se cierre)
                DialogResult resultado = frmBuscar.ShowDialog();

                // 3. Verifica si el usuario presionó "Seleccionar" (OK)
                if (resultado == DialogResult.OK)
                {
                    // 4. Obtiene el item que 'frmBuscar' preparó
                    entProformaItem nuevoItem = frmBuscar.ItemSeleccionado;

                    // 5. Calcula el Subtotal del item (aquí en el formulario principal)
                    nuevoItem.Subtotal = Math.Round(nuevoItem.Cantidad * nuevoItem.PrecioUnitario, 2);

                    // 6. Añade el item a nuestra lista en memoria
                    detalleItems.Add(nuevoItem);

                    // 7. Refresca la grilla y los totales (usando los métodos que ya creamos)
                    ActualizarGrilla();
                    CalcularTotales();
                }
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            // 1. Validar que haya una fila seleccionada en la grilla
            if (dgvItems.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un ítem de la grilla para quitarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener el objeto EntProformaItem de la fila seleccionada
            // (Esta es la forma más segura de obtener el objeto)
            entProformaItem itemParaQuitar = (entProformaItem)dgvItems.CurrentRow.DataBoundItem;

            // 3. Quitar el item de nuestra lista en memoria
            detalleItems.Remove(itemParaQuitar);

            // 4. Refrescar la grilla y los totales (reutilizando nuestros métodos)
            ActualizarGrilla();
            CalcularTotales();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIONES DE UI (Rápidas) ---
            if (cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return; // Detiene la ejecución
            }

            if (detalleItems.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la proforma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAgregarProducto.Focus();
                return; // Detiene la ejecución
            }

            // --- 2. CREAR EL OBJETO A ENVIAR ---
            entProforma proforma = new entProforma();

            // --- 3. POBLAR LA CABECERA ---
            proforma.IdCliente = (int)cmbCliente.SelectedValue; // (int)
            proforma.FechaEmision = dtpFechaEmision.Value.Date;
            proforma.FechaVencimiento = dtpFechaVencimiento.Value.Date;
            proforma.Comentarios = txtComentarios.Text.Trim();

            // Asumiendo que tienes un ID de usuario guardado globalmente
            // proforma.IdUsuario = SesionActual.IdUsuario; // (Ejemplo)

            // Lee el descuento
            decimal descuento = 0;
            decimal.TryParse(txtDescuento.Text, out descuento);
            proforma.Descuento = descuento;


            // --- 4. POBLAR EL DETALLE ---
            // (Tu logProforma espera la lista de items)
            proforma.Items = detalleItems;

            // NOTA: No calculamos el Subtotal aquí.
            // Tu capa 'logProforma' es la responsable de hacer ese cálculo
            // antes de pasarlo a 'datProforma'.

            // --- 5. LLAMAR A LA CAPA LÓGICA ---
            try
            {
                string numeroProformaGenerado;

                // Llamamos al Singleton de la lógica
                int idProformaGenerada = logProforma.Instancia.InsertarProforma(proforma, out numeroProformaGenerado);

                // --- 6. MANEJAR RESPUESTA ---
                if (idProformaGenerada > 0)
                {
                    MessageBox.Show($"¡Proforma guardada exitosamente!\nNúmero: {numeroProformaGenerado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mostramos el número en la caja de texto
                    txtNumeroProforma.Text = numeroProformaGenerado;

                    // Limpiamos el formulario para la siguiente proforma
                    InicializarFormulario();
                }
                else
                {
                    // Si la lógica devuelve 0 pero no lanza excepción
                    MessageBox.Show("No se pudo guardar la proforma. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción que venga de 'logProforma' o 'datProforma'
                // (Ej: "El cliente no es válido", "El DNI ya existe", "Error de SQL")
                MessageBox.Show("Error al guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
