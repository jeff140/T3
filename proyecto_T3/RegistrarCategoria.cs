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
    public partial class RegistrarCategoria : Form
    {
        public RegistrarCategoria()
        {
            InitializeComponent();
            ListarCategorias();
            LimpiarCampos();
        }


        private void ListarCategorias()
        {
            try
            {
                dtgCategoria.DataSource = logCategoria.Instancia.ListarCategoria();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar categorías: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtidCategoria.Clear();
            txtDescripcion.Clear();
            checkEstado.Checked = true;
            txtDescripcion.Focus();
        }
        private void btnregistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese la descripción de la categoría");
                    return;
                }

                entCategoria cat = new entCategoria();
                cat.Descripcion = txtDescripcion.Text.Trim();
                cat.estCategoria = checkEstado.Checked;

                logCategoria.Instancia.InsertarCategoria(cat);
                MessageBox.Show("Categoría registrada exitosamente");
                ListarCategorias();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtidCategoria.Text))
                {
                    MessageBox.Show("Seleccione una categoría para editar");
                    return;
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese la descripción de la categoría");
                    return;
                }

                entCategoria cat = new entCategoria();
                cat.idCategoria = int.Parse(txtidCategoria.Text);
                cat.Descripcion = txtDescripcion.Text.Trim();
                cat.estCategoria = checkEstado.Checked;

                logCategoria.Instancia.EditarCategoria(cat);
                MessageBox.Show("Categoría actualizada exitosamente");
                ListarCategorias();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        private void btndeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtidCategoria.Text))
                {
                    MessageBox.Show("Seleccione una categoría para deshabilitar");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de deshabilitar esta categoría?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    entCategoria cat = new entCategoria();
                    cat.idCategoria = int.Parse(txtidCategoria.Text);

                    logCategoria.Instancia.DeshabilitarCategoria(cat);
                    MessageBox.Show("Categoría deshabilitada exitosamente");
                    ListarCategorias();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al deshabilitar: " + ex.Message);
            }
        }

        private void dtgCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dtgCategoria.Rows[e.RowIndex];
                    txtidCategoria.Text = row.Cells["idCategoria"].Value.ToString();
                    txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                    checkEstado.Checked = Convert.ToBoolean(row.Cells["estCategoria"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar: " + ex.Message);
            }
        }
    }
}