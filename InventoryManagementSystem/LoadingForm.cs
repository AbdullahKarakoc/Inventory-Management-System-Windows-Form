using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            timer1.Start();
        }
        int startpoint = 0;
        int endpoint = 100;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            endpoint -= 1;
            progressBar1.Value = startpoint;
            progressBar2.Value = endpoint;
            progressBar3.Value = startpoint;
            progressBar4.Value = endpoint;
            progressBar5.Value = startpoint;
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                progressBar3.Value = 0;
                progressBar4.Value = 0;
                progressBar5.Value = 0;
                timer1.Stop();
                MainForm main = new MainForm();
                this.Hide();
                main.Show();
            }
        }
    }
}
