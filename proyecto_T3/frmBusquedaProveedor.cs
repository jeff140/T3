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
    public partial class frmBusquedaProveedor : Form
    {
        public frmBusquedaProveedor()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmBusquedaProveedor_Load(object sender, EventArgs e)
        {
            // Seleccionar "Activo" por defecto para evitar errores
            cboEstado.SelectedIndex = 0;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            // Determinar si buscamos true (Activo) o false (Inactivo)
            bool estadoBuscado = cboEstado.Text == "Activo";

            logProveedor objLogica = new logProveedor();
            List<entProveedor> lista = objLogica.ListarPorEstado(estadoBuscado);

            dgvResultados.DataSource = lista;
        }
    }
}
