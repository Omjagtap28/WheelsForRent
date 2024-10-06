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
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["Email"] == null)
            {
                Session["PlzLogin"] = "PlzLogin";
                Response.Redirect("~/index.aspx");
            }
            else
            {
                if (Request.Url.AbsolutePath.EndsWith("/Feedback.aspx"))
                {
                    // Add the CSS class to highlight the home page link
                    homeLink.Attributes["class"] = "highlight-homepage";
                }
            }

        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("index.aspx");
        }

        protected void submitFeedbackButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rating.SelectedValue))
            {
                ErrorMessage.Text = "Please select a rating.";
                ErrorMessage.BackColor = System.Drawing.Color.Red;
                return; // Stop further execution if rating is not selected
            }

            int Rating = Convert.ToInt32(rating.SelectedValue);
            string email = up_email.Text;  // Assuming you want to use the email from the label
            string comment = this.comment.Text.Trim();
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  // Assuming SQL Server datetime format


            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // Your SQL query to insert data into the feedback table
            string query = "INSERT INTO feedback (Email, Comment, Rating, Date) VALUES (@Email, @Comment, @Rating, @Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Comment", comment);
                        command.Parameters.AddWithValue("@Rating", Rating);
                        command.Parameters.AddWithValue("@Date", date);

                        // Execute the query
                        command.ExecuteNonQuery();

                        l_rating.Visible = false;
                        rating.Visible = false;
                        l_comment.Visible = false;
                        this.comment.Visible = false;
                        submitFeedbackButton.Visible = false;
                        ErrorMessage.Text = "Thank you for your feedback! We appreciate your input.";
                        ErrorMessage.ForeColor = System.Drawing.Color.Green;

                        // You can add additional logic or redirection here if needed
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You may want to log the error or show an error message to the user
                    // For simplicity, I'm just printing the error to the console
                    Console.WriteLine("Error: " + ex.Message);
                    ErrorMessage.Text = "An error occurred while submitting your feedback. Please try again." +ex.Message;
                    ErrorMessage.BackColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}