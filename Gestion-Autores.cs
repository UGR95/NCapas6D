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
    public partial class Gestion_Autores : Form
    {
        public Gestion_Autores()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Gestion_Autores_Load(object sender, EventArgs e)
        {
            MostrarDatos();
            CargarEstados();
            MostrarEstadosRegistro();
        }
        private void MostrarDatos()
        {
            string query = "SELECT * FROM dbo.authors";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();

                    dataAdapter.Fill(dataTable);

                    dgvAutores.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvAutores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAutores.Rows[e.RowIndex];

                txtIdAutor.Text = row.Cells["au_id"].Value.ToString();
                txtNombreAutor.Text = row.Cells["au_fname"].Value.ToString();
                txtApellidoAutor.Text = row.Cells["au_lname"].Value.ToString();
                txtTelefono.Text = row.Cells["phone"].Value.ToString();
                txtDireccion.Text = row.Cells["address"].Value.ToString();
                txtCiudad.Text = row.Cells["city"].Value.ToString();
                cmbEstado.SelectedValue = row.Cells["state"].Value.ToString();
                txtCP.Text = row.Cells["zip"].Value.ToString();
                ckbContrato.Checked = Convert.ToBoolean(row.Cells["contract"].Value);


            }
        }
        private void CargarEstados()
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = "SELECT Abreviacion, Nombre FROM Estados ORDER BY Nombre";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Agrega una fila vacía al inicio
                DataRow dr = dt.NewRow();
                dr["Abreviacion"] = "";
                dr["Nombre"] = "";
                dt.Rows.InsertAt(dr, 0);

                cmbEstado.DisplayMember = "Nombre";
                cmbEstado.ValueMember = "Abreviacion";
                cmbEstado.DataSource = dt;
                cmbEstado.SelectedIndex = 0;
                cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = @"UPDATE authors SET 
                            au_fname = @Nombre, 
                            au_lname = @Apellido, 
                            phone = @Telefono, 
                            address = @Direccion, 
                            city = @Ciudad, 
                            state = @Estado, 
                            zip = @CP, 
                            contract = @Contrato 
                         WHERE au_id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", txtNombreAutor.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellidoAutor.Text);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                cmd.Parameters.AddWithValue("@Estado", cmbEstado.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CP", txtCP.Text);
                cmd.Parameters.AddWithValue("@Contrato", ckbContrato.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@Id", txtIdAutor.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Autor actualizado correctamente.");
                MostrarDatos(); 
            }
        }
        private void MostrarEstadosRegistro()
        {
            // Cargar los estados al ComboBox
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = "SELECT Nombre, Abreviacion FROM Estados";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbEstadosDisponibles.DisplayMember = "Nombre"; // Mostrar el nombre completo del estado
                    cmbEstadosDisponibles.ValueMember = "Abreviacion"; // Guardar la abreviación
                    cmbEstadosDisponibles.DataSource = dt;
                }
            }

            cmbEstadosDisponibles.SelectedIndex = -1;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                string query = "INSERT INTO authors (au_id, au_fname, au_lname, phone, address, city, state, zip, contract) " +
                               "VALUES (@Id, @Nombre, @Apellido, @Telefono, @Direccion, @Ciudad, @Estado, @CP, @Contrato)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", txtIdRegister.Text);
                    cmd.Parameters.AddWithValue("@Nombre", txtNombreRegister.Text);
                    cmd.Parameters.AddWithValue("@Apellido", txtApellidoRegister.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefonoRegister.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccionRegister.Text);
                    cmd.Parameters.AddWithValue("@Ciudad", txtCiudadRegister.Text);
                    cmd.Parameters.AddWithValue("@Estado", cmbEstadosDisponibles.SelectedValue?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@CP", txtCPRegister.Text);
                    cmd.Parameters.AddWithValue("@Contrato", chkContratoRegister.Checked ? 1 : 0);

                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Autor registrado correctamente.");
                        MostrarDatos();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar autor: " + ex.Message);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdAutor.Text))
            {
                MessageBox.Show("Selecciona un autor para eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de eliminar este autor?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    string query = "DELETE FROM authors WHERE au_id = @Id";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", txtIdAutor.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Autor eliminado correctamente.");
                        MostrarDatos();

                        // Limpia los campos
                        txtIdAutor.Clear();
                        txtNombreAutor.Clear();
                        txtApellidoAutor.Clear();
                        txtTelefono.Clear();
                        txtDireccion.Clear();
                        txtCiudad.Clear();
                        txtCP.Clear();
                        ckbContrato.Checked = false;
                        cmbEstado.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
        }

        private void dgvAutores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReporteAutores reporte = new frmReporteAutores();
            Dispose();
            reporte.ShowDialog();

        }
    }
}
