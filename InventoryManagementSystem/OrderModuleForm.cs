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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dataGridView_customer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT(cid,cname) LIKE '%" + textBox_searchCustomer.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView_customer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView_product.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + textBox_searchProduct.Text + "%'", con);
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

        private void textBox_searchCustomer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textBox_searchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt32(numericUpDown_qty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown_qty.Value = numericUpDown_qty.Value - 1;
                return;
            }
            if (Convert.ToInt32(numericUpDown_qty.Value) > 0)
            {
                int total = Convert.ToInt32(textBox_price.Text) * Convert.ToInt32(numericUpDown_qty.Value);
                textBox_total.Text = total.ToString();
            }

        }

        private void dataGridView_customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_cId.Text = dataGridView_customer.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox_cName.Text = dataGridView_customer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dataGridView_product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_pId.Text = dataGridView_product.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox_pName.Text = dataGridView_product.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox_price.Text = dataGridView_product.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void Button_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_cId.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textBox_pId.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(odate, pid, cid, qty, price, total)VALUES(@odate, @pid, @cid, @qty, @price, @total)", con);
                    cm.Parameters.AddWithValue("@odate", dateTimePicker_order.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(textBox_pId.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt32(textBox_cId.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(numericUpDown_qty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(textBox_price.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(textBox_total.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully inserted.");


                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty) WHERE pid LIKE '" + textBox_pId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt32(numericUpDown_qty.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textBox_cId.Clear();
            textBox_cName.Clear();

            textBox_pId.Clear();
            textBox_pName.Clear();

            textBox_price.Clear();
            numericUpDown_qty.Value = 0;
            textBox_total.Clear();
            dateTimePicker_order.Value = DateTime.Now;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid='" + textBox_pId.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

    }
}
