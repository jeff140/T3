using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;  // Asegúrate de tener esta referencia
using CapaEntidad; // Asegúrate de tener esta referencia

namespace proyecto_T3 // O el namespace que tenga tu proyecto CapaPresentacionT3
{
    public partial class FormProveedor : Form

    {

        private List<entProveedor> listaProveedoresCompleta;

        private int idProveedorSeleccionado = 0;
        public FormProveedor()
        {
            InitializeComponent();
        }

        // --- MÉTODOS REUTILIZABLES ---

        /// <summary>
        /// Este método llama a la capa lógica para llenar la tabla
        /// </summary>
        private void ListarProveedores()
        {
            try
            {
                // 1. Obtenemos la lista COMPLETA de la capa lógica
                listaProveedoresCompleta = logProveedor.Instancia.ListarProveedores();

                // 2. Asignamos esta lista completa al DataGridView
                dgvProveedores.DataSource = listaProveedoresCompleta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar proveedores: " + ex.Message);
            }
        }

        /// <summary>
        /// Este método deja todos los campos de texto en blanco
        /// </summary>
        private void limpiarCampos()
        {
            txtRazonSocial.Text = "";
            txtRuc.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";

            idProveedorSeleccionado = 0; // Reiniciar el ID

            // --- Controlar los botones ---
            btnModificar.Enabled = false;  // Deshabilitar Modificar
            btnEliminar.Enabled = false;   // Deshabilitar Eliminar
            btnRegistrar.Enabled = true;  // Habilitar Registrar

            // Habilitar RUC
            txtRuc.Enabled = true;

            txtRazonSocial.Focus();
        }

        /// <summary>
        /// Valida un RUC peruano usando el algoritmo de Módulo 11.
        /// </summary>
        /// <param name="ruc">El RUC de 11 dígitos como string.</param>
        /// <returns>True si es un RUC válido, False si no lo es.</returns>
        private bool ValidarRuc(string ruc)
        {
            // 1. Validar longitud
            if (ruc.Length != 11)
                return false;

            // 2. Validar prefijo (Debe empezar con 10, 15, 17 o 20)
            string prefijo = ruc.Substring(0, 2);
            if (prefijo != "10" && prefijo != "15" && prefijo != "17" && prefijo != "20")
                return false;

            // 3. Validar Dígito Verificador (Módulo 11)
            int[] factores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;

            try
            {
                // Sumamos los 10 primeros dígitos multiplicados por los factores
                for (int i = 0; i < 10; i++)
                {
                    // Convertimos el char a int
                    int digito = int.Parse(ruc[i].ToString());
                    suma += digito * factores[i];
                }

                int residuo = suma % 11;
                int digitoVerificadorCalculado = 11 - residuo;

                // Si el resultado es 10, el dígito es 0. Si es 11, es 1.
                if (digitoVerificadorCalculado == 10)
                    digitoVerificadorCalculado = 0;
                if (digitoVerificadorCalculado == 11)
                    digitoVerificadorCalculado = 1;

                // Comparamos el dígito calculado con el último dígito del RUC
                int digitoVerificadorReal = int.Parse(ruc[10].ToString());

                return digitoVerificadorCalculado == digitoVerificadorReal;
            }
            catch (Exception)
            {
                // Si ocurre un error (ej. RUC no es numérico), no es válido
                return false;
            }
        }


        // --- EVENTOS DE LOS CONTROLES ---

        /// <summary>
        /// Este código se ejecuta UNA VEZ cuando el formulario se abre
        /// </summary>
        private void FormProveedor_Load(object sender, EventArgs e)
        {
            // Llamamos al método para llenar la tabla
            ListarProveedores();
        }

