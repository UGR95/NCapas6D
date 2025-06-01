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
    public partial class Dashboard_Operador : Form
    {
        public Dashboard_Operador()
        {
            InitializeComponent();
        }

        private void Dashboard_Operador_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Venta_Libros().Show();
        }
    }
}
