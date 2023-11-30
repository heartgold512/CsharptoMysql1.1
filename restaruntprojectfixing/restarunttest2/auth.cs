using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Net.Security;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace restarunttest2
{
    public partial class auth : Form
    {
        private bool isMaximized = false;
        MySqlConnection connection = new MySqlConnection("Server=localhost; Port=3306; Database=restrauntdb2;User=root;Password=Jonathandarth512?;");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable dt;

        public auth()
        {
            InitializeComponent();
            LoadTableNames();
        }
        private void LoadTableNames()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection("Server=localhost; Port=3306; Database=restrauntdb2;User=root;Password=Jonathandarth512?;"))
                {

                    connection.Open();

                    // Assuming you have a query to get table names from the database
                    DataTable tableNames = connection.GetSchema("Tables");
                    MySqlCommand cmd = new MySqlCommand("SHOW TABLES", connection);

                    foreach (DataRow row in tableNames.Rows)
                    {

                        string tableName = row["TABLE_NAME"].ToString();
                        comboBox1.Items.Add(tableName);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //be able to select tables from this combobox
            string selectedTableName = comboBox1.SelectedItem.ToString();
            LoadTableDataIntoDataGridView(selectedTableName);

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //add a method to expand maxise and minimise on clicking in a corner or something
        }

        private void LoadTableDataIntoDataGridView(string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost; Port=3306; Database=restrauntdb2;User=root;Password=Jonathandarth512?;"))
            {
                connection.Open();

                // Select all columns from the selected table
                string query = $"SELECT * FROM {tableName}";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);




                // Display the data in the DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E) //this is stupid atm
            {
                // Toggle maximized mode
                ToggleMaximizedMode();
            }
        }
        private void ToggleMaximizedMode()
        {
            if (isMaximized)
            {
                // Restore original size
                dataGridView1.Dock = DockStyle.None;
                isMaximized = false;
            }
            else
            {
                // Maximize the data grid
                dataGridView1.Dock = DockStyle.Fill;
                isMaximized = true;
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {//commit button 
            using (MySqlConnection connection = new MySqlConnection("Server=localhost; Port=3306; Database=restrauntdb2;User=root;Password=Jonathandarth512?;"))
            {
                connection.Open();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM user", connection))
                {
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);

                    // Get the changes from the DataGridView
                    DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();

                    if (changes != null)
                    {
                        // Update the changes to the database
                        adapter.Update(changes);
                        ((DataTable)dataGridView1.DataSource).AcceptChanges();
                    }
                }
            }

            MessageBox.Show("Changes committed successfully .");
        }
  

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        { // tasks may be assigned here to complete on the database for example

        }
    }
}