        /// <summary>
        /// Este es el evento de CLIC para tu botón "Registrar"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIÓN DE CAMPOS VACÍOS ---
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) ||
                string.IsNullOrWhiteSpace(txtRuc.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Debe completar todos los campos.", "Error: Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. VALIDACIÓN DE FORMATO PERÚ (Longitud) ---
            if (txtRuc.Text.Length != 11)
            {
                MessageBox.Show("El RUC debe tener 11 dígitos.", "Error en RUC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRuc.Focus();
                return;
            }
            if (txtTelefono.Text.Length != 9)
            {
                MessageBox.Show("El Teléfono (celular) debe tener 9 dígitos.", "Error en Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return;
            }

            // --- 3. VALIDACIÓN DE NÚMERO (Lógica de Perú) ---

            // Validar Teléfono (Debe empezar con 9)
            if (!txtTelefono.Text.StartsWith("9"))
            {
                MessageBox.Show("El número de celular debe empezar con 9.", "Error en Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return;
            }

            // Validar RUC (Usando nuestra nueva función)
            if (!ValidarRuc(txtRuc.Text))
            {
                MessageBox.Show("El número de RUC no es válido.\nVerifique el prefijo (10 o 20) y el dígito verificador.", "Error en RUC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRuc.Focus();
                return;
            }

            // --- 4. INTENTO DE REGISTRO ---
            // Si llegamos aquí, ¡todos los datos son válidos!
            try
            {
                entProveedor prov = new entProveedor();
                prov.razonSocial = txtRazonSocial.Text.Trim();
                prov.ruc = txtRuc.Text.Trim();
                prov.telefono = txtTelefono.Text.Trim();
                prov.direccion = txtDireccion.Text.Trim();

                logProveedor.Instancia.InsertarProveedor(prov);

                MessageBox.Show("¡Proveedor registrado exitosamente!");
                ListarProveedores();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar proveedor: " + ex.Message);
            }
        }

        // --- EVENTOS PARA VALIDAR ENTRADA DE TEXTO ---

        /// <summary>
        /// Evento para el RUC: Solo permite números y teclas de control (borrar)
        /// </summary>
        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si NO es un número Y NO es una tecla de control (como borrar)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // "Manejamos" el evento, es decir, bloqueamos la tecla
            }
        }

        /// <summary>
        /// Evento para el Teléfono: Lo mismo, solo números
        /// </summary>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // --- Eventos vacíos que tenías en tu archivo ---
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtRuc_TextChanged(object sender, EventArgs e) { }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegurarse de que el clic no fue en el encabezado
            if (e.RowIndex >= 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow fila = dgvProveedores.Rows[e.RowIndex];

                // Cargar los datos de la fila a los TextBox
                // OJO: Los nombres de columna deben ser EXACTOS a como vienen de tu SP
                txtRazonSocial.Text = fila.Cells["razonSocial"].Value.ToString();
                txtRuc.Text = fila.Cells["ruc"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();

                // Almacenar el ID del proveedor seleccionado
                idProveedorSeleccionado = Convert.ToInt32(fila.Cells["idProveedor"].Value);

                // --- Controlar los botones ---
                btnModificar.Enabled = true;  // Habilitar Modificar
                btnEliminar.Enabled = true;   // Habilitar Eliminar
                btnRegistrar.Enabled = false; // Deshabilitar Registrar

                // Opcional: Deshabilitar RUC (generalmente no se puede modificar)
                txtRuc.Enabled = false;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIÓN DE SELECCIÓN ---
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor de la lista antes de modificar.", "Error: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. VALIDACIÓN DE CAMPOS VACÍOS ---
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Debe completar todos los campos (excepto RUC).", "Error: Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 3. VALIDACIÓN DE TELÉFONO (9 dígitos y empieza con 9) ---
            if (txtTelefono.Text.Length != 9 || !txtTelefono.Text.StartsWith("9"))
            {
                MessageBox.Show("El Teléfono (celular) debe tener 9 dígitos y empezar con 9.", "Error en Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return;
            }

            // --- 4. INTENTO DE ACTUALIZACIÓN ---
            try
            {
                entProveedor prov = new entProveedor();

                // ¡Usamos el ID que guardamos!
                prov.idProveedor = idProveedorSeleccionado;

                prov.razonSocial = txtRazonSocial.Text.Trim();
                prov.telefono = txtTelefono.Text.Trim();
                prov.direccion = txtDireccion.Text.Trim();
                // No pasamos el RUC porque nuestro SP no lo actualiza

                logProveedor.Instancia.ActualizarProveedor(prov);

                MessageBox.Show("¡Proveedor modificado exitosamente!");
                ListarProveedores();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar proveedor: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIÓN DE SELECCIÓN ---
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor de la lista antes de eliminar.",
                                "Error: Sin selección",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // --- 2. PREGUNTA DE CONFIRMACIÓN (¡MUY IMPORTANTE!) ---
            DialogResult resultado = MessageBox.Show("¿Está realmente seguro de que desea eliminar (deshabilitar) este proveedor?",
                                                   "Confirmar Eliminación",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            // Si el usuario hace clic en "NO"
            if (resultado == DialogResult.No)
            {
                return; // Detiene la ejecución aquí
            }

            // --- 3. INTENTO DE ELIMINACIÓN ---
            // Si el usuario hizo clic en "SÍ"
            try
            {
                // Llamamos a la lógica pasándole el ID que guardamos
                logProveedor.Instancia.EliminarProveedor(idProveedorSeleccionado);

                MessageBox.Show("Proveedor eliminado (deshabilitado) exitosamente.");

                // Refrescamos la lista y limpiamos los campos
                ListarProveedores();
                limpiarCampos();
            }
            catch (Exception ex)
            {
                // Esto podría pasar si el proveedor tiene Órdenes de Compra activas
                // y la base de datos tiene una restricción (Foreign Key) que impide borrar.
                MessageBox.Show("Error al eliminar proveedor.\nEs posible que esté en uso en otros registros.\n\nDetalle: " + ex.Message,
                                "Error de Base de Datos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el texto de búsqueda y convertirlo a minúsculas
                string textoBusqueda = txtBuscar.Text.Trim().ToLower();

                // 1. Si el cuadro de búsqueda está vacío...
                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    // ...mostrar la lista completa original
                    dgvProveedores.DataSource = listaProveedoresCompleta;
                }
                else
                {
                    // 2. Si hay texto, filtrar la "Lista Maestra" usando LINQ
                    List<entProveedor> listaFiltrada = listaProveedoresCompleta
                        .Where(prov =>
                            prov.razonSocial.ToLower().Contains(textoBusqueda) || // Buscar por Razón Social
                            prov.ruc.Contains(textoBusqueda)                      // Buscar por RUC
                        )
                        .ToList(); // Convertir el resultado de nuevo a una Lista

                    // 3. Asignar la lista FILTRADA a la tabla
                    dgvProveedores.DataSource = listaFiltrada;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Crear y abrir la nueva ventana como un diálogo (bloquea la ventana de atrás)
            frmBusquedaProveedor ventanaBusqueda = new frmBusquedaProveedor();
            ventanaBusqueda.ShowDialog();
        }
    }
}