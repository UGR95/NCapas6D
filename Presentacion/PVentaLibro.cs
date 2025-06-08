using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_U1_2.Presentacion
{
    public class PVentaLibro
    {
        public string title_Id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public decimal price {  get; set; }
        public string notes { get; set; }
        public DateTime pubDate { get; set; }
        public string NombreAutor { get; set; }

        public int Stock {  get; set; }
        public string Error { get; set; }

        public string IdTienda { get; set; }
        public string NombreTienda { get; set; }

    }
}
