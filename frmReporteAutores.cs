using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinal_U1_2.Reportes;

namespace ProyectoFinal_U1_2
{
    public partial class frmReporteAutores : Form
    {
        public frmReporteAutores()
        {
            InitializeComponent();
        }

        private void frmReporteAutores_Load(object sender, EventArgs e)
        {
            ConsultaAutoresRpt(1, true);
        }

        private void ConsultaAutoresRpt(int tipo, bool Contrato)
        {
            ReporteAutores rpt = new ReporteAutores();
            DatosPubs dato = new DatosPubs();
            DataTable dt = new DataTable();

            try
            {
                switch (tipo)
                {
                    case 1:
                        dt = dato.ConsultaAutoresRpt();
                        rpt.table1.DataSource = dt;
                        reportVAutores.ReportSource = rpt;
                        reportVAutores.RefreshReport();
                        break;
                    case 2:
                        dt = dato.ConsultaAutoresActivos(Contrato);
                        rpt.table1.DataSource = dt;
                        reportVAutores.ReportSource = rpt;
                        reportVAutores.RefreshReport();
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void reportVAutores_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ConsultaAutoresRpt(2, chkContratoAct.Checked);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
