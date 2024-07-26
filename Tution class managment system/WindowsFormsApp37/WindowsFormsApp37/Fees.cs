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

namespace WindowsFormsApp37
{
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            DisplayFees();
            GetStudent();
            GetSubject();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Teachers n = new Teachers();
            n.Show();
            n.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetStudent()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select stnum from StudentTb1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Tnum", typeof(int));
            dt.Load(Rdr);

            stcb.ValueMember = "stnum";
            stcb.DataSource = dt;
            con.Close();
        }

        private void Fetchstname()
        {
            con.Open();
            string query = "select * from StudentTb1 where stnum='" + stcb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stnametb.Text = dr["stname"].ToString();
            }


            con.Close();
        }

        private void GetSubject()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Snum from SubjectTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Snum", typeof(int));
            dt.Load(Rdr);

            subcb.ValueMember = "Snum";
            subcb.DataSource = dt;
            con.Close();
        }

        private void FetchSname()
        {
            con.Open();
            string query = "select * from SubjectTbl where Snum='" + subcb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                subtb.Text = dr["Sname"].ToString();
            }


            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\Tution.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayFees()
        {
            con.Open();
            string Query = "select * from  FeesTbl";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            payDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Reset()
        {
            Amounttb.Text = "";
            stnametb.Text = "";
            subtb.Text = "";

        }
        private void paytb_Click(object sender, EventArgs e)
        {
            if (subtb.Text == "" || stnametb.Text == "" || Amounttb.Text=="")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {

                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from FeesTbl where fstId='" + stcb.SelectedValue.ToString() + "' and fcourseId='" + subcb.SelectedValue.ToString() + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Fees already paid");
                        Reset();
                    }
                    else
                    { 
                     SqlCommand cmd = new SqlCommand("insert into FeesTbl(fstId,fstName,fcourseId,fcourseNmae,fAmount,fdate) values(@FSID,@FSTN,@FCID,@FCN,@FA,@FD)", con);
                    cmd.Parameters.AddWithValue("@FSID", stcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@FSTN", stnametb.Text);
                    cmd.Parameters.AddWithValue("@FCID", subcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@FCN", subtb.Text);
                    cmd.Parameters.AddWithValue("@FA", Amounttb.Text);
                    cmd.Parameters.AddWithValue("@FD", paydate.Value.Date);

                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfull payment");
                    }

                   
                    con.Close();
                    DisplayFees();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void stcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fetchstname();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }

        private void subcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchSname();
        }

        private void Resettb_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }
    }
}
