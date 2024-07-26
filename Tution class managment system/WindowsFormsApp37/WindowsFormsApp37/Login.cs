using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp37
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            usernametb.Text = "";
            passwrdtb.Text = "";

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (usernametb.Text == "username" || passwrdtb.Text == "")
            {
                MessageBox.Show("Missing information");
                usernametb.Text = "";
                passwrdtb.Text = "";

            }
            else if (usernametb.Text == "Zigma" && passwrdtb.Text == "password")
            {
                Home n = new Home();
                n.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("wrong username or/And password");
                usernametb.Text = "";
                passwrdtb.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void passwrdtb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
