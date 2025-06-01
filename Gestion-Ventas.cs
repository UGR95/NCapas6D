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
    public partial class Gestion_Ventas : Form
    {
        public Gestion_Ventas()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Gestion_Ventas_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            string query = @"
        SELECT 
            s.ord_num,
            s.ord_date,
            s.qty,
            t.title AS title_name,
            s.porcentaje_descuento,
            s.total
        FROM sales s
        JOIN titles t ON s.title_id = t.title_id";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();
                    dataAdapter.Fill(dataTable);
                    dgvVentas.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVentas.Rows[e.RowIndex];

                txtId.Text = row.Cells["ord_num"].Value.ToString();
                txtLibro.Text = row.Cells["title_name"].Value.ToString();  // Nombre del libro
                txtCantidad.Text = row.Cells["qty"].Value.ToString();
                txtFecha.Text = row.Cells["ord_date"].Value.ToString();
                txtDescuento.Text = row.Cells["porcentaje_descuento"].Value.ToString();
                txtTotal.Text = row.Cells["total"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecciona una venta primero.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de eliminar esta venta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM sales WHERE ord_num = @ord_num";

                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ord_num", txtId.Text);

                        try
                        {
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Venta eliminada correctamente.");
                                MostrarDatos();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró la venta.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar la venta: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }
    }
}
