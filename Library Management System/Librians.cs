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
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Security.Policy;
using System.Xml.Linq;

namespace Library_Management_System
{
    public partial class Librians : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public Librians()
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

        private void Librians_Load(object sender, EventArgs e)
        {
            DisplayBooks();
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

        private void Mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DisplayBooks()
        {
            try
            {


                string query = "SELECT * FROM librian";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                DataTable dataTable = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }

                // Bind the DataTable to your DataGridView
                bunifuCustomDataGrid1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || phone.Text == "" || ps.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO librian (LName, Lphone, Lpass) VALUES (@name, @phone, @ps)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@name", name.Text);
                        command.Parameters.AddWithValue("@phone", phone.Text);
                        command.Parameters.AddWithValue("@ps", ps.Text);

                        command.ExecuteNonQuery(); // Insert the data into the database

                        MessageBox.Show("Data inserted successfully");
                        DisplayBooks(); // Refresh the DataGridView
                        name.Text = "";
                        phone.Text = "";
                        ps.Text = "";
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
        }



        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure a valid row is clicked
            {
                DataGridViewRow row = bunifuCustomDataGrid1.Rows[e.RowIndex];

                // Populate the input fields with the data from the selected row
                name.Text = row.Cells["LName"].Value.ToString();
                phone.Text = row.Cells["Lphone"].Value.ToString();
                ps.Text = row.Cells["Lpass"].Value.ToString();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            // Get the selected record's ID from the DataGridView
            int selectedRowIndex = bunifuCustomDataGrid1.SelectedRows[0].Index;
            int librarianID = Convert.ToInt32(bunifuCustomDataGrid1.Rows[selectedRowIndex].Cells["LID"].Value);

            try
            {
                conn.Open();

                string query = "DELETE FROM librian WHERE LID=@lid";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@lid", librarianID);

                    int rowsAffected = command.ExecuteNonQuery(); // Delete the record from the database

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        DisplayBooks(); // Refresh the DataGridView
                        name.Text = "";
                        phone.Text = "";
                        ps.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the record");
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

        private void sizes_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            hm.Show();
            this.Hide();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            // Get the selected record's ID from the DataGridView
            int selectedRowIndex = bunifuCustomDataGrid1.SelectedRows[0].Index;
            int librarianID = Convert.ToInt32(bunifuCustomDataGrid1.Rows[selectedRowIndex].Cells["LID"].Value);

            try
            {
                conn.Open();

                string query = "UPDATE librian SET LName=@name, Lphone=@phone, Lpass=@ps WHERE LID=@lid";


                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Name", name.Text);
                    command.Parameters.AddWithValue("@phone", phone.Text);
                    command.Parameters.AddWithValue("@ps", ps.Text);
                    command.Parameters.AddWithValue("@lid", librarianID);

                    command.ExecuteNonQuery(); // Update the data in the database

                    MessageBox.Show("Data updated successfully");
                    DisplayBooks(); // Refresh the DataGridView
                    name.Text = "";
                    phone.Text = "";
                    ps.Text = "";
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

        private void sizes_Click_1(object sender, EventArgs e)
        {
            Home hm = new Home();
            hm.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // User confirmed, proceed to log out
                LOGIN lg = new LOGIN();
                lg.Show();
                this.Hide();
            }
        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // User confirmed, proceed to log out
                LOGIN lg = new LOGIN();
                lg.Show();
                this.Hide();
            }
        }
    }
}
    

