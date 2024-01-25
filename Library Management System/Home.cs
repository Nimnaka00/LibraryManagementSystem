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

namespace Library_Management_System
{
    public partial class Home : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public Home()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Home_Load(object sender, EventArgs e)
        {

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Book bk = new Book();
            bk.Show();
            this.Hide();
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            hm.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            issue iss = new issue();
            iss.Show();
            this.Hide();
        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {
            issue iss = new issue();
            iss.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Return rn= new Return();
            rn.Show();
            this.Hide();
        }

        private void bunifuCustomLabel5_Click(object sender, EventArgs e)
        {
            Return rn = new Return();
            rn.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Member mb = new Member();   
            mb.Show();
            this.Hide();
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {
            Member mb = new Member();
            mb.Show();
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

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
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
