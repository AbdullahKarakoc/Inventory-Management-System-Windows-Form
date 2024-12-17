using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Button_save_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox_password.Text != textBox_rePassword.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save this user?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbUser(username,fullname,password,phone)VALUES(@username,@fullname,@password,@phone)", con);
                    cm.Parameters.AddWithValue("@username", textBox_username.Text);
                    cm.Parameters.AddWithValue("@fullname", textBox_fullname.Text);
                    cm.Parameters.AddWithValue("@password", textBox_password.Text);
                    cm.Parameters.AddWithValue("@phone", textBox_phone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully saved.");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            Clear();
            Button_save.Enabled = true;
            button_update.Enabled = false;
        }

        public void Clear()
        {
            textBox_username.Clear();
            textBox_fullname.Clear();
            textBox_password.Clear();
            textBox_rePassword.Clear();
            textBox_phone.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_password.Text != textBox_rePassword.Text)
                {
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update this user?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbUser SET fullname = @fullname, password=@password, phone=@phone WHERE username LIKE '" + textBox_username.Text + "' ", con);
                    cm.Parameters.AddWithValue("@fullname", textBox_fullname.Text);
                    cm.Parameters.AddWithValue("@password", textBox_password.Text);
                    cm.Parameters.AddWithValue("@phone", textBox_phone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully updated.");
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
