using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls;
using RV = CarRentWeb.RegistrationValidator;

namespace CarRentWeb
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Email"] == null)
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    string userEmail = Session["Email"].ToString();
                    PopulateUserInfo(userEmail);
                    StoreInitialValues();
                }
            }
        }
        private void StoreInitialValues()
        {
            ViewState["InitialPassword"] = txtPassword.Text;
            ViewState["InitialFirstName"] = txtFirstName.Text;
            ViewState["InitialLastName"] = txtLastName.Text;
            ViewState["InitialPhoneNo"] = txtPhoneNo.Text;
            ViewState["InitialQuestion"] = securityQuestion.SelectedValue;
            securityQuestion.Enabled = false;
            ViewState["InitialAnswer"] = txtAnswer.Text;
            ViewState["InitialAddress"] = txtAddress.Text;
            
        }

        private void PopulateUserInfo(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            string query = "SELECT * FROM [User] WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEmail.Text = reader["Email"].ToString();
                    txtPassword.Text = reader["Password"].ToString();
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtPhoneNo.Text = reader["PhoneNo"].ToString();

                    string questionValue = reader["Question"].ToString();
                    ListItem questionItem = securityQuestion.Items.FindByValue(questionValue);
                    if (questionItem != null)
                    {
                        questionItem.Selected = true;
                    }

                    txtAnswer.Text = reader["Answer"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                }

                reader.Close();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            // Validating first name
            if (!RV.IsValidName(txtFirstName.Text,ErrorMessage))
            {
                return;
            }
            if (!RV.IsitFilled(txtFirstName.Text, ErrorMessage))
            {
                return;
            }
            // Validating last name
            if (!RV.IsValidName(txtLastName.Text, ErrorMessage))
            {
                return;
            }
            if (!RV.IsitFilled(txtLastName.Text, ErrorMessage))
            {
                return;
            }
            // Password Validations
            if (!RV.IsPasswordLengthValid(password,8,ErrorMessage))
            {
                return;
            }
            if (!RV.ContainsNumber(password,ErrorMessage))
            {
                return;
            }
            if (!RV.ContainsSymbol(password, ErrorMessage))
            {
                return;
            }
            if (!RV.ContainsUpperCase(password, ErrorMessage))
            {
                return;
            }
            if (!RV.ContainsLowerCase(password, ErrorMessage))
            {
                return;
            }
            //PhoneNo Validation
            if (!RV.IsValidMobileNumber(txtPhoneNo.Text, ErrorMessage))
            {
                return;
            }
            //Answer not provided
            if (!RV.IsAnswerProvided(txtAnswer.Text ,ErrorMessage))
            {
                return;
            }


            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            string query = "UPDATE [User] SET Password = @Password, FirstName = @FirstName, LastName = @LastName, PhoneNo = @PhoneNo, Question = @Question, Answer = @Answer, Address = @Address WHERE Email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@Question", securityQuestion.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Answer", txtAnswer.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Update successful
                    DisableEditingMode();
                    ErrorMessage.ForeColor = Color.Green;
                    ErrorMessage.Text = "Profile updated successfully";
                }
                else
                {
                    // Update failed
                    ErrorMessage.ForeColor = Color.Red;
                    ErrorMessage.Text = "Failed to update profile";
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EnableEditingMode();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Restore initial values
            RestoreInitialValues();
            // Disable editing mode
            DisableEditingMode();
        }
        private void EnableEditingMode()
        {
            txtPassword.ReadOnly = false;
            txtFirstName.ReadOnly = false;
            txtLastName.ReadOnly = false;
            securityQuestion.Enabled = true;
            txtAnswer.ReadOnly = false;
            txtPhoneNo.ReadOnly = false;
            txtAddress.ReadOnly = false;
            profileEdit.Visible = false;
            profileSubmit.Visible = true;
            profileCancel.Visible = true;
        }
        private void DisableEditingMode()
        {
            txtPassword.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            securityQuestion.Enabled = false;
            txtAnswer.ReadOnly = true;
            txtPhoneNo.ReadOnly = true;
            txtAddress.ReadOnly = true;

            profileSubmit.Visible = false;
            profileCancel.Visible = false;
            profileEdit.Visible = true;
        }

        private void RestoreInitialValues()
        {
            txtPassword.Text = ViewState["InitialPassword"].ToString();
            txtFirstName.Text = ViewState["InitialFirstName"].ToString();
            txtLastName.Text = ViewState["InitialLastName"].ToString();
            txtPhoneNo.Text = ViewState["InitialPhoneNo"].ToString();
            securityQuestion.SelectedValue = ViewState["InitialQuestion"].ToString();
            txtAnswer.Text = ViewState["InitialAnswer"].ToString();
            txtAddress.Text = ViewState["InitialAddress"].ToString();
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("index.aspx");
        }
    }
}
