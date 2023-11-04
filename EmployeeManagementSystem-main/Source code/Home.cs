using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Church_MS
{
    public partial class Home : Form
    {
        public Home()
        {
            try
            {
                string connectionString = "Data Source=EmpMS.db;Version=3;";
                if (!File.Exists("EmpMS.db"))
                {
                    SQLiteConnection.CreateFile("EmpMS.db");
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string createTableQuery = "CREATE TABLE IF NOT EXISTS EMPMS (EmpID TEXT PRIMARY KEY, Name TEXT, Age INTEGER, Department varchar(20),BloodGroup TEXT,MobileNo TEXT)";
                        SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error in creation of database...{ex}"); }
            InitializeComponent();
            listView1.OwnerDraw = true;
            listView1.DrawItem += new DrawListViewItemEventHandler(listView1_DrawItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            int n;
            listView1.Items.Clear();
            string search;
            string searchEntryQuery = "";
            search = textBox1.Text;
            bool isNumeric = int.TryParse(search, out n);
            if (isNumeric == false)
            {
                MessageBox.Show("Enter an ID which is an integer please.");
                textBox1.Text = "";
            }
            else
            {
                try
                {
                    string connectionString = "Data Source=EmpMS.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        searchEntryQuery = $"SELECT * FROM EMPMS WHERE EmpID =\'{n}\'";
                        SQLiteCommand command = new SQLiteCommand(searchEntryQuery, connection);
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
                            flag = true;
                        }
                        if (flag == false) { MessageBox.Show("Sorry, there is no record matching that ID."); }
                    }
                }
                catch (Exception) { MessageBox.Show($"SQL Query is {searchEntryQuery}"); }
            }
        }

        private void addEntry_Click(object sender, EventArgs e)
        {
            Main obj = new Main();
            obj.Show();
            this.Close();
        }

        private void delEntry_Click(object sender, EventArgs e)
        {
            Update obj = new Update();
            obj.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEntry obj = new UpdateEntry();
            obj.Show();
            this.Close();
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; // Let the default drawing occur

            // Draw grid lines for rows and column headers
            using (Pen gridPen = new Pen(Color.LightGray))
            {
                System.Windows.Forms.ListView listView = (System.Windows.Forms.ListView)sender;

                // Draw horizontal grid lines for rows
                for (int i = 0; i < listView.Columns.Count; i++)
                {
                    e.Graphics.DrawLine(gridPen, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                }

                // Draw vertical grid lines for column headers
                if (e.ItemIndex == -1)
                {
                    int x = 0;
                    foreach (ColumnHeader col in listView.Columns)
                    {
                        e.Graphics.DrawLine(gridPen, x, e.Bounds.Top, x, e.Bounds.Bottom);
                        x += col.Width;
                    }
                }
            }
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true; // Let the default drawing occur

            // Draw vertical grid lines for column headers
            using (Pen gridPen = new Pen(Color.LightGray))
            {
                System.Windows.Forms.ListView listView = (System.Windows.Forms.ListView)sender;
                int x = e.Bounds.Left;

                // Draw vertical grid lines for each column header
                foreach (ColumnHeader col in listView.Columns)
                {
                    e.Graphics.DrawLine(gridPen, x, e.Bounds.Top, x, e.Bounds.Bottom);
                    x += col.Width;
                }
            }
        }
    }
}
