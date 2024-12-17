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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView_product.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + textBox_search.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView_product.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            ProductModuleForm formModule = new ProductModuleForm();
            formModule.Button_save.Enabled = true;
            formModule.button_update.Enabled = false;
            formModule.ShowDialog();
            LoadProduct();
        }

        private void dataGridView_product_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView_product.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.label_pId.Text = dataGridView_product.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.textBox_pName.Text = dataGridView_product.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.textBox_pQuantify.Text = dataGridView_product.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.textBox_pPrice.Text = dataGridView_product.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.textBox_pDescription.Text = dataGridView_product.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.comboBox_category.Text = dataGridView_product.Rows[e.RowIndex].Cells[6].Value.ToString();

                productModule.Button_save.Enabled = false;
                productModule.button_update.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dataGridView_product.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadProduct();
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
