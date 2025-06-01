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
    public partial class Gestion_Descuentos : Form
    {
        public Gestion_Descuentos()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Gestion_Descuentos_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            string query = "SELECT discounttype, discount FROM dbo.discounts";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();

                    dataAdapter.Fill(dataTable);

                    dgvDescuentos.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvDescuentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDescuentos.Rows[e.RowIndex];

                txtNombre.Text = row.Cells["discounttype"].Value.ToString();
                txtDescuento.Text = row.Cells["discount"].Value.ToString();
                


            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = @"UPDATE discounts SET 
                            discounttype = @Nombre, 
                            discount = @Descuento 
                         WHERE discounttype = @Nombre";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Descuento", Convert.ToDecimal(txtDescuento.Text));


                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Descuento actualizado correctamente.");
                MostrarDatos();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Selecciona o escribe el nombre del descuento a eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de eliminar este descuento?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    string query = "DELETE FROM discounts WHERE discounttype = @Nombre";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Descuento eliminado correctamente.");
                        MostrarDatos();

                        txtNombre.Clear();
                        txtDescuento.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreRegister.Text) || string.IsNullOrWhiteSpace(txtDescuentoRegister.Text))
            {
                MessageBox.Show("Completa todos los campos para registrar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = "INSERT INTO discounts (discounttype, discount) VALUES (@Nombre, @Descuento)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", txtNombreRegister.Text);
                cmd.Parameters.AddWithValue("@Descuento", Convert.ToDecimal(txtDescuentoRegister.Text));

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Descuento registrado correctamente.");
                    MostrarDatos();

                    txtNombreRegister.Clear();
                    txtDescuentoRegister.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar: " + ex.Message);
                }
            }
        }
    }
}
