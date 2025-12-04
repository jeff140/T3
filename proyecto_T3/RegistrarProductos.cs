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
        // 🚨 VARIABLE 'esNuevo' ELIMINADA (Línea 16 corregida)

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
            txtCodigoProducto.Enabled = false;
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
            txtCodigoProducto.Enabled = false;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // esNuevo = true; <-- ELIMINADO
            LimpiarCampos();
            HabilitarControles();
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            txtCodigoProducto.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                entProducto prod = new entProducto();
                int idProducto = 0;

                // 1. DETERMINAR SI ES MODIFICACIÓN Y LEER ID
                bool esModificacion = int.TryParse(txtCodigoProducto.Text.Trim(), out idProducto) && idProducto > 0;

                // 2. SOLUCIÓN NULLREFERENCEEXCEPTION 
                if (cmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar una Categoría válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. CARGAR EL RESTO DE PARÁMETROS
                if (esModificacion)
                {
                    prod.idProducto = idProducto;
                }

                prod.unidadMedida = "UND";

                prod.nombreProducto = txtNombre.Text.Trim();

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                {
                    MessageBox.Show("El precio unitario no es un valor numérico válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                prod.precioUnitario = precio;

                prod.stock = (int)txtStock.Value;
                prod.idCategoria = (int)cmbCategoria.SelectedValue;
                prod.estProducto = true;

                // 4. LÓGICA DE EDICIÓN O INSERCIÓN 
                if (esModificacion)
                {
                    logProducto.Instancia.EditarProducto(prod);
                    MessageBox.Show("Producto modificado con éxito.");
                }
                else
                {
                    logProducto.Instancia.InsertarProducto(prod);
                    MessageBox.Show("Producto registrado con éxito.");
                }

                // 5. FINALIZAR
                ListarProductos();
                btnCancelar_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar/modificar: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigoProducto.Text))
            {
                MessageBox.Show("Seleccione un producto para modificar");
                return;
            }

            // esNuevo = false; <-- ELIMINADO
            HabilitarControles();
            txtCodigoProducto.Enabled = false;

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
                    MessageBox.Show("Seleccione un producto para eliminar/deshabilitar.", "Advertencia");
                    return;
                }

                DialogResult r = MessageBox.Show("¿Está seguro que desea deshabilitar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    entProducto prod = new entProducto();
                    prod.idProducto = int.Parse(txtCodigoProducto.Text.Trim());

                    logProducto.Instancia.DeshabilitarProducto(prod);

                    MessageBox.Show("Producto deshabilitado exitosamente.");

                    ListarProductos();
                    btnCancelar_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al deshabilitar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // esNuevo = false; <-- ELIMINADO
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
                    ).ToList();

                    dgvProductos.DataSource = productosFiltrados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCodigoProducto_TextChanged(object sender, EventArgs e) { }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                if (string.IsNullOrEmpty(texto))
                {
                    ListarProductos();
                    return;
                }

                List<entProducto> listaFiltrada = logProducto.Instancia.BuscarProducto(texto);

                dgvProductos.DataSource = listaFiltrada;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}