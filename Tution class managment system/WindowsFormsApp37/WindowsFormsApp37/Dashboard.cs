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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountStudents();
            SumAmount();
            CountTeachers();
            CountSubjets();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\Tution.mdf;Integrated Security=True;Connect Timeout=30");

        private void CountStudents()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select  Count(*) from StudentTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            stnumtb.Text = dt.Rows[0][0].ToString()+"Students";
            con.Close();

        }

        private void SumAmount()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select  Sum(fAmount) from FeesTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Amounttbl.Text = dt.Rows[0][0].ToString() + "Rupees";
            con.Close();
        }

        private void CountTeachers()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select  Count(*) from TeachersTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            techerstbl.Text = dt.Rows[0][0].ToString() + "Instructors";
            con.Close();

        }

        private void CountSubjets()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select  Count(*) from SubjectTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            subtbl.Text = dt.Rows[0][0].ToString() + "Subjects";
            con.Close();

        }
        private void label1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }
    }
}
