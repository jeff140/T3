using CapaEntidad;
using CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace proyecto_T3
{
    public partial class RegistrarProductos : Form
    {
        private bool esNuevo = false;
        public RegistrarProductos()
        {
            InitializeComponent();
            ListarProductos();
            CargarCategorias();
            ConfigurarControles();
            DeshabilitarControles();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void ConfigurarControles()
        {
            txtCodigoProducto.Enabled = false;
            txtStock.Minimum = 0;
            txtStock.Maximum = 999999;
        }

        private void CargarCategorias()
        {
            try
            {
                cmbCategoria.DataSource = logCategoria.Instancia.ListarCategoria();
                cmbCategoria.DisplayMember = "Descripcion";
                cmbCategoria.ValueMember = "idCategoria";
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }

        private void ListarProductos()
        {
            try
            {
                dgvProductos.DataSource = logProducto.Instancia.ListarProducto();
                dgvProductos.Columns["idProducto"].HeaderText = "Código";
                dgvProductos.Columns["nombreProducto"].HeaderText = "Producto";
                dgvProductos.Columns["precioUnitario"].HeaderText = "Precio";
                dgvProductos.Columns["stock"].HeaderText = "Stock";
                //dgvProductos.Columns["nombreCategoria"].HeaderText = "Categoría";
                dgvProductos.Columns["idCategoria"].Visible = false;
                dgvProductos.Columns["estProducto"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar productos: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtCodigoProducto.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Value = 0;
            cmbCategoria.SelectedIndex = -1;
            txtNombre.Focus();
        }

        private void HabilitarControles()
        {
            txtNombre.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            cmbCategoria.Enabled = true;
        }

        private void DeshabilitarControles()
        {
            txtNombre.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
            cmbCategoria.Enabled = false;
        }
        private void btnregistrar_Click(object sender, EventArgs e)
        {
          
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            LimpiarCampos();
            HabilitarControles();
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del producto");
                    return;
                }

                if (string.IsNullOrEmpty(txtPrecio.Text))
                {
                    MessageBox.Show("Ingrese el precio del producto");
                    return;
                }

                if (cmbCategoria.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una categoría");
                    return;
                }

                entProducto prod = new entProducto();
                prod.nombreProducto = txtNombre.Text.Trim();
                prod.unidadMedida = "UND"; // Puedes agregar un control para esto
                prod.precioUnitario = decimal.Parse(txtPrecio.Text);
                prod.stock = (int)txtStock.Value;
                prod.estProducto = true;
                prod.idCategoria= (int)cmbCategoria.SelectedValue;

                if (esNuevo)
                {
                    logProducto.Instancia.ListarProducto();
                    MessageBox.Show("Producto registrado exitosamente");
                }
                else
                {
                    prod.idProducto = int.Parse(txtCodigoProducto.Text);
                    logProducto.Instancia.EditarProducto(prod);
                    MessageBox.Show("Producto actualizado exitosamente");
                }

                ListarProductos();
                btnCancelar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoProducto.Text))
            {
                MessageBox.Show("Seleccione un producto para modificar");
                return;
            }

            esNuevo = false;
            HabilitarControles();
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtCodigoProducto.Text))
                {
                    MessageBox.Show("Seleccione un producto para eliminar");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este producto?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    entProducto prod = new entProducto();
                    prod.idProducto = int.Parse(txtCodigoProducto.Text);

                    logProducto.Instancia.DeshabilitarProducto(prod);
                    MessageBox.Show("Producto eliminado exitosamente");
                    ListarProductos();
                    btnCancelar_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            DeshabilitarControles();
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            esNuevo = false;
        }
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                    txtCodigoProducto.Text = row.Cells["idProducto"].Value.ToString();
                    txtNombre.Text = row.Cells["nombreProducto"].Value.ToString();
                    txtPrecio.Text = row.Cells["precioUnitario"].Value.ToString();
                    txtStock.Value = Convert.ToInt32(row.Cells["stock"].Value);
                    cmbCategoria.SelectedValue = row.Cells["idCategoria"].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBuscar.Text))
                {
                    ListarProductos();
                }
                else
                {
                    var listaProductos = logProducto.Instancia.ListarProducto();
                    var productosFiltrados = listaProductos.Where(p =>
                        p.nombreProducto.ToLower().Contains(txtBuscar.Text.ToLower())
                    //|| p.nombreCategoria.ToLower().Contains(txtBuscar.Text.ToLower())
                    ).ToList();

                    dgvProductos.DataSource = productosFiltrados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}