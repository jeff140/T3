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
    public partial class BuscarClienteSimple : Form
    {

        public entCliente ClienteSeleccionado { get; private set; }
        private List<entCliente> listaClientes;
        public BuscarClienteSimple()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void BuscarClienteSimple_Load(object sender, EventArgs e)
        {
            CargarClientes();
            ConfigurarGrilla();
        }

        private void CargarClientes()
        {
            try
            {
                // Usamos logCliente para traer la lista con DNI
                listaClientes = logCliente.Instancia.ListarCliente();
                dgvClientes.DataSource = listaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Clear();

            // 1. Columna DNI (Busca la propiedad DniRucCli en entCliente)
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DniRucCli",
                HeaderText = "DNI / RUC",
                Width = 100
            });

            // 2. Columna Nombre (Busca la propiedad Nom_razonSocial en entCliente)
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom_razonSocial",
                HeaderText = "Razón Social / Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Ocultamos columnas que no necesitamos ver pero que están en el objeto
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "idCliente", Visible = false });
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                dgvClientes.DataSource = listaClientes;
            }
            else
            {
                // Filtra si el DNI empieza con el texto O si el Nombre lo contiene
                var filtrados = listaClientes.Where(c =>
                    c.DniRucCli.StartsWith(filtro) ||
                    c.Nom_razonSocial.ToLower().Contains(filtro)
                ).ToList();

                dgvClientes.DataSource = filtrados;
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow != null)
            {
                ClienteSeleccionado = (entCliente)dgvClientes.CurrentRow.DataBoundItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
