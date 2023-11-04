using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Church_MS
{
    public partial class UpdateEntry : Form
    {
        public UpdateEntry()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = String.Format("Enter the value of {0}",comboBox1.Text);
            textBox2.Text = "";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Close();
        }

        private void btnUpd_Click(object sender, EventArgs e)
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
                    string connectionString = "Data Source=EmpMS.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string updateEntryQuery = "UPDATE EMPMS SET Name=@Name, Age=@Age, Department=@Department, BloodGroup=@BloodGroup, MobileNo=@MobileNo WHERE EmpID=@EmpID";
                        SQLiteCommand command = new SQLiteCommand(updateEntryQuery, connection);
                        command.Parameters.AddWithValue("@EmpID", txtboxEmpID.Text);
                        command.Parameters.AddWithValue("@Name", txtboxEmpName.Text);
                        command.Parameters.AddWithValue("@Age", txtboxAge.Text);
                        command.Parameters.AddWithValue("@Department", txtboxDept.Text);
                        command.Parameters.AddWithValue("@BloodGroup", txtboxBldGrp.Text);
                        command.Parameters.AddWithValue("@MobileNo", txtboxMobNo.Text);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Record has been successfully updated :)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"You have encountered an error {ex}");
                txtboxMobNo.Text = ""; txtboxEmpName.Text = ""; txtboxEmpID.Text = ""; txtboxDept.Text = ""; txtboxBldGrp.Text = ""; txtboxAge.Text = "";
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Employee ID")
            {
                int n;
                bool isNumeric = int.TryParse(textBox2.Text, out n);
                if (isNumeric == true)
                {
                    try
                    {
                        //MessageBox.Show("Ivide ethunnundo?");
                        bool flag = false;
                        string connectionString = "Data Source=EmpMS.db;Version=3;";
                        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                        {
                            connection.Open();
                            string searchEntryQuery = $"SELECT * FROM EMPMS WHERE EmpID =\'{n}\'";
                            //MessageBox.Show($"SQL QUERY : {searchEntryQuery}");
                            SQLiteCommand command = new SQLiteCommand(searchEntryQuery, connection);
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                txtboxEmpID.Text = reader["EmpID"].ToString();
                                txtboxEmpName.Text = reader["Name"].ToString();
                                txtboxAge.Text = reader["Age"].ToString();
                                txtboxDept.Text = reader["Department"].ToString();
                                txtboxBldGrp.Text = reader["BloodGroup"].ToString();
                                txtboxMobNo.Text = reader["MobileNo"].ToString();
                                flag = true;
                            }
                            if (flag == false) { MessageBox.Show("Sorry, there is no record matching that ID.");
                                txtboxEmpID.Text = "";
                                txtboxEmpName.Text = "";
                                txtboxAge.Text = "";
                                txtboxDept.Text = "";
                                txtboxBldGrp.Text = "";
                                txtboxMobNo.Text = "";
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show($"You have encountered an error {ex}"); }
                }
                else { MessageBox.Show("You have entered a string instead of an integer."); }
            }
            else if (comboBox1.Text == "Employee Name")
            {
                bool isNumeric = (textBox2.Text).Any(char.IsDigit);
                if (isNumeric == false)
                {
                    try
                    {
                        bool flag = false;
                        string connectionString = "Data Source=EmpMS.db;Version=3;";
                        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                        {
                            connection.Open();
                            string searchEntryQuery = $"SELECT * FROM EMPMS WHERE Name =\'{textBox2.Text}\'";
                            SQLiteCommand command = new SQLiteCommand(searchEntryQuery, connection);
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                txtboxEmpID.Text = reader["EmpID"].ToString();
                                txtboxEmpName.Text = reader["Name"].ToString();
                                txtboxAge.Text = reader["Age"].ToString();
                                txtboxDept.Text = reader["Department"].ToString();
                                txtboxBldGrp.Text = reader["BloodGroup"].ToString();
                                txtboxMobNo.Text = reader["MobileNo"].ToString();
                                flag = true;
                            }
                            if (flag == false) { MessageBox.Show("Sorry, there is no record matching that ID.");
                                txtboxEmpID.Text = "";
                                txtboxEmpName.Text = "";
                                txtboxAge.Text = "";
                                txtboxDept.Text = "";
                                txtboxBldGrp.Text = "";
                                txtboxMobNo.Text = "";
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show($"You have encountered an error {ex}"); }
                }
                else { MessageBox.Show("You have entered a string instead of an integer."); }
            }
            else { MessageBox.Show("Something is wrong bro");
                txtboxEmpID.Text = "";
                txtboxEmpName.Text = "";
                txtboxAge.Text = "";
                txtboxDept.Text = "";
                txtboxBldGrp.Text = "";
                txtboxMobNo.Text = "";
            }

        }

        private void addEntry_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            this.Close();
            obj.Show();
        }

        private void delEntry_Click(object sender, EventArgs e)
        {
            Update obj = new Update();
            this.Close();
            obj.Show();
        }
    }
}
