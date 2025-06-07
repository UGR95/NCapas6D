using ProyectoFinal_U1_2.Presentacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal_U1_2.Presentacion;

namespace ProyectoFinal_U1_2.Datos
{
    internal class DVentaLibro
    {

        public List<PVentaLibro> MostrarLibrosVenta()
        {
			try
			{
                List<PVentaLibro> ListaVentas = new List<PVentaLibro>();

				using(SqlConnection con = new SqlConnection(Conexion.ConnectionString))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("spr_MostrarTituloVenta", con);

					cmd.CommandType = CommandType.StoredProcedure;

					IDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
					{
                        PVentaLibro pVenta = new PVentaLibro();
                        pVenta.title_Id = dr[0].ToString();
						pVenta.title = dr[1].ToString();
						pVenta.type = dr[2].ToString();
						if (!string.IsNullOrEmpty(dr[3].ToString()))
                            pVenta.price = Convert.ToDecimal(dr[3].ToString());
                        else
                            pVenta.price = 0; 

						pVenta.notes = dr[4].ToString();
						pVenta.pubDate = Convert.ToDateTime(dr[5].ToString());
						pVenta.NombreAutor = dr[6].ToString();

						ListaVentas.Add(pVenta);
                    }
					
				}

				return ListaVentas;
			}
			catch (Exception ex)
			{
				List<PVentaLibro> lista = new List<PVentaLibro>();
				PVentaLibro pVenta = new PVentaLibro();

				pVenta.Error = ex.ToString();
				lista.Add(pVenta);
				return lista;
			}

        }

		public void GenerarVenta(int qty, string Title_id, decimal Porcentaje, decimal Total)
		{
			try
			{
				
				using(SqlConnection con = new SqlConnection(Conexion.ConnectionString))
				{
					SqlCommand cmd = new SqlCommand("InsertSale", con);
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@title_id", Title_id);
                    cmd.Parameters.AddWithValue("@porcentaje_descuento", Porcentaje);
                    cmd.Parameters.AddWithValue("@total", Total);

					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
                }
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
