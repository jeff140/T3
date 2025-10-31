using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidad;
using CapaDatos;

namespace CapaDatos
{
    public class DatosProducto
    {

        public List<EntidadProducto> ListarProductos()
        {
            List<EntidadProducto> lista = new List<EntidadProducto>();
            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new EntidadProducto
                    {
                        IdProducto = (int)dr["IdProducto"],
                        NombreProducto = dr["NombreProducto"].ToString(),
                        Precio = (decimal)dr["Precio"]
                    });
                }
            }
            return lista;
        }
    }
}
