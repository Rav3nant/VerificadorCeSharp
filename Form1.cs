using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

//Ultima Modificación Hecha por: Pineda Ramos Jesus Ivan
//Fecha: 13 de octubre de 2021
namespace Verificador_Precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;

        private String codigo = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 0);
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, pictureBox1.Height + 10);
            pictureBox2.Location = new Point(this.Width/2 - pictureBox2.Width/2,this.Height/2);
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
                try
                {
                    MySqlConnection servidor;
                    servidor = new MySqlConnection("server = 127.0.0.1; user = root; database = tienda; SSL Mode = None; ");
                    servidor.Open();
                    string query = "SELECT nombreProducto, precioProducto, cantidadProducto, imagenProducto FROM productos WHERE numeroProducto =" + codigo + ";";
                    
                    MySqlCommand consulta;
                    consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resultado = consulta.ExecuteReader();
                    if (resultado.HasRows)
                    {
                        resultado.Read();
                        
                        label2.Visible = true;
                        pictureBox2.Visible=false;
                        pictureBox3.Visible = true;
                        label2.Text = resultado.GetString(0)+Environment.NewLine+"Precio: "+resultado.GetString(1) + " pesos"+
                            Environment.NewLine + "Stock: " + resultado.GetString(2)+" unidades";
                        pictureBox3.ImageLocation = resultado.GetString(3);
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                        segundos = 0;
                        timer1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Llame al supervisor, el producto no fue encontrado");
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            segundos++;

            if (segundos == 8)
            {
                timer1.Enabled = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                label2.Text = "";
            }
		}
	}
}