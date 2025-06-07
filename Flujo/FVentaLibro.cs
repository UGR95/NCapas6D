using ProyectoFinal_U1_2.Presentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal_U1_2.Datos;

namespace ProyectoFinal_U1_2.Flujo
{
    public class FVentaLibro
    {
        public List<PVentaLibro> MostrarLibrosVenta()
        {
			try
			{
				DVentaLibro dv = new DVentaLibro();
				return dv.MostrarLibrosVenta(); 
			}
			catch (Exception)
			{
                List<PVentaLibro> lista = new List<PVentaLibro>();
                return lista;

            }
        }

        public void GenerarVenta(int qty, string Title_id, decimal Porcentaje, decimal Total)
        {
            DVentaLibro dVenta = new DVentaLibro();
            dVenta.GenerarVenta(qty, Title_id, Porcentaje, Total);
        }
    }
}
