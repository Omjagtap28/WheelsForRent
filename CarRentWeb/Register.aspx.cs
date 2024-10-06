using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CarRentWeb
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("Register.aspx"))
            {
                homeLink.Attributes["class"] = "highlight-homepage";
            }
            ErrorMessage.ForeColor = System.Drawing.Color.Red;
        }

        protected void Register_or_Login_Click(object sender, EventArgs e)
        {
            Session["register_to_login"] = "true";
            Response.Redirect("index.aspx");
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {

            string email = registerUsername.Text.Trim(); // Trim to remove any leading or trailing spaces
            string password = registerPassword.Text;
            string confirmPassword = registerPassword_c.Text;
            string selectedQuestion = securityQuestion.SelectedItem.Text;
            string answer = securityAnswer.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ErrorMessage.Text = "Please fill out all the fields.";
                
                return;
            }
            // Email format validation
            if (!IsValidEmailFormat(email))
            {
                ErrorMessage.Text = "Please Enter Correct Email ID";
                
                return;
            }



            // Check if password and confirm password fields match
            if (password != confirmPassword)
            {
                ErrorMessage.Text = "Passwords do not match. Please try again.";
                
                return;
            }
            if (password.Length < 8)
            {
                ErrorMessage.Text = "Password should be at least 8 characters long";
                
                return;
            }

            // Check if password contains at least one number
            if (!password.Any(char.IsDigit))
            {
                ErrorMessage.Text = "Password should contain at least one number.";
                
                return;
            }

            // Check if password contains at least one symbol (excluding alphanumeric characters)
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                ErrorMessage.Text = "Password should contain at least one symbol.";
                
                return;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                ErrorMessage.Text = "Password should contain at least one uppercase letter and one lowercase letter.";
                
                return;
            }


            // Validate if the answer is provided
            if (string.IsNullOrWhiteSpace(answer))
            {
                ErrorMessage.Text = "Please provide an answer to the security question.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if the email already exists in the database
                    string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int emailCount = (int)checkCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        ErrorMessage.Text = "Email already exists. Please use a different email.";
                        return;
                    }

                    // If email doesn't exist, proceed with insertion
                    string insertQuery = "INSERT INTO [User] (Email, Password, Question, Answer) VALUES(@Value1, @Value2, @Value3, @Value4)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);

                    cmd.Parameters.AddWithValue("@Value1", email);
                    cmd.Parameters.AddWithValue("@Value2", password);
                    cmd.Parameters.AddWithValue("@Value3", selectedQuestion); // Question
                    cmd.Parameters.AddWithValue("@Value4", answer);

                    cmd.ExecuteNonQuery();

                    ErrorMessage.Text = "Register Successfully !!";
                    ErrorMessage.BackColor = System.Drawing.Color.Green;
                    Session["IsRegistered"] = "true";

                    if (Session["CarIDForRedirect"] != null)
                    {
                        Session["Email"] = email;
                        // Get the CarID and remove the Session variable
                        string carIDForRedirect = Session["CarIDForRedirect"].ToString();
                        Session.Remove("CarIDForRedirect");

                        

                        // Redirect back to CarDetails.aspx with the stored CarID
                        Response.Redirect($"CarDetails.aspx?CarID={carIDForRedirect}");
                    }
                    else
                    {
                        // Redirect to the default page if CarIDForRedirect is not set
                        Response.Redirect("index.aspx");
                    }
                    
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static bool IsValidEmailFormat(string email)
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
    }
}

