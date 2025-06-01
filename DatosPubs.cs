using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting.Charting.Styles;

namespace ProyectoFinal_U1_2
{
    internal class DatosPubs
    {
        #region Logging

        public object Acceder (string Usuario, string Clave)
        {
            try
            {
                object restultado = null;

                using(SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open ();
                    SqlCommand cmd = new SqlCommand("spr_ConsultaUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Clave", Clave);

                    restultado = cmd.ExecuteScalar();
                    con.Close();
                    return restultado;

                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public DataTable MostrarUsuarios()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open ();
                    SqlDataAdapter da = new SqlDataAdapter("spr_MostrarUsuarios",con);
                    
                    da.Fill(dt);

                    con.Close();
                    return dt;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ValidarUSuario(string Usuario, int? IdUsuario)
        {
            try
            {
                int Resultado = 0;

                using (SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open ();
                    SqlCommand cmd = new SqlCommand("spr_ValidaUsuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    Resultado = (int)cmd.ExecuteScalar();

                    return Resultado.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string AgregarUsuario(string Usuario, string Clave, bool IdTipoUsuario)
        {
            try
            {
                string Error = "";

                using (SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spi_UsuarioNuevo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
                    cmd.Parameters.AddWithValue("@TipoUsuario",IdTipoUsuario);

                    SqlParameter paramError = new SqlParameter("@Error", SqlDbType.VarChar, 100);
                    paramError.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramError);

                    cmd.ExecuteNonQuery();

                    con.Close();


                    return paramError.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string ActualizarUsuario(int IdUsuario,string Usuario, string Clave, bool IdTipoUsuario)
        {
            try
            {
                string Error = "";

                using (SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("spu_Usuario", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.Parameters.AddWithValue("@Usuario", Usuario);
                    cmd.Parameters.AddWithValue("@Clave", Clave);
                    cmd.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);

                    SqlParameter paramError = new SqlParameter("@Error", SqlDbType.VarChar, 100);
                    paramError.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramError);

                    cmd.ExecuteNonQuery();

                    con.Close();


                    return paramError.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region Autores
        public DataTable ConsultaAutoresRpt()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.ConnectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("spr_ConsultaAutores", con);

                    da.Fill(dt);

                }
                    return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public DataTable ConsultaAutoresActivos(bool Contrato)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlConnection con = new SqlConnection(Conexion.ConnectionString);
                SqlCommand cmd = new SqlCommand("spr_ConsultaAutorContratoActvios", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Contrato", Contrato);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                
                dt.Load(dr);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion 
    }
}
