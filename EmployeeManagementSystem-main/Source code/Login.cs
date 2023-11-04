using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Church_MS
{
    public partial class Login : Form
    {
        string userName = "", passWord = "";
        public Login()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userName = textBox1.Text;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            passWord = textBox2.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Home form = new Home();
                if (userName == "admin" && passWord == "admin")
                {
                    this.Hide();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login Credentials", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception) {  }
        }
    }
}
