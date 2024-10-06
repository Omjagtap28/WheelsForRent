using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CarRentWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("/index.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }
            if (Session["Email"] != null)
            {
                Response.Redirect("HomePage.aspx");
            }
            if (Session["PlzLogin"] != null)
            {
                loginFormContainer.Attributes["class"] = "";
                ErrorMessage.Text = "Please Login First";
                Session.Remove("PlzLogin");
            }
            if (Session["IsRegistered"] != null && Session["IsRegistered"].ToString() == "true")
            {
                // If the user has just registered and redirected here, show the form
                ErrorMessage.Text = "Registration Successful!";
                ErrorMessage.ForeColor = System.Drawing.Color.Green;
                loginFormContainer.Attributes["class"] = "";
            }
            if (Session["register_to_login"] != null && Session["register_to_login"].ToString() == "true")
            {
                loginFormContainer.Attributes["class"] = "";
                Session["register_to_login"] = null;// Reset the session variable
            }

            if (Session["PasswordResetMessage"] != null)
            {
                ErrorMessage.Text = Session["PasswordResetMessage"].ToString();
                ErrorMessage.ForeColor = System.Drawing.Color.Green;
                Session.Remove("PasswordResetMessage");
                loginFormContainer.Attributes["class"] = ""; // Make sure the form is visible
            }
            DeleteEmptyReservations();
            
        }


        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (!RadioButton1.Checked && !RadioButton2.Checked) // Neither radio button is selected
            {
                ErrorMessage.Text = "Please select an appropriate user type (User/Admin)";
                return;
            }

            string email = loginUsername.Text.Trim(); // Trim to remove any leading or trailing spaces
            string password = loginPassword.Text.Trim(); // Trim to remove any leading or trailing spaces

            // Check if email or password is empty or contains only spaces
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage.Text = "Please Enter your email and password";
                return;
            }

            if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                ErrorMessage.Text = "Please select an appropriate user type (User/Admin)";
                return;
            }

            if (!IsValidEmailFormat(email))
            {
                ErrorMessage.Text = "Please Enter Correct Email ID";
                return;
            }

            bool isAdmin = RadioButton2.Checked;

            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = isAdmin ? "SELECT COUNT(1) FROM [Admin] WHERE Email=@Email AND Password=@Password"
                                           : "SELECT COUNT(1) FROM [User] WHERE Email=@Email AND Password=@Password";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        // Login successful

                        Session["Email"] = email;
                        if (isAdmin)
                        {
                            // Redirect admin to admin page
                            Session["IsAdmin"] = "true";
                            Response.Redirect("Admin/a_home.aspx");
                        }
                        else
                        {

                            // Redirect user to user page
                            ErrorMessage.Text = "Login Successful";
                            ErrorMessage.ForeColor = System.Drawing.Color.Green;

                            if (Session["CarIDForRedirect"] != null)
                            {
                                // Get the CarID and remove the Session variable
                                string carIDForRedirect = Session["CarIDForRedirect"].ToString();
                                Session.Remove("CarIDForRedirect");

                                

                                // Redirect back to CarDetails.aspx with the stored CarID
                                Response.Redirect($"CarDetails.aspx?CarID={carIDForRedirect}");
                            }
                            else
                            {
                                // Redirect to the default page if CarIDForRedirect is not set
                                Response.Redirect("HomePage.aspx");
                            }


                            Response.Redirect("HomePage.aspx");

                        }
                    }
                    else
                    {
                        // Login failed
                        ErrorMessage.Text = "Login Failed !";
                        ErrorMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Error occurred: " + ex.Message;
                    ErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void Register_or_Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
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
        private void DeleteEmptyReservations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Delete reservations with empty PaymentStatus and TotalAmount for all users
                    string deleteQuery = "DELETE FROM Reservation WHERE PaymentStatus IS NULL OR TotalAmount IS NULL";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    // Log the number of deleted reservations (optional)
                    if (rowsAffected > 0)
                    {
                        
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Error :" + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
