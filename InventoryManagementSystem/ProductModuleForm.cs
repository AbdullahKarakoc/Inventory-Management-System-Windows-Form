using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboBox_category.Items.Clear();
            cm = new SqlCommand("SELECT catname FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboBox_category.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Button_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pprice,@pdescription,@pcategory)", con);
                    cm.Parameters.AddWithValue("@pname", textBox_pName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(textBox_pQuantify.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt32(textBox_pPrice.Text));
                    cm.Parameters.AddWithValue("@pdescription", textBox_pDescription.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboBox_category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textBox_pName.Clear();
            textBox_pQuantify.Clear();
            textBox_pPrice.Clear();
            textBox_pDescription.Clear();
            comboBox_category.Text = "";
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            Clear();
            Button_save.Enabled = true;
            button_update.Enabled = false;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbProduct SET pname = @pname, pqty=@pqty, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + label_pId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", textBox_pName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(textBox_pQuantify.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt32(textBox_pPrice.Text));
                    cm.Parameters.AddWithValue("@pdescription", textBox_pDescription.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboBox_category.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully updated.");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
