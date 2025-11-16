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
    public partial class Guia_Remision : Form
    {
        private int idGuiaSeleccionada = 0;
        public Guia_Remision()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            ListarGuiasRemision();
            LimpiarCampos();
            GenerarNumeroGuia();
        }
        private void ConfigurarDataGridView()
        {
            dgvGuiasRemision.AutoGenerateColumns = false;
            dgvGuiasRemision.Columns.Clear();
            dgvGuiasRemision.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGuiasRemision.MultiSelect = false;
            dgvGuiasRemision.ReadOnly = true;
            dgvGuiasRemision.AllowUserToAddRows = false;
            dgvGuiasRemision.AllowUserToDeleteRows = false;
            dgvGuiasRemision.BackgroundColor = Color.White;
            dgvGuiasRemision.BorderStyle = BorderStyle.Fixed3D;

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "idGuiaRemision",
                HeaderText = "ID",
                Name = "colID",
                Width = 50
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "numeroGuia",
                HeaderText = "Nº Guía",
                Name = "colNumeroGuia",
                Width = 120
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "fechaEmision",
                HeaderText = "Fecha Emisión",
                Name = "colFechaEmision",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "fechaTransporte",
                HeaderText = "Fecha Transporte",
                Name = "colFechaTransporte",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "idVenta",
                HeaderText = "ID Venta",
                Name = "colIdVenta",
                Width = 80
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "idCliente",
                HeaderText = "ID Cliente",
                Name = "colIdCliente",
                Width = 80
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "direccionLlegada",
                HeaderText = "Dirección Llegada",
                Name = "colDireccionLlegada",
                Width = 200
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "motivoTraslado",
                HeaderText = "Motivo",
                Name = "colMotivo",
                Width = 150
            });

            dgvGuiasRemision.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "estado",
                HeaderText = "Activo",
                Name = "colEstado",
                Width = 60
            });
        }
        private void GenerarNumeroGuia()
        {
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            Random rnd = new Random();
            int correlativo = rnd.Next(1, 9999);
            txtNumeroGuia.Text = $"GR-{fecha}-{correlativo:D4}";
        }
        private void ListarGuiasRemision()
        {
            try
            {
                List<entGuiaRemision> lista = logGuiaRemision.Instancia.ListarGuiaRemision();
                dgvGuiasRemision.DataSource = lista;

                if (lista.Count == 0)
                {
                    MessageBox.Show("No hay guías de remisión registradas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar guías de remisión: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvGuiasRemision_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow fila = dgvGuiasRemision.Rows[e.RowIndex];

                    idGuiaSeleccionada = Convert.ToInt32(fila.Cells["colID"].Value);
                    txtNumeroGuia.Text = fila.Cells["colNumeroGuia"].Value.ToString();
                    dtpFechaEmision.Value = Convert.ToDateTime(fila.Cells["colFechaEmision"].Value);
                    dtpFechaTransporte.Value = Convert.ToDateTime(fila.Cells["colFechaTransporte"].Value);
                    txtIdVenta.Text = fila.Cells["colIdVenta"].Value.ToString();
                    txtIdCliente.Text = fila.Cells["colIdCliente"].Value.ToString();
                    txtNumeroComprobante.Text = "";
                    txtDireccionPartida.Text = "";
                    txtDireccionLlegada.Text = fila.Cells["colDireccionLlegada"].Value.ToString();
                    txtMotivoTraslado.Text = fila.Cells["colMotivo"].Value.ToString();
                    txtTransporte.Text = "";
                    txtPlacaVehiculo.Text = "";

                    btnGuardar.Text = "Actualizar";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                entGuiaRemision guia = new entGuiaRemision
                {
                    numeroGuia = txtNumeroGuia.Text.Trim(),
                    fechaEmision = dtpFechaEmision.Value,
                    fechaTransporte = dtpFechaTransporte.Value,
                    idVenta = Convert.ToInt32(txtIdVenta.Text),
                    idCliente = Convert.ToInt32(txtIdCliente.Text),
                    numeroComprobante = txtNumeroComprobante.Text.Trim(),
                    direccionPartida = txtDireccionPartida.Text.Trim(),
                    direccionLlegada = txtDireccionLlegada.Text.Trim(),
                    motivoTraslado = txtMotivoTraslado.Text.Trim(),
                    transporte = txtTransporte.Text.Trim(),
                    placaVehiculo = txtPlacaVehiculo.Text.Trim(),
                    estado = true
                };

                bool resultado = false;
                string mensaje = "";

                if (btnGuardar.Text == "Guardar")
                {
                    int idGenerado = logGuiaRemision.Instancia.InsertarGuiaRemision(guia);
                    resultado = idGenerado > 0;
                    mensaje = resultado ? "Guía de remisión registrada correctamente." :
                        "No se pudo registrar la guía de remisión.";
                }
                else
                {
                    guia.idGuiaRemision = idGuiaSeleccionada;
                    resultado = logGuiaRemision.Instancia.EditarGuiaRemision(guia);
                    mensaje = resultado ? "Guía de remisión actualizada correctamente." :
                        "No se pudo actualizar la guía de remisión.";
                }

                if (resultado)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarGuiasRemision();
                    LimpiarCampos();
                    GenerarNumeroGuia();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            GenerarNumeroGuia();
            btnGuardar.Text = "Guardar";
            idGuiaSeleccionada = 0;
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (idGuiaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una guía de remisión para deshabilitar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro de deshabilitar esta guía de remisión?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    bool resultado = logGuiaRemision.Instancia.DeshabilitarGuiaRemision(idGuiaSeleccionada);

                    if (resultado)
                    {
                        MessageBox.Show("Guía de remisión deshabilitada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarGuiasRemision();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo deshabilitar la guía de remisión.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al deshabilitar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
               "¿Desea cancelar la operación actual?",
               "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                LimpiarCampos();
                GenerarNumeroGuia();
                btnGuardar.Text = "Guardar";
                idGuiaSeleccionada = 0;
            }
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroGuia.Text))
            {
                MessageBox.Show("Ingrese el número de guía.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroGuia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIdVenta.Text) || !int.TryParse(txtIdVenta.Text, out _))
            {
                MessageBox.Show("Ingrese un ID de venta válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdVenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIdCliente.Text) || !int.TryParse(txtIdCliente.Text, out _))
            {
                MessageBox.Show("Ingrese un ID de cliente válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdCliente.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccionLlegada.Text))
            {
                MessageBox.Show("Ingrese la dirección de llegada.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccionLlegada.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMotivoTraslado.Text))
            {
                MessageBox.Show("Ingrese el motivo de traslado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotivoTraslado.Focus();
                return false;
            }

            if (dtpFechaTransporte.Value < dtpFechaEmision.Value)
            {
                MessageBox.Show("La fecha de transporte no puede ser anterior a la fecha de emisión.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaTransporte.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            idGuiaSeleccionada = 0;
            txtNumeroGuia.Clear();
            dtpFechaEmision.Value = DateTime.Now;
            dtpFechaTransporte.Value = DateTime.Now;
            txtIdVenta.Clear();
            txtIdCliente.Clear();
            txtNumeroComprobante.Clear();
            txtDireccionPartida.Clear();
            txtDireccionLlegada.Clear();
            txtMotivoTraslado.Clear();
            txtTransporte.Clear();
            txtPlacaVehiculo.Clear();
            btnGuardar.Text = "Guardar";
        }

    }
}

