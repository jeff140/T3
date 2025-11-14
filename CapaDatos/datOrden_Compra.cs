using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datOrden_Compra
    {

            #region singleton
            private static readonly datOrden_Compra _instancia = new datOrden_Compra();
            public static datOrden_Compra Instancia
            {
                get { return datOrden_Compra._instancia; }
            }
            #endregion singleton

            #region metodos
            // Listar Ordenes de Compra
            public List<entOrden_Compra> ListarOrdenCompra()
            {
                SqlCommand cmd = null;
                List<entOrden_Compra> lista = new List<entOrden_Compra>();
                try
                {
                    SqlConnection cn = Conexion.Instancia.Conectar();
                    cmd = new SqlCommand("sp_ListarOrdenesCompra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        entOrden_Compra oc = new entOrden_Compra();
                        oc.idOrdenCompra = Convert.ToInt32(dr["idOrdenCompra"]);
                        oc.fechaEmision = Convert.ToDateTime(dr["fechaEmision"]);
                        oc.idProveedor = Convert.ToInt32(dr["idProveedor"]);
                        oc.total = Convert.ToDecimal(dr["total"]);
                        oc.estado = Convert.ToBoolean(dr["estado"]);
                        lista.Add(oc);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Connection.Close();
                }
                return lista;
            }

            // Insertar Orden de Compra (retorna el ID generado)
            public int InsertarOrdenCompra(entOrden_Compra oc)
            {
                SqlCommand cmd = null;
                int idGenerado = 0;
                try
                {
                    SqlConnection cn = Conexion.Instancia.Conectar();
                    cmd = new SqlCommand("sp_InsertarOrdenCompra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaEmision", oc.fechaEmision);
                    cmd.Parameters.AddWithValue("@idProveedor", oc.idProveedor);
                    cmd.Parameters.AddWithValue("@total", oc.total);
                    cmd.Parameters.AddWithValue("@estado", oc.estado);
                    cn.Open();
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally { cmd.Connection.Close(); }
                return idGenerado;
            }

            // Editar Orden de Compra
            public Boolean EditarOrdenCompra(entOrden_Compra oc)
            {
                SqlCommand cmd = null;
                Boolean edita = false;
                try
                {
                    SqlConnection cn = Conexion.Instancia.Conectar();
                    cmd = new SqlCommand("sp_ActualizarOrdenCompra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idOrdenCompra", oc.idOrdenCompra);
                    cmd.Parameters.AddWithValue("@fechaEmision", oc.fechaEmision);
                    cmd.Parameters.AddWithValue("@idProveedor", oc.idProveedor);
                    cmd.Parameters.AddWithValue("@total", oc.total);
                    cmd.Parameters.AddWithValue("@estado", oc.estado);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        edita = true;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally { cmd.Connection.Close(); }
                return edita;
            }

            // Deshabilitar Orden de Compra
            public Boolean DeshabilitarOrdenCompra(entOrden_Compra oc)
            {
                SqlCommand cmd = null;
                Boolean delete = false;
                try
                {
                    SqlConnection cn = Conexion.Instancia.Conectar();
                    cmd = new SqlCommand("sp_EliminarOrdenCompra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idOrdenCompra", oc.idOrdenCompra);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        delete = true;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally { cmd.Connection.Close(); }
                return delete;
            }

            // Agregar este método en la clase datOrdenCompra
            public int ObtenerSiguienteIdOrden()
            {
                SqlCommand cmd = null;
                int siguienteId = 1;
                try
                {
                    SqlConnection cn = Conexion.Instancia.Conectar();
                    cmd = new SqlCommand("spObtenerSiguienteIdOrden", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    siguienteId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Connection.Close();
                }
                return siguienteId;
            }









            #endregion metodos
        }
    }
