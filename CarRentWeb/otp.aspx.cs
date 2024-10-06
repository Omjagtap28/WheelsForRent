using System;
using System.Data.SqlClient;
using System.Diagnostics;
// Add these using statements at the top if not already present
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace CarRentWeb
{
    public partial class otp : System.Web.UI.Page
    {
        private string email;
        private int carID;
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can perform any initialization if needed
            if (!IsPostBack)
            {
                if (Session["EnteredUPI"] != null)
                {
                    if (Session["PaymentOption"].ToString() == "UPI")
                    {
                        message.Text = "Enter Upi Pin :";
                    }
                    else
                    {
                        message.Text = "Enter your OTP :";
                    }
                }
                email = Session["Email"].ToString(); // Assuming user email is stored in Session
                carID = Convert.ToInt32(Request.QueryString["CarID"]); // Assuming carID is passed in the query string
                decimal totalAmount = (decimal)Session["TotalAmount"];
                string paymentOption = (string)Session["PaymentOption"];

                lblTotalAmount.Text = $"Total Amount: {totalAmount}";
                lblPaymentStatus.Text = $"Payment Status: {paymentOption}";

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string enteredOTP = txtOTP.Text.Trim();
            string enteredPin = txtOTP.Text.Trim();

            string paymentOption = (string)Session["PaymentOption"];
            decimal totalAmount = (decimal)Session["TotalAmount"];

            email = Session["Email"].ToString();
            carID = Convert.ToInt32(Request.QueryString["CarID"]);

            int remainingAttempts = GetRemainingAttempts();

            if (Session["EnteredUPI"] != null)
            {
                if (IsFourOrSixDigitPin(enteredPin))
                {
                    // PIN verification successful
                    lblMessage.Text = "PIN verification successful. Redirecting...";
                    // Update Reservation table based on paymentOption
                    ErrorMessage.Text = "Success";
                    UpdateReservationTable(email, carID, totalAmount, paymentOption);
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    remainingAttempts--;

                    if (remainingAttempts > 0)
                    {
                        lblMessage.Text = $"Invalid PIN. {remainingAttempts} attempts remaining. Please enter a 4 or 6-digit PIN.";
                        // You can optionally clear the entered PIN field here
                        txtOTP.Text = "";
                    }
                    else
                    {
                        // Redirect to payment.aspx after 2 attempts
                        ResetRemainingAttempts(); // Reset attempts when redirecting to payment.aspx
                        int carID = Convert.ToInt32(Request.QueryString["CarID"]);
                        Response.Redirect($"payment.aspx?paymentStatus=failed&carID={carID}");
                    }
                }
            }
            else
            {
                // Check if the entered OTP is a 6-digit number
                if (IsSixDigitOTP(enteredOTP))
                {
                    // OTP verification successful
                    lblMessage.Text = "OTP verification successful. Redirecting...";
                    // Update Reservation table based on paymentOption
                    ErrorMessage.Text = "Success";
                    UpdateReservationTable(email, carID, totalAmount, paymentOption);

                    confirmationLabel.Text = "OTP verification successful. You will now be redirected to the homepage.";

                    string script = "setTimeout(function(){ window.location.href = 'homepage.aspx'; }, 3000);"; // 3000 milliseconds = 3 seconds
                    ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);



                    // Redirect to homepage.aspx on successful OTP verification
                    Response.Redirect("homepage.aspx");
                }
                else
                {
                    remainingAttempts--;

                    if (remainingAttempts > 0)
                    {
                        lblMessage.Text = $"Invalid OTP. {remainingAttempts} attempts remaining. Please enter a 6-digit OTP.";
                        // You can optionally clear the entered OTP field here
                        txtOTP.Text = "";
                    }
                    else
                    {
                        // Redirect to payment.aspx after 2 attempts
                        ResetRemainingAttempts(); // Reset attempts when redirecting to payment.aspx
                        int carID = Convert.ToInt32(Request.QueryString["CarID"]);
                        Response.Redirect($"payment.aspx?paymentStatus=failed&carID={carID}");
                    }
                }
            }
        }

        private void ResetRemainingAttempts()
        {
            // Reset remaining attempts to the initial value
            const string attemptsKey = "RemainingAttempts";
            Session[attemptsKey] = 3;
        }


        private int GetRemainingAttempts()
        {
            // Use Session or any other storage mechanism to track remaining attempts
            const string attemptsKey = "RemainingAttempts";
            int remainingAttempts = Session[attemptsKey] as int? ?? 3; // Set the initial number of attempts

            // Decrement the attempts and store the value in Session
            Session[attemptsKey] = --remainingAttempts;

            return remainingAttempts;
        }

        private bool IsFourOrSixDigitPin(string pin)
        {
            // Check if the PIN is either 4 or 6 digits
            return (pin.Length == 4 || pin.Length == 6) && int.TryParse(pin, out _);
        }

        private bool IsSixDigitOTP(string otp)
        {
            // Check if the OTP is a 6-digit number
            int parsedOTP;
            return int.TryParse(otp, out parsedOTP) && otp.Length == 6;
        }
        private void UpdateReservationTable(string email, int carID, decimal totalAmount, string paymentOption)
        {
            // Update Reservation table with TotalAmount and PaymentStatus for the latest reservation
            string connectionString = "Data Source=(LocalDB)\\LocalDbDemo;Initial Catalog=WheelsForRent;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Retrieve the latest ReservationID for the specified user and vehicle
                    // Retrieve the latest ReservationID for the specified user and vehicle
                    // Retrieve the latest ReservationID for the specified user and vehicle
                    string latestReservationQuery = "SELECT TOP 1 ReservationID FROM Reservation WHERE Email = @Email AND VehicleID = @CarID ORDER BY ReservationID DESC";
                    SqlCommand latestReservationCmd = new SqlCommand(latestReservationQuery, conn);
                    // Ensure email is not null or empty before adding it as a parameter
                    if (!string.IsNullOrEmpty(email))
                    {
                        latestReservationCmd.Parameters.Add(new SqlParameter("@Email", email));
                    }
                    else
                    {
                        // Handle the case where email is null or empty
                        // You can add appropriate error handling or use a default email value
                        Other.Text = "no email";
                    }

                    // Ensure carID is a valid integer before adding it as a parameter
                    if (carID > 0)
                    {
                        latestReservationCmd.Parameters.Add(new SqlParameter("@CarID", carID));
                    }
                    else
                    {
                        // Handle the case where carID is not a valid integer
                        // You can add appropriate error handling or use a default carID value
                        Other.Text = "no car id";
                    }
                    latestReservationCmd.Parameters.Clear(); // Clear existing parameters
                    latestReservationCmd.Parameters.AddWithValue("@Email", email);
                    latestReservationCmd.Parameters.AddWithValue("@CarID", carID);


                    object latestReservationResult = latestReservationCmd.ExecuteScalar();



                    if (latestReservationResult != null && latestReservationResult != DBNull.Value)
                    {
                        int latestReservationID = Convert.ToInt32(latestReservationResult);
                        Session["LatestReservagtionId"] = latestReservationID;

                        

                        // Update the latest reservation with TotalAmount and PaymentStatus
                        string updateQuery = "UPDATE Reservation SET TotalAmount = @TotalAmount, PaymentStatus = @PaymentStatus WHERE ReservationID = @ReservationID";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        updateCmd.Parameters.AddWithValue("@PaymentStatus", paymentOption); // Use paymentOption as PaymentStatus
                        updateCmd.Parameters.AddWithValue("@ReservationID", latestReservationID);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update successful
                            // You can add any additional logic or messages here
                            ErrorMessage.Text = "Update successful";
                        }
                        else
                        {
                            // Update failed, handle accordingly
                            // You can add any additional logic or messages here
                            ErrorMessage.Text = "Update Fail";
                        }
                    }
                    else
                    {
                        // No reservation found for the specified user and vehicle
                        // Handle accordingly
                        ErrorMessage.Text = "No reservation found";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Error : " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}