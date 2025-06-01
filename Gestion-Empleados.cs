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
    public partial class Gestion_Empleados : Form
    {
        public Gestion_Empleados()
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

        private void Gestion_Empleados_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            string query = "SELECT * FROM dbo.employee";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();

                    dataAdapter.Fill(dataTable);

                    dgvEmpleados.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];

                txtID.Text = row.Cells["emp_id"].Value.ToString();
                txtNombre.Text = row.Cells["fname"].Value.ToString();
                txtApellido.Text = row.Cells["lname"].Value.ToString();
                txtIDEditorial.Text = row.Cells["pub_id"].Value.ToString();
                txtIDTrabajo.Text = row.Cells["job_id"].Value.ToString();
                txtFechaContrato.Text = row.Cells["hire_date"].Value.ToString();

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string query = "UPDATE employee SET fname = @fname, lname = @lname, pub_id = @pub_id, job_id = @job_id, hire_date = @hire_date WHERE emp_id = @emp_id";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@fname", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@lname", txtApellido.Text);
                    cmd.Parameters.AddWithValue("@pub_id", txtIDEditorial.Text);
                    cmd.Parameters.AddWithValue("@job_id", txtIDTrabajo.Text);
                    cmd.Parameters.AddWithValue("@hire_date", DateTime.Parse(txtFechaContrato.Text));

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Empleado actualizado correctamente.");
                            MostrarDatos();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el empleado.");
                        }
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
            DialogResult result = MessageBox.Show("¿Estás seguro de eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM employee WHERE emp_id = @emp_id";

                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@emp_id", txtID.Text);

                        try
                        {
                            conn.Open();
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("Empleado eliminado correctamente.");
                                MostrarDatos();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el empleado.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO employee (emp_id, fname, lname, pub_id, job_id, job_lvl, hire_date) VALUES (@emp_id, @fname, @lname, @pub_id, @job_id, 80, GETDATE())";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@emp_id", txtIDRegister.Text);
                    cmd.Parameters.AddWithValue("@fname", txtNombreRegister.Text);
                    cmd.Parameters.AddWithValue("@lname", txtApellidoRegister.Text);
                    cmd.Parameters.AddWithValue("@pub_id", txtIDEditorialRegister.Text);
                    cmd.Parameters.AddWithValue("@job_id", txtIDTrabajoRegister.Text);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Empleado registrado correctamente.");
                            MostrarDatos();
                            txtIDRegister.Text = "";
                            txtNombreRegister.Text = "";
                            txtApellidoRegister.Text = "";
                            txtIDEditorialRegister.Text = "";
                            txtIDTrabajoRegister.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el empleado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar: " + ex.Message);
                    }
                }
            }
        }
    }
}
