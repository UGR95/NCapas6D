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
    public partial class StockLibros : Form
    {
        public StockLibros()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void StockLibros_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        private void MostrarDatos()
        {

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    s.id_stock,
                    s.title_id,
                    t.title AS NombreLibro,
                    s.stock
                FROM 
                    stocklibros s
                JOIN 
                    titles t ON s.title_id = t.title_id";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvStocks.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar datos: " + ex.Message);
                }
            }
        }

        private void dgvStocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStocks.Rows[e.RowIndex];

                txtIdStock.Text = row.Cells["id_stock"].Value.ToString();
                txtIdLibro.Text = row.Cells["title_id"].Value.ToString();
                txtLibro.Text = row.Cells["NombreLibro"].Value.ToString();
                txtStock.Text = row.Cells["stock"].Value.ToString();

            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tx_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {

        }

        private void txtIdLibro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtLibro_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvStocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvStocks.CurrentRow != null && !string.IsNullOrEmpty(txtStock.Text))
            {
                string titleId = dgvStocks.CurrentRow.Cells["title_id"].Value.ToString();
                int nuevoStock;

                if (!int.TryParse(txtStock.Text, out nuevoStock))
                {
                    MessageBox.Show("Ingresa un valor numérico válido para el stock.");
                    return;
                }


                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "UPDATE stocklibros SET stock = @stock WHERE title_id = @title_id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@stock", nuevoStock);
                        cmd.Parameters.AddWithValue("@title_id", titleId);

                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Stock actualizado correctamente.");
                            MostrarDatos(); // Refresca el DataGridView
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el libro para actualizar.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el stock: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un libro y escribe un nuevo stock.");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtIdStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUserRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbRolRegister_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseñaRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRepetirContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }
    }
}
