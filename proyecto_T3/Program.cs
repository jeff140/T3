using CapaEntidad;
using CapaLogica;
using proyecto_T3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacionT3;

namespace CapaPresentacionT3
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new Ingreso());
=======

            // 🔸 Aquí indicas qué formulario se abrirá primero
            Application.Run(new ventaProducto());
>>>>>>> origin/venta
        }
    }
}
