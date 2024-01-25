using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Library_Management_System
{
    public partial class loading : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public loading()
        {
            InitializeComponent();
            Region=System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0,0,Width,Height,25,25));
            circularProgressBar1.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar1.Value+=1;
            circularProgressBar1.Text= circularProgressBar1.Value.ToString()+"%";
            if(circularProgressBar1.Value == 100 )
            {
                timer1.Enabled= false;
                LOGIN lg = new LOGIN();
                lg.Show();
                this.Hide();
            }
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}