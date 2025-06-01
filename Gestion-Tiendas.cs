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
    public partial class Gestion_Tiendas : Form
    {
        public Gestion_Tiendas()
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

        private void Gestion_Tiendas_Load(object sender, EventArgs e)
        {
            MostrarDatos();
            MostrarEstadosRegistro();
            CargarEstados();
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
        private void MostrarDatos()
        {
            string query = "SELECT * FROM dbo.stores";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                DataTable dataTable = new DataTable();

                try
                {
                    conn.Open();

                    dataAdapter.Fill(dataTable);

                    dgvTiendas.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void dgvTiendas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTiendas.Rows[e.RowIndex];

                txtId.Text = row.Cells["stor_id"].Value.ToString();
                txtNombre.Text = row.Cells["stor_name"].Value.ToString();
                txtDireccion.Text = row.Cells["stor_address"].Value.ToString();
                txtCiudad.Text = row.Cells["city"].Value.ToString();
                cmbEstado.SelectedValue = row.Cells["state"].Value.ToString();
                txtCP.Text = row.Cells["zip"].Value.ToString();

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecciona una tienda para actualizar.");
                return;
            }

            string query = "UPDATE stores SET stor_name = @Nombre, stor_address = @Direccion, city = @Ciudad, state = @Estado, zip = @CP WHERE stor_id = @Id";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                cmd.Parameters.AddWithValue("@Estado", cmbEstado.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CP", txtCP.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tienda actualizada correctamente.");
                    MostrarDatos(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar tienda: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Selecciona una tienda para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show("¿Estás seguro de que deseas eliminar esta tienda?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM stores WHERE stor_id = @Id";

                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tienda eliminada correctamente.");
                        MostrarDatos(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar tienda: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdRegister.Text) ||
        string.IsNullOrWhiteSpace(txtNombreRegister.Text) ||
        string.IsNullOrWhiteSpace(txtDireccionRegister.Text) ||
        string.IsNullOrWhiteSpace(txtCiudadRegister.Text) ||
        string.IsNullOrWhiteSpace(cmbEstadosDisponibles.SelectedValue?.ToString()) ||
        string.IsNullOrWhiteSpace(txtCPRegister.Text))
            {
                MessageBox.Show("Completa todos los campos para registrar la tienda.");
                return;
            }

            string query = "INSERT INTO stores (stor_id, stor_name, stor_address, city, state, zip) VALUES (@Id, @Nombre, @Direccion, @Ciudad, @Estado, @CP)";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", txtIdRegister.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombreRegister.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccionRegister.Text);
                cmd.Parameters.AddWithValue("@Ciudad", txtCiudadRegister.Text);
                cmd.Parameters.AddWithValue("@Estado", cmbEstadosDisponibles.SelectedValue?.ToString());
                cmd.Parameters.AddWithValue("@CP", txtCPRegister.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tienda registrada correctamente.");
                    MostrarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar tienda: " + ex.Message);
                }
            }
        }
    }
}
