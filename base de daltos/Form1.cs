using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;  

namespace GestionClientes
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=Clientes;User ID=tu_usuario;Password=tu_contraseña;";

        public Form1()
        {
            InitializeComponent();  
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) || string.IsNullOrWhiteSpace(txtRubro.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO minoristas (nombre, apellido, dni, rubro) VALUES (@nombre, @apellido, @dni, @rubro)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
                        cmd.Parameters.AddWithValue("@rubro", txtRubro.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery(); 
                        conn.Close();
                    }

                    MessageBox.Show("Datos guardados correctamente.");
                    LimpiarCampos();  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDNI.Clear();
            txtRubro.Clear();
        }
    }
}
