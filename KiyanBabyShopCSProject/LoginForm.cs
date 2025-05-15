using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyanBabyShopCSProject
{
    public partial class LoginForm : Form
    {
        public bool succeeded;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'usersDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.usersDataSet.Users);
            panel2.Visible = false;
            panel2.Enabled = false;
            label7.Visible = false;
            label7.Enabled = false;
        }


        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel1.Enabled = false;
            label2.Visible = false;
            label2.Enabled = false;
            panel2.Visible = true;
            panel2.Enabled = true;
            label7.Visible = true;
            label7.Enabled = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Enabled = true;
            label2.Visible = true;
            label2.Enabled = true;
            panel2.Visible = false;
            panel2.Enabled = false;
            label7.Visible = false;
            label7.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            usersTableAdapter.FillByUserAndPass(usersDataSet.Users, txtUserName.Text, txtPass.Text);
            if (usersDataSet.Users.Rows.Count > 0)
            {
                succeeded = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز اشتباه میباشد.");
            }
        }
    }
}
