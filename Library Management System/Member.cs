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
using System.Security.Policy;

namespace Library_Management_System
{
    public partial class Member : Form
    {
    
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public Member()
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

        private void Mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void ChangePanelOpacity(int alphaValue)
        {
            Color panelColor = panel1.BackColor;
            panel1.BackColor = Color.FromArgb(alphaValue, panelColor.R, panelColor.G, panelColor.B);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            ChangePanelOpacity(1);
        }
        private void DisplayMembers()
        {
            try
            {
                string query = "SELECT * FROM members"; // Query for members table
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
        }

        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuCustomDataGrid1.Rows[e.RowIndex];

                // Populate the input fields with the data from the selected row
                name.Text = row.Cells["MName"].Value.ToString();
                address.Text = row.Cells["MAddress"].Value.ToString();
                phone.Text = row.Cells["MPhone"].Value.ToString();
                age.Text = row.Cells["MAge"].Value.ToString();
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ChangePanelOpacity(120);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || address.Text == "" || phone.Text == "" || age.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO members (MName, MAddress, MPhone, MAge) VALUES (@name, @address, @phone, @age)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@name", name.Text);
                        command.Parameters.AddWithValue("@address", address.Text);
                        command.Parameters.AddWithValue("@phone", phone.Text);
                        command.Parameters.AddWithValue("@age", age.Text);

                        command.ExecuteNonQuery(); // Insert the data into the database

                        MessageBox.Show("Data inserted successfully");
                        DisplayMembers(); // Refresh the DataGridView
                        name.Text = "";
                        address.Text = "";
                        phone.Text = "";
                        age.Text = "";
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



        private void bunifuCustomLabel6_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangePanelOpacity(1);
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

        private void bunifuCustomLabel7_Click(object sender, EventArgs e)
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

        private void Member_Load(object sender, EventArgs e)
        {
            DisplayMembers();
        }

        private void bunifuCustomDataGrid1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure a valid row is clicked
            {
                DataGridViewRow row = bunifuCustomDataGrid1.Rows[e.RowIndex];

                // Populate the input fields with the data from the selected row
                name.Text = row.Cells["MName"].Value.ToString();
                address.Text = row.Cells["MAddress"].Value.ToString();
                phone.Text = row.Cells["MPhone"].Value.ToString();
                age.Text = row.Cells["MAge"].Value.ToString();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            // Get the selected record's ID from the DataGridView
            int selectedRowIndex = bunifuCustomDataGrid1.SelectedRows[0].Index;
            int memberID = Convert.ToInt32(bunifuCustomDataGrid1.Rows[selectedRowIndex].Cells["MID"].Value);

            try
            {
                conn.Open();

                string query = "DELETE FROM members WHERE MID=@id";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", memberID);

                    int rowsAffected = command.ExecuteNonQuery(); // Delete the record from the database

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        DisplayMembers(); // Refresh the DataGridView
                        name.Text = "";
                        address.Text = "";
                        phone.Text = "";
                        age.Text = "";

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

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(name.Text)) // Check the value of the TextBox
            //{
            //    MessageBox.Show("Please select a record to update.");
            //    return;
            //}

            // Check if a row is selected in the DataGridView
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            // Get the selected record's ID from the DataGridView
            int selectedRowIndex = bunifuCustomDataGrid1.SelectedRows[0].Index;
            int memberID = Convert.ToInt32(bunifuCustomDataGrid1.Rows[selectedRowIndex].Cells["MID"].Value);

            try
            {
                conn.Open();

                string query = "UPDATE members SET MName=@name, MAddress=@address, MPhone=@phone, MAge=@age where MID=@id";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@name", name.Text);
                    command.Parameters.AddWithValue("@address", address.Text);
                    command.Parameters.AddWithValue("@phone", phone.Text);
                    command.Parameters.AddWithValue("@age", age.Text);
                    command.Parameters.AddWithValue("@id", memberID);

                    command.ExecuteNonQuery(); // Update the data in the database

                    MessageBox.Show("Data updated successfully");
                    DisplayMembers(); // Refresh the DataGridView
                    name.Text = "";
                    address.Text = "";
                    phone.Text = "";
                    age.Text = "";
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

    }
}
