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
    public partial class Subject : Form
    {
        public Subject()
        {
            InitializeComponent();
            GetTeachers();
            DisplaySubjects();
        }

        private void GetTeachers()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Tnum from TeachersTbl", con);
            SqlDataReader Rdr;
             Rdr=   cmd.ExecuteReader();
          
            DataTable dt = new DataTable();
            dt.Columns.Add("Tnum", typeof(int));
            dt.Load(Rdr);
            
            TCb.ValueMember = "Tnum";
            TCb.DataSource = dt;
            con.Close();
        }

        private void FetchTname()
        {
            con.Open();
            string query = "select * from TeachersTbl where Tnum='" + TCb.SelectedValue.ToString()+"';";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Tname.Text = dr["Tname"].ToString();
            }

        
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\asus\Documents\Tution.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplaySubjects()
        {
            con.Open();
            string Query = "select * from  SubjectTbl";

            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
         subjectsDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Subsave_Click(object sender, EventArgs e)
        {
            {
                if (subname.Text == "" || TCb.SelectedIndex == -1 || Tname.Text == "" || price.Text == "")
                {
                    MessageBox.Show("Missing information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into  SubjectTbl (Sname,STid,STname,sprice) Values(@SN,@STID,@STN,@SP)", con);
                        cmd.Parameters.AddWithValue("@SN", subname.Text);
                        cmd.Parameters.AddWithValue("@STID", TCb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@STN", Tname.Text);
                        cmd.Parameters.AddWithValue("@SP", price.Text);
                        
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Subject Added");
                        con.Close();
                        DisplaySubjects();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

     

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();

        }
        private void TCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FetchTname();
        }

        int Key = 0;
        private void subjectsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           subname.Text = subjectsDGV.SelectedRows[0].Cells[1].Value.ToString();
            TCb.SelectedValue = subjectsDGV.SelectedRows[0].Cells[2].Value.ToString();
           Tname.Text = subjectsDGV.SelectedRows[0].Cells[3].Value.ToString();
            price.Text= subjectsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if ( subname.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(subjectsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void subdelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Subject to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SubjectTbl where Snum=@SKey", con);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Subject Deleted");
                    con.Close();
                    DisplaySubjects();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void subedit_Click(object sender, EventArgs e)
        {
            {
                if (subname.Text == "" || TCb.SelectedIndex == -1 || Tname.Text == "" || price.Text == "")
                {
                    MessageBox.Show("Missing information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Update SubjectTbl set Sname=@SN,STid=@STID,STname=@STN,sprice=@SP where Snum=@Skey ", con);
                        cmd.Parameters.AddWithValue("@SN", subname.Text);
                        cmd.Parameters.AddWithValue("@STID", TCb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@STN", Tname.Text);
                        cmd.Parameters.AddWithValue("@SP", price.Text);
                        cmd.Parameters.AddWithValue("@SKey", Key);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Subject Updated");
                        con.Close();
                        DisplaySubjects();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }
    }
}
