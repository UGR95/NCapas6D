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
						pVenta.price = Convert.ToDecimal(dr[3].ToString());
						pVenta.notes = dr[4].ToString();
						pVenta.pubDate = Convert.ToDateTime(dr[5].ToString());
						pVenta.NombreAutor = dr[6].ToString();

						ListaVentas.Add(pVenta);
                    }
					
				}

				return ListaVentas;
			}
			catch (Exception)
			{
				return new List<PVentaLibro>();
			}

        }

    }
}
