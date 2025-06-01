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
    public partial class Dashboard_Admin : Form
    {
        public Dashboard_Admin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void RegistroUsuario_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            new Registro_Usuario().Show();
            this.Hide();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            new StockLibros().Show();
            this.Hide();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            new Venta_Libros().Show();
        }

        private void btnAutores_Click(object sender, EventArgs e)
        {
            new Gestion_Autores().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Gestion_Titulos().Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new Gestion_Tiendas().Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new Gestion_Ventas().Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Gestion_Empleados().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Gestion_Descuentos().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Gestion_Autor_Titulo().Show();
            this.Hide();
        }
    }
}
