using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ProyectoFinal_U1_2
{
    public partial class Login : Form
    {
        DatosPubs datos = new DatosPubs();
        public Login()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {

            Ingresar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Ingresar()
        {
            try
            {
                string usuario = txtUser.Text.Trim();
                string contrasena = txtPassword.Text.Trim();
                object resultado = null;
                bool rol = false;
                if (usuario.Length < 6 || contrasena.Length < 10)
                {
                    MessageBox.Show("Usuario o contraseña no cumplen con los requisitos.");
                    return;
                }

                resultado = datos.Acceder(usuario, contrasena);

                if (resultado != null)
                {
                    rol = Convert.ToBoolean(resultado);
                    txtUser.Text = "";
                    txtPassword.Text = "";
                    if (!rol)
                    {

                        new Dashboard_Admin().Show();
                        this.Hide();

                    }
                    if (rol)
                    {
                        new Dashboard_Operador().Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ingresar();
            }
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ingresar();
            }
        }
    }
}
