using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Linq;
using System.Configuration;

namespace CarRentWeb
{
    public partial class forgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.ForeColor = System.Drawing.Color.Red;
        }
        protected void submitEmailButton_Click(object sender, EventArgs e)
        {
            // Here, you should check if the email exists in your database.
            // If it exists, show the security question and answer textbox.

            string email = forgotEmail.Text.Trim();
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            if (string.IsNullOrWhiteSpace(email))
            {
                ErrorMessage.Text = "Please Enter your Email ID";
                return;
            }
            if (!IsValidEmailFormat(email))
            {
                ErrorMessage.Text = "Please Enter Correct Email ID";
                return;
            }


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Question FROM [User] WHERE Email=@Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        securityQuestionLabel.Text = result.ToString();
                        securityQuestionLabel.Visible = true;
                        securityAnswerTextbox.Visible = true;
                        forgotEmail.Visible = false;
                        submitEmailButton.Visible = false;
                        verifyAnswerButton.Visible = true;
                        forgotEmailLabel.Visible = false;
                        ErrorMessage.Text = "";
                    }
                    else
                    {
                        ErrorMessage.Text = "Email not found !";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Exception occured:", ex);
                    ErrorMessage.Text = "sql Error";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void verifyAnswerButton_Click(object sender, EventArgs e)
        {
            string answer = securityAnswerTextbox.Text.Trim().ToLower();
            string email = forgotEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(answer))
            {
                ErrorMessage.Text = "Please Enter Answer of Security Question";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Answer FROM [User] WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    object result = cmd.ExecuteScalar();

                    if (result != null && answer == result.ToString().ToLower())
                    {
                        // If the answer matches, display the new password fields.
                        newPasswordLabel.Visible = true;
                        newPasswordTextbox.Visible = true;
                        confirmPasswordLabel.Visible = true;
                        confirmPasswordTextbox.Visible = true;
                        resetPasswordButton.Visible = true;
                        verifyAnswerButton.Visible = false;
                        securityAnswerTextbox.Visible = false;
                        ErrorMessage.Text = "";
                        securityQuestionLabel.Visible = false;
                        
                    }
                    else
                    {
                        ErrorMessage.Text = "Incorrect answer!";
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Exception occurred:", ex);
                    ErrorMessage.Text = "SQL Error";
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        protected void resetPasswordButton_Click(object sender, EventArgs e)
        {
            string newPassword = newPasswordTextbox.Text.Trim();
            string confirmPassword = confirmPasswordTextbox.Text.Trim();
            string email = forgotEmail.Text.Trim();
            string connectionString = "Data Source=(LocalDB)\\LocalDbDemo;Initial Catalog=WheelsForRent;Integrated Security=True";
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ErrorMessage.Text = "Please Enter the password";
                return;
            }
            if (newPassword.Length < 8)
            {
                ErrorMessage.Text = "Password should be at least 8 characters long";
                return;
            }
            // Check if password contains at least one number
            if (!newPassword.Any(char.IsDigit))
            {
                ErrorMessage.Text = "Password should contain at least one number.";
                return;
            }

            // Check if password contains at least one symbol (excluding alphanumeric characters)
            if (!newPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                ErrorMessage.Text = "Password should contain at least one symbol.";
                return;
            }
            if (!newPassword.Any(char.IsUpper))
            {
                ErrorMessage.Text = "Password should contain at least one uppercase letter.";
                return;
            }

            // Check if password contains at least one lowercase letter
            if (!newPassword.Any(char.IsLower))
            {
                ErrorMessage.Text = "Password should contain at least one lowercase letter.";
                return;
            }


            if (newPassword == confirmPassword)
            {
                // Check if the entered password is the same as the one stored in the database
                string storedPassword = "";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT Password FROM [User] WHERE Email=@Email";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Email", email);

                        storedPassword = cmd.ExecuteScalar()?.ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Exception occurred:", ex);
                        ErrorMessage.Text = "SQL Error";
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                if (newPassword == storedPassword)
                {
                    ErrorMessage.Text = "Please enter a different password. You cannot use the same password as the previous one.";
                    return;
                }


                // Update the password in the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE [User] SET Password=@Password WHERE Email=@Email";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        cmd.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Session["PasswordResetMessage"] = "Password Updated successfully!";
                            ErrorMessage.ForeColor = System.Drawing.Color.Green;
                            ErrorMessage.Text = "";
                            Response.Redirect("index.aspx");
                        }
                        else
                        {
                            ErrorMessage.Text = "Failed to update password!";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Exception occurred:", ex);
                        ErrorMessage.Text = "SQL Error";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                ErrorMessage.Text = "Passwords do not match!";
            }
        }

        private bool IsValidEmailFormat(string email)
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
