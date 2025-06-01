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
    public partial class Gestion_Titulos : Form
    {
        public Gestion_Titulos()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }

        private void Gestion_Titulos_Load(object sender, EventArgs e)
        {
            MostrarDatos();
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

                    dgvTitulos.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvTitulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTitulos.Rows[e.RowIndex];

                txtID.Text = row.Cells["title_id"].Value.ToString();
                txtTitulo.Text = row.Cells["title"].Value.ToString();
                txtTipo.Text = row.Cells["type"].Value.ToString();
                txtNotas.Text = row.Cells["notes"].Value.ToString();
                txtPrecio.Text = row.Cells["price"].Value.ToString();
                txtPublicado.Text = row.Cells["pubdate"].Value.ToString();

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtTipo.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            string query = "UPDATE titles SET title = @Title, type = @Type, notes = @Notes, price = @Price, pubdate = @Pubdate WHERE title_id = @TitleID";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TitleID", txtID.Text);
                    cmd.Parameters.AddWithValue("@Title", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@Type", txtTipo.Text);
                    cmd.Parameters.AddWithValue("@Notes", txtNotas.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.TryParse(txtPrecio.Text, out decimal price) ? price : 0);
                    cmd.Parameters.AddWithValue("@Pubdate", DateTime.TryParse(txtPublicado.Text, out DateTime pubDate) ? pubDate : DateTime.Now);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro actualizado correctamente.");
                        MostrarDatos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar: " + ex.Message);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Por favor, seleccione un registro para eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM titles WHERE title_id = @TitleID";

                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TitleID", txtID.Text);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registro eliminado correctamente.");
                            MostrarDatos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar, verifique que no esté relacionado a algún autor" );
                        }
                    }
                }
            }
        }
        private void LimpiarCamposRegistro()
        {
            txtIdRegister.Clear();
            txtTituloRegister.Clear();
            txtTipoRegister.Clear();
            txtNotasRegister.Clear();
            txtPrecioRegister.Clear();
            txtPublicadoRegister.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdRegister.Text) ||
       string.IsNullOrWhiteSpace(txtTituloRegister.Text) ||
       string.IsNullOrWhiteSpace(txtTipoRegister.Text))
            {
                MessageBox.Show("Por favor, completa los campos obligatorios (ID, Título y Tipo).");
                return;
            }

            // Consulta SQL para insertar un nuevo título
            string query = "INSERT INTO titles (title_id, title, type, notes, price, pubdate) " +
                           "VALUES (@ID, @Titulo, @Tipo, @Notas, @Precio, @FechaPublicacion)";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Asignar parámetros
                    cmd.Parameters.AddWithValue("@ID", txtIdRegister.Text);
                    cmd.Parameters.AddWithValue("@Titulo", txtTituloRegister.Text);
                    cmd.Parameters.AddWithValue("@Tipo", txtTipoRegister.Text);
                    cmd.Parameters.AddWithValue("@Notas", txtNotasRegister.Text);
                    cmd.Parameters.AddWithValue("@Precio", decimal.TryParse(txtPrecioRegister.Text, out decimal precio) ? precio : 0);
                    cmd.Parameters.AddWithValue("@FechaPublicacion", DateTime.TryParse(txtPublicadoRegister.Text, out DateTime fecha) ? fecha : DateTime.Now);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Título registrado correctamente.");

                        // Limpia los campos después de insertar (opcional)
                        LimpiarCamposRegistro();

                        // Recarga el DataGridView si lo necesitas
                        MostrarDatos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar el título: " + ex.Message);
                    }
                }
            }
        }
    }
}
