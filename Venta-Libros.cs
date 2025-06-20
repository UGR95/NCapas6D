﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinal_U1_2.Flujo;

namespace ProyectoFinal_U1_2
{
    public partial class Venta_Libros : Form
    {
        public Venta_Libros()
        {
            InitializeComponent();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Venta_Libros_Load(object sender, EventArgs e)
        {
            MostrarDatos();
            CargarTiendas();
            MostrarDescuentos();
            txtPorcentajeDescuento.Text = "0 %";
        }

        private void CargarTiendas()
        {
            FVentaLibro fVenta = new FVentaLibro();
            cmbTienda.DataSource = fVenta.CargarTiendas();
            cmbTienda.DisplayMember = "NombreTienda";
            cmbTienda.ValueMember = "IdTienda";
        }

        private void MostrarDescuentos()
        {
            string query = "SELECT discounttype, discount FROM vista_descuentos_disponibles";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Agregamos una columna para mostrar el texto traducido
                dt.Columns.Add("descuento_es", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string original = row["discounttype"].ToString();
                    row["descuento_es"] = TraducirDescuento(original); // Usa la función de abajo
                }

                cmbDescuentos.DataSource = dt;
                cmbDescuentos.DisplayMember = "descuento_es"; // Mostramos traducción
                cmbDescuentos.ValueMember = "discount";       // Usamos valor real
            }
        }
        private string TraducirDescuento(string original)
        {
            switch (original)
            {
                case "Initial Customer":
                    return "Cliente nuevo";
                case "Volume Discount":
                    return "Volumen de ventas";
                case "Customer Discount":
                    return "Personalizado";
                default:
                    return original;
            }
        }
        private void CalcularTotal()
        {
            if (//decimal.TryParse(txtCantidad.Text, out decimal cantidad) &&
                decimal.TryParse(txtPrecio.Text, out decimal precio) &&
                decimal.TryParse(txtPorcentajeDescuento.Text.Replace("%", ""), out decimal descuento))
            {
                decimal subtotal = NumUDCantidad.Value * precio;
                decimal totalConDescuento = subtotal - (subtotal * (descuento / 100));
                txtTotal.Text = totalConDescuento.ToString("0.00");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void MostrarDatos()
        {
            FVentaLibro fVenta = new FVentaLibro();
            try
            {
                //dgvLibros.Columns.Add("Title_Id", "Id Titulo");
                dgvLibros.DataSource = fVenta.MostrarLibrosVenta();
                dgvLibros.Columns["Error"].Visible = false;
                dgvLibros.Columns["IdTienda"].Visible = false;
                dgvLibros.Columns["NombreTienda"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLibros.Rows[e.RowIndex];

                txtIdLibro.Text = row.Cells["title_id"].Value.ToString();

                txtNombreLibro.Text = row.Cells[1].Value.ToString();
                txtNombreLibro.Enabled = false;
                txtTipo.Text = row.Cells[2].Value.ToString();
                txtTipo.Enabled = false;
                txtAutor.Text = row.Cells[6].Value.ToString();
                txtAutor.Enabled = false;
                txtNotas.Text = row.Cells[4].Value.ToString();
                txtNotas.Enabled = false;
                txtPublicado.Text = row.Cells[5].Value.ToString();
                txtPublicado.Enabled = false;
                txtPrecio.Text = row.Cells[3].Value.ToString();
                txtPrecio.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDescuentos.SelectedValue != null)
            {
                txtPorcentajeDescuento.Text = cmbDescuentos.SelectedValue.ToString();
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void label17_Click(object sender, EventArgs e)
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

        private void txtNombreLibro_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvLibros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNotas_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTipo_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FVentaLibro fVenta = new FVentaLibro();
            int qty;
            string titleId = txtIdLibro.Text;
            string Error = "";

            //if (!int.TryParse(txtCantidad.Text, out qty))
            //{
            //    MessageBox.Show("Por favor, ingresa una cantidad válida.");
            //    return;
            //}

            
            if (!ExisteTitle(titleId))
            {
                MessageBox.Show("El libro seleccionado no existe.");
                return;
            }
            if (NumUDCantidad.Value == 0) {
                MessageBox.Show("Valor invalido en cantidad a vender");
                return;
            }

            try
            {
               Error = fVenta.GenerarVenta(cmbTienda.SelectedValue.ToString(), Convert.ToInt32(NumUDCantidad.Value), titleId, Convert.ToDecimal(txtPorcentajeDescuento.Text), Convert.ToDecimal(txtTotal.Text));

                if (string.IsNullOrEmpty(Error))
                {

                    MessageBox.Show("Venta registrada correctamente.");
                    MostrarDatos();
                }
                else
                    MessageBox.Show(Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message);
            }
            
            string mensajeInventario = ObtenerMensajeInventario(titleId);

            MessageBox.Show(mensajeInventario);
        }
        private string ObtenerMensajeInventario(string titleId)
        {
            string mensaje = string.Empty;

            string checkInventarioQuery = "SELECT dbo.CheckLowInventory(@title_id)";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(checkInventarioQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@title_id", titleId);

                        mensaje = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar el inventario: " + ex.Message);
                }
            }

            return mensaje;
        }
        private bool ExisteTitle(string titleId)
        {
            string query = "SELECT COUNT(*) FROM Titles WHERE title_id = @title_id";

            using (SqlConnection conn = new SqlConnection(Conexion.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title_id", titleId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar(); 

                return count > 0;  
            }
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

        private void txtIdLibro_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbDescuentos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtPublicado_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtPorcentajeDescuento_TextChanged(object sender, EventArgs e)
        {
            CalcularTotal();

        }

        private void NumUDCantidad_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }
    }
}
