using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_U1_2
{
    public partial class frmReporteUsers : Form
    {
        public frmReporteUsers()
        {
            InitializeComponent();
        }

        private void frmReporteUsers_Load(object sender, EventArgs e)
        {
            ReporteUsuarios();

        }

        private void ReporteUsuarios()
        {
            Reportes.ReporteUsuarios rpt = new Reportes.ReporteUsuarios();
            DataTable dt = new DataTable();
            DatosPubs datos = new DatosPubs();
            dt = datos.MostrarUsuarios();
            rpt.table1.DataSource = dt;
            rptViewUsuarios.ReportSource = rpt;
            rptViewUsuarios.RefreshReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Registro_Usuario ru = new Registro_Usuario();
            Dispose();
            ru.ShowDialog();
        }
    }
}
