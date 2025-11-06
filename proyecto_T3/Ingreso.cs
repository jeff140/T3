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
    public partial class Ingreso : Form
    {
        public Ingreso()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            String Usuario = txtUsuario.Text.Trim();
            String Contraseña = txtContraseña.Text.Trim();
            if (Usuario == "Admin" && Contraseña == "Admin123")
            {
                RegistrarCliente nuevoFormulario = new RegistrarCliente();
                nuevoFormulario.Show();

            }
            else
            {
                MessageBox.Show("Ingrese el usuario y contraseña correctos",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
