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
using System.IO;
using System.Reflection;

namespace WindowsFormsApp37
{
    public partial class Teachers : Form
    {
        SqlConnection con;
        public Teachers()
        {
            InitializeComponent();
            String directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+directory+@"\Tution.mdf;Integrated Security=True;Connect Timeout=30");

            DisplayTeachers();
        }
        private void DisplayTeachers()
        {
            con.Open();
            string Query ="select * from  TeachersTbl"; 
            
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TeachersDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Tsave_Click(object sender, EventArgs e)
        {
            if (Tname.Text == "" || Tqulif.SelectedIndex == -1 || Tgender.SelectedIndex == -1 || Tphone.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TeachersTbl(Tname,Tqulifications,Tgender,Tphone)values(@TN,@TQ,@TG,@TP)", con);
                    cmd.Parameters.AddWithValue("@TN", Tname.Text);
                    cmd.Parameters.AddWithValue("@TQ", Tqulif.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TG", Tgender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TP", Tphone.Text);
                    cmd.ExecuteNonQuery();
                   MessageBox.Show("Teacher Added");
                    con.Close();
                    DisplayTeachers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        int Key=0;

        private void TeachersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Tname.Text = TeachersDGV.SelectedRows[0].Cells[1].Value.ToString();
            Tqulif.SelectedItem = TeachersDGV.SelectedRows[0].Cells[2].Value.ToString();
            Tgender.SelectedItem = TeachersDGV.SelectedRows[0].Cells[3].Value.ToString();
            Tphone.Text=TeachersDGV.SelectedRows[0].Cells[4].Value.ToString(); 
            if (Tname.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(TeachersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Tedit_Click(object sender, EventArgs e)
        {
            if (Tname.Text == "" || Tqulif.SelectedIndex == -1 || Tgender.SelectedIndex == -1 || Tphone.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update  TeachersTbl set Tname=@TN,Tqulifications=@TQ,Tgender=@TG,Tphone=@TP where Tnum=@Tkey", con);
                    cmd.Parameters.AddWithValue("@TN", Tname.Text);
                    cmd.Parameters.AddWithValue("@TQ", Tqulif.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TG", Tgender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TP", Tphone.Text);
                    cmd.Parameters.AddWithValue("@Tkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Added");
                    con.Close();
                    DisplayTeachers();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Tdelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Teacher to be deleted");
            }
            else {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from TeachersTbl where Tnum=@TKey", con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Teacher Deleted");
                    con.Close();
                    DisplayTeachers();
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

        private void label1_Click(object sender, EventArgs e)
        {
            Home n = new Home();
            n.Show();
            this.Hide();
        }
    }
    }

