using System;
using System.Net.Security;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Remoting.Messaging;

namespace restarunttest2
{
    public class Loginform : Form1 //class to reference as using .:. declare public
    {

        private TextBox usernametextbox;
        private TextBox passwordtextbox;
        private Button loginbutton;
        private int failedLoginAttempts = 0;
        //global stuff for global reasons

        public Loginform()
        {
            InitializeComponents();
            eventh();
        }

        private void InitializeComponents() //method defines properties of textboxes and stuff
        {


            this.Text = "Login";
            

            usernametextbox = new TextBox
            {
                //can rid of after
                Location = new System.Drawing.Point(800, 110),
                Width = 200,

                ///if enter any num or char clear the text of field username, when empty write in again?
                ///
            };

            passwordtextbox = new TextBox
            {
                Location = new System.Drawing.Point(800, 150),
                Width = 200,
                PasswordChar = '*',
            };

            loginbutton = new Button
            {
                Text = "Login",
                Location = new System.Drawing.Point(800, 200),
            };


            this.Controls.Add(usernametextbox);
            this.Controls.Add(passwordtextbox);
            this.Controls.Add(loginbutton);
        }
        //method ends here?


        private void eventh()
        {
            loginbutton.Click += loginbuttonvclick; //we can now define a method for logginbuttonvclick
        }
        public void loginbuttonvclick(object sender, EventArgs e) //eeeeeeeeeeeeeeeeeeee
        {
            string username = usernametextbox.Text; //this just autogened  i think itll work fine anyways
            string password = passwordtextbox.Text;
            //option to create f and else statments here
            // if condition is true do login
            //else print login fail or something
            if (Isauthenticated(username, password))
            {

                MessageBox.Show("successful logging in...");

            }
            //make a condition that if nothing is entered it will just enable a fresh retry the counter wont be incremeted at all
            else
            {
                failedLoginAttempts++;
                MessageBox.Show("try again");

                //enter a future way to only allow a total input of three times failure and then sleep the form input
                if (failedLoginAttempts >= 3)
                {
                    LockForm();
                }
            }
        }
        public bool Isauthenticated(string username, string password)
        {
            bool isauthenticated = false;
            string query = "SELECT user_id FROM user WHERE username = @username AND password = @password"; //ie your db credentials to serach for
            string connectiondb = "specify here";
            using (MySqlConnection connection = new MySqlConnection(connectiondb))
            {
                try
                {
                    connection.Open();

                    MessageBox.Show("opened connection");
                }
                catch
                {
                    MessageBox.Show("noconnection");
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())

                        {

                            isauthenticated = true; //make this restricted based on account_name where only admins are aloud to type and use auth form the editor
                            auth authForm = new auth();
                            authForm.Show();
                            Loginform loginform = new Loginform();
                            loginform.Close();
                            

                            
                            //improve get rid of loggining buttons etc when someonne logs in write the username and password into a file with times

                            // do the same for when operations are done on the auth form but this does not need to be done here.
                        }

                    }

                }

            }
            return isauthenticated;
        }

        private bool VerifyPassword(string enteredPassword, string hashedPasswordFromDb)
        {
           /// there is no point in this yet but you can implement reccommend sha256 or argon 2
            return enteredPassword == hashedPasswordFromDb;
        }







        //enter actual auth later allow mor ethan one auths


        // Example usage in your code


        //needs fixing

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Loginform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1004, 452);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Loginform";
            this.Load += new System.EventHandler(this.Loginform_Load);
            this.ResumeLayout(false);

        }

        private void Loginform_Load(object sender, EventArgs e)
        {

        }
    }
}


