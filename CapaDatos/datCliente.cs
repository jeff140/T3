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
    public class datCliente
    {
        #region sigleton
        //Patron Singleton
        // Variable estática para la instancia
        private static readonly datCliente _instancia = new datCliente();
        //privado para evitar la instanciación directa
        public static datCliente Instancia
        {
            get
            {
                return datCliente._instancia;
            }
        }
        #endregion singleton

        #region metodos
        // ===== LISTAR CLIENTES =====
        public List<entCliente> ListarCliente()
        {
            List<entCliente> lista = new List<entCliente>();

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spListarCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        entCliente cli = new entCliente
                        {
                            idCliente = Convert.ToInt32(dr["idCliente"]),
                            DniRucCli = dr["DniRucCli"].ToString(),
                            Nom_razonSocial = dr["Nom_razonSocial"].ToString(),
                            fecRegCliente = Convert.ToDateTime(dr["fecRegCliente"]),
                            idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                            Telefono = dr["Telefono"] == DBNull.Value ? "" : dr["Telefono"].ToString()
                        };
                        lista.Add(cli);
                    }
                }
            }

            return lista;
        }
        // ===== INSERTAR CLIENTE =====
        public bool InsertarCliente(entCliente cli)
        {
            bool inserta = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spInsertarCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60;

                cmd.Parameters.AddWithValue("@DniRucCli", cli.DniRucCli);
                cmd.Parameters.AddWithValue("@Nom_razonSocial", cli.Nom_razonSocial);
                cmd.Parameters.AddWithValue("@fecRegCliente", cli.fecRegCliente);
                cmd.Parameters.AddWithValue("@idDepartamento", cli.idDepartamento);
                cmd.Parameters.AddWithValue("@Telefono", (object)cli.Telefono ?? DBNull.Value);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = (i > 0);
            }

            return inserta;
        }

        // ===== EDITAR CLIENTE =====
        public bool EditarCliente(entCliente cli)
        {
            bool edita = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spEditarCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCliente", cli.idCliente);
                cmd.Parameters.AddWithValue("@DniRucCli", cli.DniRucCli);
                cmd.Parameters.AddWithValue("@Nom_razonSocial", cli.Nom_razonSocial);
                cmd.Parameters.AddWithValue("@fecRegCliente", cli.fecRegCliente);
                cmd.Parameters.AddWithValue("@idDepartamento", cli.idDepartamento);
                cmd.Parameters.AddWithValue("@Telefono", (object)cli.Telefono ?? DBNull.Value);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                edita = (i > 0);
            }

            return edita;
        }
        // ===== ELIMINAR CLIENTE =====
        public bool EliminarCliente(entCliente cli)
        {
            bool eliminar = false;

            using (SqlConnection cn = Conexion.Instancia.Conectar())
            using (SqlCommand cmd = new SqlCommand("spEliminarCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCliente", cli.idCliente);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                eliminar = (i > 0);
            }

            return eliminar;
        }
        #endregion
    }
}