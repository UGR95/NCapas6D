using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_U1_2
{
    public partial class Gestion_Autor_Titulo : Form
    {
        public Gestion_Autor_Titulo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Gestion_Autor_Titulo_Load(object sender, EventArgs e)
        {
            MostrarDatos();
            CargarAutores();
        }
        private void CargarAutores()
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = "SELECT au_id, au_fname + ' ' + au_lname AS NombreCompleto FROM authors";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                try
                {
                    conn.Open();
                    da.Fill(dt);

                    cmbAutores.DataSource = dt;
                    cmbAutores.DisplayMember = "NombreCompleto"; // Lo que se muestra
                    cmbAutores.ValueMember = "au_id";             // Lo que se guarda internamente
                    cmbAutores.SelectedIndex = -1;                // Opcional: para que aparezca vacío
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar autores: " + ex.Message);
                }
            }
        }

        private void MostrarDatos()
        {
            string query = "SELECT * FROM dbo.titles";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();

                    dataAdapter.Fill(dataTable);

                    dgvLibros.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLibros.Rows[e.RowIndex];

                txtID.Text = row.Cells["title_id"].Value.ToString();
                txtTitulo.Text = row.Cells["title"].Value.ToString();
                txtTipo.Text = row.Cells["type"].Value.ToString();
                txtNotas.Text = row.Cells["notes"].Value.ToString();
                txtPrecio.Text = row.Cells["price"].Value.ToString();
                txtPublicado.Text = row.Cells["pubdate"].Value.ToString();
                txtNombreAsignar.Text = row.Cells["title"].Value.ToString();
                

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = @"INSERT INTO titleauthor (au_id, title_id, royaltyper)
                         VALUES (@AutorID, @TituloID, @Regalias)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AutorID", cmbAutores.SelectedValue);
                cmd.Parameters.AddWithValue("@TituloID", txtID.Text);
                cmd.Parameters.AddWithValue("@Regalias", Convert.ToInt32(txtRegalias.Text));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Autor asignado al título correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al asignar: " + ex.Message);
                }
            }
        }
    }
}
