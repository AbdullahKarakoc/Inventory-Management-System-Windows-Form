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
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
        }

        void FillChartPrice()
        {
            SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select pname,pprice from tbProduct", con);
            da.Fill(dt);
            chart1.DataSource = dt;
            con.Close();

            chart1.Series["pprice"].XValueMember = "pname";
            chart1.Series["pprice"].YValueMembers = "pprice";
            chart1.Titles.Add("Product Price");
        }

        void FillChartQuantity()
        {
            SqlConnection con = new SqlConnection(@"Data Source=ABDULLAH\SQLEXPRESS;Initial Catalog=IMSdb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select pname,pqty from tbProduct", con);
            da.Fill(dt);
            chart2.DataSource = dt;
            con.Close();

            chart2.Series["pqty"].XValueMember = "pname";
            chart2.Series["pqty"].YValueMembers = "pqty";
            chart2.Titles.Add("Product Quantity");
        }


        private void ChartForm_Load(object sender, EventArgs e)
        {
            FillChartPrice();
            FillChartQuantity();
        }


    }
}
