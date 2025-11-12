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
    public partial class BuscarProductoSimple : Form
    {
        public entProformaItem ItemSeleccionado { get; private set; }
        private List<entProducto> listaProductos;
        public BuscarProductoSimple()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void BuscarProductoSimple_Load(object sender, EventArgs e)
        {
            CargarProductos();
            ConfigurarGrillaProductos();
        }

        private void CargarProductos()
        {
            try
            {
                // Usamos el Singleton de la capa LÓGICA
                listaProductos = logProducto.Instancia.ListarProducto();

                // Mostramos la lista completa en la grilla
                dgvProductos.DataSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillaProductos()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            // Usamos los ALIAS que definiste en 'spListarProducto'

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Producto",
                DataPropertyName = "Nombre", // Alias de 'spListarProducto'
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio", // Alias de 'spListarProducto'
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 100
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock", // Alias de 'spListarProducto'
                Width = 60
            });

            // Ocultamos el ID, no necesitamos mostrarlo
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idProducto",
                DataPropertyName = "idProducto",
                Visible = false
            });
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            // 1. Validar fila
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un producto de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener cantidad (Opción A: int)
            // numCantidad.Value devuelve 'decimal', lo convertimos a int
            int cantidad = (int)numCantidad.Value;

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser 1 o más.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Obtener el producto de la fila seleccionada
            // (Es más seguro obtener el objeto completo que leer celda por celda)
            entProducto productoElegido = (entProducto)dgvProductos.CurrentRow.DataBoundItem;

            // 4. Crear el EntProformaItem que devolveremos
            ItemSeleccionado = new entProformaItem();
            ItemSeleccionado.IdProducto = productoElegido.idProducto;
            ItemSeleccionado.NombreProducto = productoElegido.nombreProducto; // 'Nombre' es el alias del SP
            ItemSeleccionado.PrecioUnitario = productoElegido.precioUnitario; // 'Precio' es el alias del SP
            ItemSeleccionado.Cantidad = cantidad; // Cantidad (int)
            // El 'Subtotal' se calculará en FormProforma

            // 5. Cerrar el formulario con resultado "OK"
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                // Si no hay texto, mostrar todo
                dgvProductos.DataSource = listaProductos;
            }
            else
            {
                // Filtrar la lista en memoria (rápido)
                List<entProducto> filtrados = listaProductos
                    .Where(p => p.nombreProducto.ToLower().Contains(filtro))
                    .ToList();

                dgvProductos.DataSource = filtrados;
            }
        }
    }
}
