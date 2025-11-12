using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class LogicaProducto
    {
        private DatosProducto datos = new DatosProducto();

        public EntidadProducto ObtenerProductoPorId(int idProducto)
        {
            DatosProducto datProducto = new DatosProducto();
            return datProducto.ObtenerProductoPorId(idProducto);
        }
    public List<EntidadProducto> ListarProductos()
        {
            List<EntidadProducto> lista = new List<EntidadProducto>();

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            {
                SqlCommand cmd = new SqlCommand("SELECT idProducto, nombreProducto, precioUnitario FROM Producto", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new EntidadProducto
                    {
                        IdProducto = Convert.ToInt32(dr["idProducto"]),
                        NombreProducto = dr["nombreProducto"].ToString(),
                        Precio = Convert.ToDecimal(dr["precioUnitario"])
                    });
                }
            }

            return lista;
            }
        }
    }