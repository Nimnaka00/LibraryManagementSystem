using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;


namespace Library_Management_System
{
    public partial class LOGIN : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public LOGIN()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            server = "localhost";
            database = "LMS";
            uid = "root";
            password = "";
            string connString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
            conn = new MySqlConnection(connString);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void Mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void ChangePanelOpacity(int alphaValue)
        {
            Color panelColor = panel1.BackColor;
            panel1.BackColor = Color.FromArgb(alphaValue, panelColor.R, panelColor.G, panelColor.B);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ChangePanelOpacity(120);
        }

        private void sizes_Click(object sender, EventArgs e)
        {

        }


        private void lgbt_Click(object sender, EventArgs e)
        {
            string username = un.Text;
            string password = pn.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                conn.Open();

                string query = "SELECT LName, Lpass FROM librian WHERE LName = @username AND Lpass = @password";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("Login successful!");
                            Home hm = new Home();
                            hm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string username = un.Text;
            string password = pn.Text;

            if (username == "admin" && password == "admin")
            {
                MessageBox.Show("Admin login successful!");
                Librians lb = new Librians();
                lb.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                un.Clear();
                pn.Clear();
            }
        }

    }
}
