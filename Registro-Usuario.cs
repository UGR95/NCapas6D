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

    public partial class Registro_Usuario : Form
    {
        DatosPubs datos = new DatosPubs();
        private string NombreUsuario;

        Dictionary<string, int> Rol = new Dictionary<string, int>()
        {
            {"Administrador", 0 },
            {"Operador", 1 }
        };

        public Registro_Usuario()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Registro_Usuario_Load(object sender, EventArgs e)
        {
            MostrarDatos();
            PanelRegistrar.Visible = false;
            cmbRolRegister.Items.Clear(); // Limpia por si acaso
            cmbRolRegister.Items.Add("Seleccionar registro");
            cmbRolRegister.Items.Add("Administrador");
            cmbRolRegister.Items.Add("Operador");
            cmbRol.Items.Add("Seleccionar registro");
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Operador");

            cmbRol.SelectedIndex = 0;
            cmbRolRegister.SelectedIndex = 0;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void MostrarDatos()
        {
            try
            {

                dgvUsers.DataSource = datos.MostrarUsuarios();
                dgvUsers.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        

        private void dgvUsers_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                txtIdUser.Text = row.Cells["usuario_id"].Value.ToString();
                txtUser.Text = row.Cells["nombreUsuario"].Value.ToString();
                NombreUsuario = row.Cells["nombreUsuario"].Value.ToString();
                cmbRol.Text = row.Cells["TipoUsuario"].Value.ToString();

            }
        }

        private void btnActualizarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string existe = "";
                string Error = "";
                bool IdTipoUsuario = false;
                
                // Validar que el ID no esté vacío
                if (string.IsNullOrEmpty(txtIdUser.Text))
                {
                    MessageBox.Show("Por favor selecciona un registro para actualizar.");
                    return;
                }

                existe = datos.ValidarUSuario(txtUser.Text, Convert.ToInt32(txtIdUser.Text));


                if (Convert.ToInt32(existe) > 0)
                {
                    MessageBox.Show("Ya existe otro usuario con ese nombre.");
                    return;
                }

                if (NombreUsuario != txtUser.Text)
                {
                    DialogResult dr = MessageBox.Show("Se modificará el nombre de usuario. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(dr == DialogResult.No)
                    {
                        return;
                    }
                }
                

                //IdTipoUsuario = Convert.ToBoolean(cmbRolRegister.SelectedIndex == 0 ? 0 : 1);

                if (Rol.ContainsKey(cmbRol.SelectedItem.ToString()))
                {
                    IdTipoUsuario = Convert.ToBoolean(Rol[cmbRol.SelectedItem.ToString()]);
                }
                else
                {
                    MessageBox.Show("Seleccionar un Rol válido.");
                }

                Error = datos.ActualizarUsuario(Convert.ToInt32(txtIdUser.Text), txtUser.Text, txtContraseña.Text, IdTipoUsuario);


                if (string.IsNullOrEmpty(Error))
                {
                    MessageBox.Show("Registro actualizado exitosamente.");
                    MostrarDatos(); // Refrescar el DataGridView
                }
                else
                {
                    MessageBox.Show("Error al actualizar actualizar.(" + Error + ")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUser.Text))
            {
                MessageBox.Show("Por favor selecciona un registro para eliminar.");
                return;
            }

            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM dbo.usuarios WHERE usuario_id = @ID";

                using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtIdUser.Text);

                        try
                        {
                            conn.Open();
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Registro eliminado correctamente.");
                                MostrarDatos(); // Refrescar tabla
                                txtIdUser.Clear();
                                txtUser.Clear();
                                cmbRol.Items.Clear();
                                txtContraseña.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro para eliminar.");
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

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string existe = "";
                string error = "";
                bool TipoUsuario = false;
                if (string.IsNullOrWhiteSpace(txtUserRegister.Text) || string.IsNullOrWhiteSpace(txtContraseñaRegister.Text) || string.IsNullOrWhiteSpace(txtRepetirContraseña.Text) || cmbRolRegister.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor completa todos los campos.");
                    return;
                }

                if (txtUserRegister.Text.Length < 6)
                {
                    MessageBox.Show("El nombre de usuario debe tener minimo 6 caracteres.");
                    return;
                }

                if (txtContraseñaRegister.Text.Length < 10)
                {
                    MessageBox.Show("La contraseña debe tener minimo 10 caracteres.");
                    return;
                }

                if (txtContraseñaRegister.Text != txtRepetirContraseña.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                    return;
                }


                existe = datos.ValidarUSuario(txtUserRegister.Text, null);

                if (Convert.ToInt32(existe) > 0)
                {
                    MessageBox.Show("Ya existe un usuario con ese nombre.");
                    return;
                }

                TipoUsuario = Convert.ToBoolean(cmbRolRegister.SelectedIndex == 0 ? 0 : 1);

                error = datos.AgregarUsuario(txtUserRegister.Text, txtContraseñaRegister.Text, TipoUsuario);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MostrarDatos();
                    txtUserRegister.Clear();
                    txtContraseñaRegister.Clear();
                    txtRepetirContraseña.Clear();
                    cmbRolRegister.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el usuario. ( " + error + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }
         

        private void button4_Click(object sender, EventArgs e)
        {
            new Dashboard_Admin().Show();
            this.Hide();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtIdUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cmbRolRegister_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtRepetirContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseñaRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Controls.Clear();
            PanelRegistrar.Dock = DockStyle.Fill;
            PanelPrincipal.Controls.Add(PanelRegistrar);
            PanelRegistrar.Visible = true;
        }

        private void BtnRegresarPnl_Click(object sender, EventArgs e)
        {
            PanelPrincipal.Controls.Clear();
            PanelActualizar.Dock = DockStyle.Fill;
            PanelPrincipal.Controls.Add(PanelActualizar);
            PanelActualizar.Visible = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {

            frmReporteUsers frm = new frmReporteUsers();
            Dispose();
            frm.Show();
        }
    }
}
