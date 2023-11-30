using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using static restarunttest2.Form1;

namespace restarunttest2
{


    //Restrauntdb connection
    //these are placeholders ie change (name and password) // 
    public partial class Form1 : Form
    {

        private Timer lockouttimer; // to lock out sessions
        private int lockoutDuration = 5 * 60 * 1000; // 5 minutes timer
        private List<string> logs = new List<string>(); // for lists
        public Form1()
        {
            InitializeComponent();
            lockouttimer = new Timer();
            lockouttimer.Interval = lockoutDuration;
            lockouttimer.Tick += lockouttimer_tick;
            lockouttimer.Enabled = false; // so not locked out immediately
            timer1.Interval = 1000; // 1000 milliseconds = 1 second
            timer1.Tick += new EventHandler(timer1_Tick); // so 5 mins pass
            timer1.Start(); // Start the timer once locked out
            UpdateDateTime(); //nice lil timer at top
            btnback.Image.RotateFlip(RotateFlipType.Rotate180FlipY);
            LoadSettings();
            NavigateToOldGoogle();
            UpdateSearchHistorylistBox1();
            AttachRichTextBoxEvent();
           






        }
        private void LoadSettings()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("textadjust.xml"); // Load  XML file

                XmlNode root = doc.DocumentElement;
                XmlNode textNode = root.SelectSingleNode("Text");
                XmlNode fontNode = root.SelectSingleNode("Font");

                if (textNode != null)
                {
                    this.Text = textNode.InnerText; // Set form text
                }

                if (fontNode != null)
                {
                    this.Font = new System.Drawing.Font(fontNode.InnerText, 12); // Set font (adjust size as needed)
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if the XML file cannot be loaded or if settings are missing.
                MessageBox.Show("Error loading settings: " + ex.Message);
            }
        }
        private void lockouttimer_tick(object sender, EventArgs e)
        {
            // Timer has elapsed. Unlock the form.
            UnlockForm();
        }

        public void LockForm()
        {
            // Disable user input on the form, display a message, or take other actions.
            this.Enabled = false;
            MessageBox.Show("You are temporarily locked out. Please try again later.");

            lockouttimer.Start();
        }

        private void UnlockForm()
        {
            // Re-enable the form.
            lockouttimer.Stop();
            this.Enabled = true;
            MessageBox.Show("Lockout period has ended. You can now try again.");

        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();



        }
        private void UpdateDateTime()
        {
            // Update label1 with the current date, time, year, and month
            label1.Text = DateTime.Now.ToString("HH:mm:ss   dd-MMM-yyyy ");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionDb = "Server=localhost;Port=3306;Database=restrauntdb2;User=admin;Password=intrestingpassword;";
            MySqlConnection connection = new MySqlConnection(connectionDb);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                // Handle exceptions here
                Console.WriteLine("MySQL Error check if the Database is present or not locally : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        //


        public class WebBrowserHandler
        {
            private WebBrowser webBrowser;

            public WebBrowserHandler(WebBrowser webBrowser)
            {
                this.webBrowser = webBrowser;
                InitializeWebBrowser();

            }

            private void InitializeWebBrowser()
            {
                // Set properties
                webBrowser.Dock = DockStyle.Fill;
              

                // Navigate to a default page or URL
                

                // Attach the DocumentCompleted event handler
                webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            }

            private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
            {
                if (e.Url.AbsoluteUri == "https://www.google.com/")
                {
                    // Your logic after the document is completed for the specified URL
                    MessageBox.Show("Web page loaded successfully!");
                }
            }

            public void NavigateToUrl(string url)
            {
                // Navigate to the specified URL
                webBrowser.Navigate(url);
            }

            public void NavigateToDefaultPage()
            {
                // Navigate to a default page or URL
                webBrowser.Navigate("https://www.google.com");
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("help and tips information ");
            //show up a label link faq
            // Application.Run(help screen and faq forum);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri == "https://www.google.com/")
            {
                // Your logic after the document is completed for the specified URL
                MessageBox.Show("Web page loaded successfully!");
            }
        }
        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            // Handle the click event on the document
            HtmlElement clickedElement = webBrowser1.Document.GetElementFromPoint(e.ClientMousePosition);

            // Check if the clicked element is a link
            if (clickedElement.TagName.ToLower() == "a")
            {
                // Your logic when a link is clicked
                MessageBox.Show("Link clicked: " + clickedElement.GetAttribute("href"));
            }
        }
       

     


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnforward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void btnback_Click(object sender, EventArgs e)
        {

            webBrowser1.GoBack();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            // Open the clicked hyperlink in your web browser
            PerformSearch();
                   
        }
        private void AttachRichTextBoxEvent()
        {
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
        }
        private void NavigateToOldGoogle()
        {
            // Replace the URL with the address of "old Google"
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://www.google.com");
        }



        public void button2_Click(object sender, EventArgs e)
        {
            PerformSearch();
            UpdateSearchHistorylistBox1();
        }
        private void button3_Click(object sender, EventArgs e)
        {//find out how to limit this.
            account_creation varshmar = new account_creation();
            varshmar.Show(); // enter form name later 

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Perform the search when the Enter key is pressed in the text box
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                tologs(textBox1.Text.Trim());
                // Suppress the Enter key to prevent a newline in the text box
               
            }
        }
        private void tologs(string searchquery)
        {
            // Add the search query to the history
            try
            {
                logs.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {searchquery}"); //functionality to timestamp and auto remove this is a simplified version of history
                UpdateSearchHistorylistBox1();

                // Limit the search history to 10 items
                if (logs.Count > 10)
            {
                logs.RemoveAt(0); // Removing the oldest entry
            }
            }
            catch (Exception ex)
            {
              MessageBox.Show($"Error adding to logs: {ex.Message}");
            }

            // Update the ListBox to display the search history
  
        }
        private void UpdateSearchHistorylistBox1()
        {
            // Clear the existing items in the ListBox
            

            // Add the search history items to the ListBox
            foreach (string searchquery in logs)
            {
                listBox1.Items.Add(searchquery);
            }
        }
       


            private void PerformSearch()
            {
            // Get the search query from the text box
            webBrowser1.ScriptErrorsSuppressed = true;
            string searchquery = textBox1.Text.Trim();
                if (!string.IsNullOrEmpty(searchquery)) //so if not null search 
                {
                    // Navigate to the search engine with the query
                    webBrowser1.Navigate($"https://www.google.com/search?q={searchquery}"); //the http to our string
             
            }
            }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    }
    
