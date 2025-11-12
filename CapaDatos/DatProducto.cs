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

        public EntidadProducto ObtenerProductoPorId(int idProducto)
        {
            EntidadProducto producto = null;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SELECT idProducto, nombreProducto, precioUnitario FROM Producto WHERE idProducto = @idProducto", cn);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    producto = new EntidadProducto
                    {
                        IdProducto = Convert.ToInt32(dr["idProducto"]),
                        NombreProducto = dr["nombreProducto"].ToString(),
                        Precio = Convert.ToDecimal(dr["precioUnitario"])
                    };
                }
            }

            return producto;
        }


    }
}
