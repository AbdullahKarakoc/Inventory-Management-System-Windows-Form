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
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dataGridView_user.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView_user.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }



        private void Button_add_Click(object sender, EventArgs e)
        {
            UserModuleForm formModule = new UserModuleForm();
            formModule.Button_save.Enabled = true;
            formModule.button_update.Enabled = false;
            formModule.ShowDialog();
            LoadUser();
        }

        private void dataGridView_user_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView_user.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.textBox_username.Text = dataGridView_user.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.textBox_fullname.Text = dataGridView_user.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.textBox_password.Text = dataGridView_user.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.textBox_rePassword.Text = dataGridView_user.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.textBox_phone.Text = dataGridView_user.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.Button_save.Enabled = false;
                userModule.button_update.Enabled = true;
                userModule.textBox_username.Enabled = false;
                userModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + dataGridView_user.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadUser();
        }


    }
}
