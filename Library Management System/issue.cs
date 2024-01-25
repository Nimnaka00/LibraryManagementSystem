using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Library_Management_System
{

    public partial class issue : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public issue()
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

        private void issue_Load(object sender, EventArgs e)
        {

            DisplayBooks();
        }
        private void DisplayBooks()
        {
            try
            {


                string query = "SELECT * FROM issue";
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
        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            ChangePanelOpacity(1);
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ChangePanelOpacity(120);
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MID.Text) || string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(BID.Text) || string.IsNullOrEmpty(BName.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO issue (MID, MName, BID, BName, issue_Date) VALUES (@MID, @name, @BID, @BName, @dt)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        // Assuming you have a variable named 'MID' for Member ID
                        command.Parameters.AddWithValue("@MID", MID.Text);

                        // Assuming 'name', 'phone', and 'ps' are the text boxes for member information
                        command.Parameters.AddWithValue("@name", name.Text);

                        // Assuming 'BID' and 'BName' are variables for Book ID and Book Name
                        command.Parameters.AddWithValue("@BID", BID.Text);
                        command.Parameters.AddWithValue("@BName", BName.Text);

                        // Assuming 'dt' is a DateTime variable for the issue date
                        command.Parameters.AddWithValue("@dt", dt.Value);

                        command.ExecuteNonQuery(); // Insert the data into the database

                        MessageBox.Show("Data inserted successfully");
                        DisplayBooks(); // Refresh the DataGridView;
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            try
            {
                conn.Open();

                int selectedRowIndex = bunifuCustomDataGrid1.SelectedRows[0].Index;
                int issueID = Convert.ToInt32(bunifuCustomDataGrid1.Rows[selectedRowIndex].Cells["INO"].Value);

                string query = "DELETE FROM issue WHERE INO=@issueID";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@issueID", issueID);

                    int rowsAffected = command.ExecuteNonQuery(); // Delete the record from the database

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully");
                        DisplayBooks(); // Refresh the DataGridView
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
            if (bunifuCustomDataGrid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to update.");
                return;
            }

            DataGridViewRow selectedRow = bunifuCustomDataGrid1.SelectedRows[0];
            int issueID = Convert.ToInt32(selectedRow.Cells["INO"].Value);

            try
            {
                conn.Open();

                string query = "UPDATE issue SET MID = @MID, MName = @name, BID = @BID, BName = @BName, issue_Date = @dt WHERE INO = @issueID";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    // Assuming you have text boxes for member information named 'name', 'BID', 'BName'
                    command.Parameters.AddWithValue("@MID", MID.Text);
                    command.Parameters.AddWithValue("@name", name.Text);
                    command.Parameters.AddWithValue("@BID", BID.Text);
                    command.Parameters.AddWithValue("@BName", BName.Text);
                    command.Parameters.AddWithValue("@dt", dt.Value);

                    // Assuming 'INO' is the primary key column of the 'issue' table
                    command.Parameters.AddWithValue("@issueID", issueID);

                    command.ExecuteNonQuery(); // Update the data in the database

                    MessageBox.Show("Data updated successfully");
                    DisplayBooks(); // Refresh the DataGridView
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




        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure a valid row is clicked
            {
                DataGridViewRow row = bunifuCustomDataGrid1.Rows[e.RowIndex];

                // Populate the input fields with the data from the selected row
                MID.Text = row.Cells["MID"].Value.ToString();
                name.Text = row.Cells["MName"].Value.ToString();
                BID.Text = row.Cells["BID"].Value.ToString();
                BName.Text = row.Cells["BName"].Value.ToString();
                dt.Text = row.Cells["issue_Date"].Value.ToString();
            }
        }

        private void sizes_Click(object sender, EventArgs e)
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

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
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
