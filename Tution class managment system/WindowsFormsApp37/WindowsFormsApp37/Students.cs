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
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
            GetSubject();
            DisplayStudents();
        }

        private void Students_Load(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\Tution.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetSubject()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Snum from SubjectTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Snum", typeof(int));
            dt.Load(Rdr);

           stSubject.ValueMember = "Snum";
            stSubject.DataSource = dt;
            con.Close();
        }



        private void FetchSname()
        {
            con.Open();
            string query = "select * from SubjectTbl where Snum='" + stSubject.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stsubname.Text = dr["Sname"].ToString();
            }


            con.Close();
        }
        private void save_Click(object sender, EventArgs e)
        {
            if (Stname.Text == "" || stAddress.Text==""|| stGender.SelectedIndex == -1 || stPhone.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTb1(Stname,stAddress,stPhone,stSubject,stsubname, stGender) values(@StN,@STA,@STP,@STs,@STnS,@STG)", con);
                    cmd.Parameters.AddWithValue("@StN", Stname.Text);
                    cmd.Parameters.AddWithValue("@STA", stAddress.Text);
                    cmd.Parameters.AddWithValue("@STP", stPhone.Text);

                   
                    cmd.Parameters.AddWithValue("@STs", stSubject.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@STnS", stsubname.Text);
                    cmd.Parameters.AddWithValue("@STG", stGender.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added");
                    con.Close();
                    DisplayStudents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void DisplayStudents()
        {
            con.Open();
            string Query = "select * from StudentTb1 ";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }

        private void SubjectCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchSname();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }

        int Key = 0;
        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Stname.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            stAddress.Text = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            stPhone.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            stGender.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            stSubject.Text = StudentDGV.SelectedRows[0].Cells[5].Value.ToString();
            stsubname.Text = StudentDGV.SelectedRows[0].Cells[6].Value.ToString();

           

            if (Stname.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(StudentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Student to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from StudentTb1 where stnum=@StKey", con);
                    cmd.Parameters.AddWithValue("@StKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student Deleted");
                    con.Close();
                    DisplayStudents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (Stname.Text == "" || stAddress.Text == "" || stGender.SelectedIndex == -1 || stPhone.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update StudentTb1 set Stname=@StN ,stAddress=@STA ,stPhone=@STP ,stSubject=@STs ,stsubname=@STnS , stGender=@STG where stnum=StKey", con);
                    cmd.Parameters.AddWithValue("@StN", Stname.Text);
                    cmd.Parameters.AddWithValue("@STA", stAddress.Text);
                    cmd.Parameters.AddWithValue("@STP", stPhone.Text);


                    cmd.Parameters.AddWithValue("@STs", stSubject.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@STnS", stsubname.Text);
                    cmd.Parameters.AddWithValue("@STG", stGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@StKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated");
                    con.Close();
                    DisplayStudents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }
    }
}
