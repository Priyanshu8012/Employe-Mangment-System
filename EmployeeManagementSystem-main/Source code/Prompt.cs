using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Church_MS
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Main obj= new Main();
            obj.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("Enter the value for \n{0}", comboBox1.Text);
            textBox2.Text = "";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //Main obj = new Main();
            try
            {
                string connectionString = "Data Source=EmpMS.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    if (comboBox1.Text == "Employee ID")
                    {
                        bool isNumeric = int.TryParse(textBox2.Text, out int n);
                        if (isNumeric)
                        {
                            string delEntryQuery = $"DELETE FROM EMPMS WHERE EmpID = \'{n}\'";
                            SQLiteCommand command = new SQLiteCommand(delEntryQuery, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Record has been successfully deleted :)");
                            textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Please enter an appropriate ID which is an integer.");
                            textBox2.Text = "";
                        }
                    }
                    else if (comboBox1.Text == "Employee Name")
                    {
                        bool containsInt = false;
                        containsInt = (textBox2.Text).Any(char.IsDigit);
                        if (containsInt == false)
                        {
                            string delEntryQuery = $"DELETE FROM EMPMS WHERE Name = \'{textBox2.Text}\'";
                            SQLiteCommand command = new SQLiteCommand(delEntryQuery, connection);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Record has been successfully deleted :)");
                            textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("You have entered a digit in Employee Name.\nPlease add an appropriate Employee Name.");
                            textBox2.Text = "";
                        }
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Deletion scene aayi"); }
        }

        private void addEntry_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Close();
            obj.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEntry obj = new UpdateEntry();
            this.Close();
            obj.Show();
        }

        private void listEntry_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Close();
            obj.Show();
        }

        private void delEntry_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listEntry_Click_1(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string connectionString = "Data Source=EmpMS.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string listEntryQuery = "SELECT EmpID,Name,Age,Department,BloodGroup,MobileNo FROM EMPMS";
                    SQLiteCommand command = new SQLiteCommand(listEntryQuery, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["EmpID"].ToString());
                        item.SubItems.Add(reader["Name"].ToString());
                        item.SubItems.Add(reader["Age"].ToString());
                        item.SubItems.Add(reader["Department"].ToString());
                        item.SubItems.Add(reader["BloodGroup"].ToString());
                        item.SubItems.Add(reader["MobileNo"].ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception) { listView1.Items.Clear(); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}
