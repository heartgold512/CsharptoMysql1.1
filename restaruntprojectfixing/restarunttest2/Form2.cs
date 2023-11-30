using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Remoting.Messaging;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.AccessControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace restarunttest2
{
    public partial class account_creation : Form
    {
        private MySqlConnection connection;

        public account_creation()
        {
            InitializeComponent();
            InitializeTextBoxes();
            InitializeDatabase();
          //  LoadTableNames(); // for the dropbox
          //  LoadData();
        }
        private void InitializeDatabase()
        {
           //conection string here string connectiondb = 
            connection = new MySqlConnection(connectiondb);
           
            //we may need other initialisations
        }
       
        
        

private void InitializeTextBoxes()
        {
            SetPlaceholderText(textBox1, "Username");
            SetPlaceholderText(PasswordBox, "Password");
        }

        private void account_creation_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss   dd-MMM-yyyy ");


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
              //add a  clear method to clear5 the input text upon clicking and then add a click off method to readd the original text

            // code for handling the TextChanged event of textBox1
        }
        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {
            //  code for handling the TextChanged event of textBox1
        }
        private int GetNextUserId()
        {
            int nextUserId = 4; //set limit to 4 bits

            try
            {
                connection.Open();

                //here the details of the user id may be changed ie tio start incrementing from 8 bit instead
                string query = "SELECT MAX(user_id) + 1 FROM user";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        nextUserId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next user_id: " + ex.Message);
            }
           

            return nextUserId;
        }
    


   
    public void Createbtn_Click(object sender, EventArgs e)
    {

        string username = textBox1.Text;
        string password = PasswordBox.Text;
      
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                username == "Username" || password == "Password")
            {
                MessageBox.Show("Please enter a valid username and password.");
                return;
            }
            try
            {
              
                int nextUserId = GetNextUserId();

                // Assuming your table is named 'user' and has columns 'username' and 'password'
                string query = "INSERT INTO user (user_id , username, password) VALUES (@user_id, @username, @password)";
               
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@user_id", nextUserId);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Account created successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating account: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    

    

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss   dd-MMM-yyyy ");
        }

        private void SetPlaceholderText(System.Windows.Forms.TextBox textBox1, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = placeholder;
                textBox1.ForeColor = SystemColors.GrayText;
            }
            else if (textBox1.Text == placeholder)
            {
                textBox1.ForeColor = SystemColors.WindowText; // Set the text color to default
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            SetPlaceholderText((System.Windows.Forms.TextBox)sender, "Username");
        }

        private void PasswordBox_Leave(object sender, EventArgs e)
        {
            SetPlaceholderText((System.Windows.Forms.TextBox)sender, "Password");
            if (PasswordBox.Text == string.Empty)
            { //needs changing and thinking
                // Set the PasswordBox text to some "original text"
                PasswordBox.Text = "Password"; // Replace "Original Text" with the actual text you want to set
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

            if (textBox1.Text == "Username")
            {
                textBox1.Text = string.Empty;
                textBox1.ForeColor = SystemColors.WindowText; // Set the text color to default
            }
        }

        private void PasswordBox_Enter(object sender, EventArgs e)
        {
            if (PasswordBox.Text == "Password")
            {
                PasswordBox.Text = string.Empty;
                PasswordBox.ForeColor = SystemColors.WindowText; // Set the text color to default
            }
        }

        private void PasswordBox_Click(object sender, EventArgs e)
        {
            if (PasswordBox.Text == string.Empty)
            { //needs changing and thinking
                // Set the PasswordBox text to some "original text"
                PasswordBox.Text = "Password"; // Replace "Original Text" with the actual text you want to set
            }
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void Helper_Click(object sender, EventArgs e)
        {
            MessageBox.Show("the top box is the Username box , the bottom is the password");
        }
    }
}
