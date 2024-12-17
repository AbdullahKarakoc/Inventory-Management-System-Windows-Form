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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //to show subform form in mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void Button_user_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void Button_customer_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        private void Button_category_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm());
        }

        private void Button_product_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void Button_order_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderForm());
        }

        private void Button_chart_Click(object sender, EventArgs e)
        {
            openChildForm(new ChartForm());
        }
    }
}
