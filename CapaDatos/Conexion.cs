using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        //patron de Diseño Singleton
        private static readonly Conexion _instancia = new Conexion();
        public static Conexion Instancia
        {
            get { return Conexion._instancia; }
        }


        //CONECTAR
        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-3S7IB5J\\SQLEXPRESS01; Initial Catalog = DB_Cliente;" +//"User ID=sa; Password=123";
             "Integrated Security=true";

            return cn;
        }
    }
}
