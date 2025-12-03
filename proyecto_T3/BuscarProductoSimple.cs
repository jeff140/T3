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
        public List<entProformaItem> ItemsEnProformaActual { get; set; } = new List<entProformaItem>();
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
                // CAMBIO: Usamos TU método nuevo y le pasamos la lista que recibimos de Proforma
                listaProductos = logProducto.Instancia.ListarProductoDisponible(ItemsEnProformaActual);

                // Mostramos el resultado final
                dgvProductos.DataSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillaProductos()
        {
            // 1. Evitamos que se generen columnas automáticas "basura"
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            // 2. IMPORTANTE: Desactivamos el auto-ajuste para permitir el SCROLL (Deslizamiento)
            // Si estaba en 'Fill', las columnas se aplastaban. En 'None', aparece la barra de abajo.
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // --- AHORA AGREGAMOS LAS COLUMNAS EN EL ORDEN QUE PEDISTE ---

            // 1. ID Producto
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idProducto",
                HeaderText = "ID",
                DataPropertyName = "idProducto",
                Width = 50, // Pequeño
                Visible = true // Lo pediste visible
            });

            // 2. Nombre del Producto
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Producto / Descripción",
                DataPropertyName = "nombreProducto",
                Width = 250 // Ancho generoso para que se lea el nombre completo
            });

            // 3. Precio
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "precioUnitario",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 80
            });

            // Stock
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock Disp.",
                DataPropertyName = "stock",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new System.Drawing.Font(dgvProductos.Font, System.Drawing.FontStyle.Bold) }
            });

            // 5. ID Categoría
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "idCategoria",
                HeaderText = "ID Cat.",
                DataPropertyName = "idCategoria",
                Width = 60,
                Visible = true // Lo pediste visible
            });

            // 6. Descripción de la Categoría
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Categoria",
                HeaderText = "Categoría",
                DataPropertyName = "nombreCategoria",
                Width = 150
            });
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            entProducto productoElegido = (entProducto)dgvProductos.CurrentRow.DataBoundItem;
            int cantidad = (int)numCantidad.Value;

            // Validación de Stock (Usa el stock visualmente restado)
            if (cantidad > productoElegido.stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo quedan {productoElegido.stock} unidades disponibles para agregar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Validación");
                return;
            }

            ItemSeleccionado = new entProformaItem();
            ItemSeleccionado.IdProducto = productoElegido.idProducto;
            ItemSeleccionado.NombreProducto = productoElegido.nombreProducto;
            ItemSeleccionado.PrecioUnitario = productoElegido.precioUnitario;
            ItemSeleccionado.Cantidad = cantidad;

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
