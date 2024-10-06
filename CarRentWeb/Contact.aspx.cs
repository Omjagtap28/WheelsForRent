using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentWeb
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("/Contact.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }
            ErrorMessage.ForeColor = System.Drawing.Color.Red;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string subject = txtSubject.Text.Trim();
            string message = txtMessage.Text.Trim();
            string email = txtEmail.Text.Trim();

            DateTime currentDate = DateTime.Now;

            // Check if any of the required fields are empty
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(message))
            {
                ErrorMessage.Text = "Please fill all the details!";
                return;
            }

            // Validate email format
            if (!IsValidEmail(email))
            {
                ErrorMessage.Text = "Please enter a valid email address!";
                return;
            }

            // Validate name (should not contain numbers)
            if (ContainsNumbers(name))
            {
                ErrorMessage.Text = "Name should not contain numbers!";
                return;
            }

            // Insert into Contact table
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            string query = "INSERT INTO Contact (Name, Email, Subject, Message, Date) VALUES (@Name, @Email, @Subject, @Message, @Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Subject", subject);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Date", currentDate);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            ErrorMessage.ForeColor = System.Drawing.Color.Green;
            ErrorMessage.Text = "Thank you for reaching out to us! Your message has been successfully sent. We'll get back to you as soon as possible. ";
            txtName.Text = "";
            txtEmail.Text = "";
            txtSubject.Text = "";
            txtMessage.Text = "";
            
        }

        // Method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Method to check if the name contains numbers
        private bool ContainsNumbers(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

    }
}