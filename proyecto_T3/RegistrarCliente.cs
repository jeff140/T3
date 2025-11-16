using CapaEntidad;
using CapaLogica;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace proyecto_T3
{
    public partial class RegistrarCliente : Form
    {
        private int idClienteSeleccionado = 0;

        public RegistrarCliente()
        {
            InitializeComponent();
            ListarCliente();
            ConfigurarComboBoxDepartamento();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuraciones adicionales al cargar el formulario
            ConfigurarDataGridView();
        }

        #region Métodos de Configuración

        /// <summary>
        /// Configura el ComboBox de Departamentos
        /// </summary>
        private void ConfigurarComboBoxDepartamento()
        {
            cmbDepartamento.Items.Clear();
            cmbDepartamento.Items.Add("Seleccione un departamento");
            cmbDepartamento.Items.Add("1 - Lima");
            cmbDepartamento.Items.Add("2 - Arequipa");
            cmbDepartamento.Items.Add("3 - Cusco");
            cmbDepartamento.Items.Add("4 - La Libertad");
            cmbDepartamento.Items.Add("5 - Piura");
            cmbDepartamento.Items.Add("6 - Lambayeque");
            cmbDepartamento.Items.Add("7 - Junín");
            cmbDepartamento.Items.Add("8 - Puno");
            cmbDepartamento.Items.Add("9 - Cajamarca");
            cmbDepartamento.Items.Add("10 - Ica");
            cmbDepartamento.SelectedIndex = 0;
        }

        /// <summary>
        /// Configura la apariencia del DataGridView
        /// </summary>
        private void ConfigurarDataGridView()
        {
            if (dgvCliente.Columns.Count > 0)
            {
                dgvCliente.Columns["idCliente"].HeaderText = "ID";
                dgvCliente.Columns["DniRucCli"].HeaderText = "DNI/RUC";
                dgvCliente.Columns["Nom_razonSocial"].HeaderText = "Nombre/Razón Social";
                dgvCliente.Columns["fecRegCliente"].HeaderText = "Fecha Registro";
                dgvCliente.Columns["idDepartamento"].HeaderText = "Departamento";
                dgvCliente.Columns["Telefono"].HeaderText = "Teléfono";

                // Opcional: Ocultar columna Estado si existe
                if (dgvCliente.Columns.Contains("Estado"))
                {
                    dgvCliente.Columns["Estado"].Visible = false;
                }

                dgvCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCliente.MultiSelect = false;
                dgvCliente.ReadOnly = true;
            }
        }

        #endregion

        #region Métodos Principales

        /// <summary>
        /// Lista todos los clientes en el DataGridView
        /// </summary>
        public void ListarCliente()
        {
            try
            {
                dgvCliente.DataSource = logCliente.Instancia.ListarCliente();
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar clientes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia todos los controles del formulario
        /// </summary>
        private void LimpiarVariables()
        {
            txtDniRuc.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            cmbDepartamento.SelectedIndex = 0;
            dtpFechaRegistro.Value = DateTime.Now;
            dtpFechaRegistro.Checked = false;
            idClienteSeleccionado = 0;
            txtDniRuc.Focus();
        }

        /// <summary>
        /// Valida que todos los campos requeridos estén completos
        /// </summary>
        private bool ValidarCampos()
        {
            // Validar DNI/RUC
            if (string.IsNullOrWhiteSpace(txtDniRuc.Text))
            {
                MessageBox.Show("El DNI/RUC es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniRuc.Focus();
                return false;
            }

            if (txtDniRuc.Text.Trim().Length < 8)
            {
                MessageBox.Show("El DNI/RUC debe tener al menos 8 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDniRuc.Focus();
                return false;
            }

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre o razón social es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validar Departamento
            if (cmbDepartamento.SelectedIndex <= 0)
            {
                MessageBox.Show("Debe seleccionar un departamento.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartamento.Focus();
                return false;
            }

            // Validar Fecha
            if (!dtpFechaRegistro.Checked)
            {
                MessageBox.Show("Debe seleccionar una fecha de registro.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaRegistro.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Extrae el ID del departamento del texto del ComboBox
        /// </summary>
        private int ObtenerIdDepartamento()
        {
            string textoSeleccionado = cmbDepartamento.Text;
            if (string.IsNullOrWhiteSpace(textoSeleccionado) ||
                textoSeleccionado == "Seleccione un departamento")
            {
                return 0;
            }

            // Extraer el número antes del guión (ej: "1 - Lima" -> 1)
            string[] partes = textoSeleccionado.Split('-');
            if (partes.Length > 0 && int.TryParse(partes[0].Trim(), out int idDepto))
            {
                return idDepto;
            }

            return 0;
        }

        #endregion

        #region Eventos del DataGridView

        /// <summary>
        /// Maneja el doble clic en una fila del DataGridView para cargar los datos
        /// </summary>

        #endregion

        #region Eventos de Botones

        /// <summary>
        /// Busca datos por DNI usando la API
        /// </summary>
        private async void btnMostrar_Click(object sender, EventArgs e)
        {
            string DNI = txtDniRuc.Text.Trim();

            if (string.IsNullOrWhiteSpace(DNI))
            {
                MessageBox.Show("Por favor ingrese un DNI.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DNI.Length != 8 || !DNI.All(char.IsDigit))
            {
                MessageBox.Show("El DNI debe tener exactamente 8 dígitos numéricos.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await BuscarPorDNI(DNI);
        }

        /// <summary>
        /// Registra un nuevo cliente
        /// </summary>
        private void btnRegistar_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (!ValidarCampos())
            {
                return;
            }

            // Confirmar registro
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de registrar este cliente?",
                "Confirmar Registro",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Crear entidad Cliente
                entCliente c = new entCliente();
                c.DniRucCli = txtDniRuc.Text.Trim();
                c.Nom_razonSocial = txtNombre.Text.Trim();
                c.Telefono = txtTelefono.Text.Trim();
                c.idDepartamento = ObtenerIdDepartamento();
                c.fecRegCliente = dtpFechaRegistro.Value;

                // Llamar a la Capa Lógica
                logCliente.Instancia.InsertaCliente(c);

                MessageBox.Show("Cliente registrado exitosamente.",
                    "Registro Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar y refrescar
                LimpiarVariables();
                ListarCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Modifica un cliente existente
        /// </summary>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un cliente
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente de la lista (doble clic) para modificar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar campos
            if (!ValidarCampos())
            {
                return;
            }

            // Confirmar modificación
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de modificar este cliente?",
                "Confirmar Modificación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Crear entidad Cliente
                entCliente c = new entCliente();
                c.idCliente = idClienteSeleccionado;
                c.DniRucCli = txtDniRuc.Text.Trim();
                c.Nom_razonSocial = txtNombre.Text.Trim();
                c.Telefono = txtTelefono.Text.Trim();
                c.idDepartamento = ObtenerIdDepartamento();
                c.fecRegCliente = dtpFechaRegistro.Value;

                // Llamar a la Capa Lógica
                logCliente.Instancia.EditaCliente(c);

                MessageBox.Show("Cliente modificado exitosamente.",
                    "Modificación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar y refrescar
                LimpiarVariables();
                ListarCliente();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente de la lista (doble clic) para eliminar.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar este cliente?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }
            try
            {
                entCliente c = new entCliente();
                c.idCliente = idClienteSeleccionado;
                // Llamar a la Capa Lógica
                logCliente.Instancia.EliminarCliente(c);

                ListarCliente();
                idClienteSeleccionado = 0;
                LimpiarVariables();

                MessageBox.Show("Cliente eliminado exitosamente.",
                    "Eliminación Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Limpia todos los campos del formulario
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarVariables();
        }

        #endregion

        #region API de Consulta DNI

        /// <summary>
        /// Busca información de una persona por su DNI usando la API
        /// </summary>
        private async Task BuscarPorDNI(string dni)
        {
            // Deshabilitar botón mientras busca
            btnMostrar.Enabled = false;
            btnMostrar.Text = "Buscando...";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // IMPORTANTE: Reemplaza "YOUR_TOKEN_HERE" con tu token real
                    string url = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImFuZ2llcHJhZG8xMjJAZ21haWwuY29tIn0.Z0lR-bf-SshTzYFAcaYr4BPoTUTpNNZsHn-0W5OyG3I";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(responseBody);

                    if (data.nombres != null)
                    {
                        string nombreCompleto = $"{data.nombres} {data.apellidoPaterno} {data.apellidoMaterno}";
                        txtNombre.Text = nombreCompleto.Trim();

                        MessageBox.Show("Datos encontrados correctamente.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos para el DNI ingresado.",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("No se pudo conectar con el servicio de consulta de DNI.\n" +
                    "Verifique su conexión a internet o ingrese los datos manualmente.",
                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar el DNI: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar botón
                btnMostrar.Enabled = true;
                btnMostrar.Text = "Buscar por DNI";
            }
        }

        #endregion


        private void dgvCliente_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Verifica que se haya hecho doble clic en una fila de datos válida
            if (e.RowIndex < 0) return;

            dgvCliente.Columns[0].Name = "idCliente";
            dgvCliente.Columns[1].Name = "DniRucCli";
            dgvCliente.Columns[2].Name = "Nom_razonSocial";
            dgvCliente.Columns[5].Name = "Telefono";
            dgvCliente.Columns[4].Name = "idDepartamento";
            dgvCliente.Columns[3].Name = "fecRegCliente";

            try
            {
                DataGridViewRow filaActual = dgvCliente.Rows[e.RowIndex];

                // Obtener ID del cliente
                if (filaActual.Cells["idCliente"].Value != null &&
                    filaActual.Cells["idCliente"].Value != DBNull.Value)
                {
                    idClienteSeleccionado = Convert.ToInt32(filaActual.Cells["idCliente"].Value);
                }
                else
                {
                    idClienteSeleccionado = 0;
                    return;
                }

                // Cargar DNI/RUC
                txtDniRuc.Text = filaActual.Cells["DniRucCli"].Value?.ToString() ?? "";

                // Cargar Nombre
                txtNombre.Text = filaActual.Cells["Nom_razonSocial"].Value?.ToString() ?? "";

                // Cargar Teléfono
                txtTelefono.Text = filaActual.Cells["Telefono"].Value?.ToString() ?? "";

                // Cargar Departamento
                if (filaActual.Cells["idDepartamento"].Value != null &&
                    filaActual.Cells["idDepartamento"].Value != DBNull.Value)
                {
                    int idDepto = Convert.ToInt32(filaActual.Cells["idDepartamento"].Value);

                    // Buscar en el ComboBox el item que contiene este ID
                    for (int i = 0; i < cmbDepartamento.Items.Count; i++)
                    {
                        string item = cmbDepartamento.Items[i].ToString();
                        if (item.StartsWith(idDepto.ToString() + " -"))
                        {
                            cmbDepartamento.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Cargar Fecha
                object valorFecha = filaActual.Cells["fecRegCliente"].Value;

                if (valorFecha != null && valorFecha != DBNull.Value)
                {
                    DateTime fecha;
                    if (DateTime.TryParse(valorFecha.ToString(), out fecha))
                    {
                        dtpFechaRegistro.Value = fecha;
                        dtpFechaRegistro.Checked = true;
                    }
                    else
                    {
                        // Valor no válido
                        dtpFechaRegistro.Value = DateTime.Now;
                        dtpFechaRegistro.Checked = false;
                    }
                }
                else
                {
                    dtpFechaRegistro.Value = DateTime.Now;
                    dtpFechaRegistro.Checked = false;
                }


                MessageBox.Show("Cliente cargado. Puede modificar los datos y presionar 'Modificar'.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos para edición: " + ex.Message,
                    "Error de Mapeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnOrdenCompra_Click(object sender, EventArgs e)
        {
            Guia_Remision nuevoFormulario = new Guia_Remision();
            nuevoFormulario.Show();
        }
        private void btnProducto_Click(object sender, EventArgs e)
        {
            RegistrarProductos nuevoFormulario = new RegistrarProductos();
            nuevoFormulario.Show();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            FormProveedor formularioProveedor = new FormProveedor();

            formularioProveedor.Show();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            RegistrarCategoria nuevoFormulario = new RegistrarCategoria();
            nuevoFormulario.Show();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            RegistrarCliente nuevoFormulario = new RegistrarCliente();
            nuevoFormulario.Show();
        }

        private void btnProforma_Click(object sender, EventArgs e)
        {
            Proforma nuevoFormulario = new Proforma();
            nuevoFormulario.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            ventaProducto nuevoFormulario = new ventaProducto();
            nuevoFormulario.Show();
        }
    }
}