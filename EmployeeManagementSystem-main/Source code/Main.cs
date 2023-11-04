using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Church_MS
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            this.Close();
            obj.Show();
        }

        private void delEntry_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Update obj = new Update();
            this.Hide();
            obj.Show();
        }

        private void listEntry_Click(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEntry obj = new UpdateEntry();
            obj.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtboxMobNo.Text == "" || txtboxEmpName.Text == "" || txtboxEmpID.Text == "" || txtboxDept.Text == "" || txtboxBldGrp.Text == "" || txtboxAge.Text == "")
                {
                    MessageBox.Show("You have entered insufficient details.. :(\nPlease try again.");
                    txtboxMobNo.Text = ""; txtboxEmpName.Text = ""; txtboxEmpID.Text = ""; txtboxDept.Text = ""; txtboxBldGrp.Text = ""; txtboxAge.Text = "";
                }
                else
                {
                    bool validName = false;
                    List<string> Name = new List<string>();
                    Name=txtboxEmpName.Text.Split(' ').ToList();
                    foreach(string s in Name)
                    {
                        if (s.All(char.IsLetter))
                        {
                            validName = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    string connectionString = "Data Source=EmpMS.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        if (!(txtboxMobNo.Text).All(char.IsDigit) || !((txtboxMobNo.Text).Count() == 10)) { MessageBox.Show($"You have entered invalid input for Mobile Number...{txtboxMobNo.Text}.\nPlease try again."); txtboxMobNo.Text = ""; }
                        else if (!(txtboxEmpID.Text).All(char.IsDigit)) { MessageBox.Show($"You have entered invalid input for Employee ID...{txtboxEmpID.Text}.\nPlease try again."); txtboxEmpID.Text = ""; }
                        else if (!(txtboxAge.Text).All(char.IsDigit)) { MessageBox.Show($"You have entered invalid input for Employee Age...{txtboxAge.Text}.\nPlease try again."); txtboxAge.Text = ""; }
                        else if (validName==false) { MessageBox.Show($"You have entered invalid input for Employee Name...{txtboxEmpName.Text}.\nPlease try again."); txtboxEmpName.Text = ""; }
                        else if (!(txtboxDept.Text).All(char.IsLetter)) { MessageBox.Show($"You have entered invalid input for Employee Department...{txtboxDept.Text}.\nPlease try again."); txtboxDept.Text = ""; }
                        else 
                        {
                            string addEntryQuery = "INSERT INTO EMPMS (EmpID,Name,Age,Department,BloodGroup,MobileNo) VALUES (@EmpID,@Name,@Age,@Department,@BloodGroup,@MobileNo)";
                            SQLiteCommand command = new SQLiteCommand(addEntryQuery, connection);
                            command.Parameters.AddWithValue("@EmpID", txtboxEmpID.Text);
                            command.Parameters.AddWithValue("@Name", txtboxEmpName.Text);
                            command.Parameters.AddWithValue("@Age", txtboxAge.Text);
                            command.Parameters.AddWithValue("@Department", txtboxDept.Text);
                            command.Parameters.AddWithValue("@BloodGroup", txtboxBldGrp.Text);
                            command.Parameters.AddWithValue("@MobileNo", txtboxMobNo.Text);
                            command.ExecuteNonQuery();
                            listView1.Items.Clear();
                            MessageBox.Show("Record has been successfully added :)");
                            txtboxMobNo.Text = ""; txtboxEmpName.Text = ""; txtboxEmpID.Text = ""; txtboxDept.Text = ""; txtboxBldGrp.Text = ""; txtboxAge.Text = "";
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"You have encountered an error {ex}");
                txtboxMobNo.Text = ""; txtboxEmpName.Text = ""; txtboxEmpID.Text = ""; txtboxDept.Text = ""; txtboxBldGrp.Text = ""; txtboxAge.Text = "";
            }
        }
    }
}
