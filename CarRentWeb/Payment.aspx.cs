using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;

namespace CarRentWeb
{
    public partial class Payment : System.Web.UI.Page
    {
        private decimal totalAmount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve necessary information from Session or database
                string email = Session["Email"].ToString(); // Assuming user email is stored in Session
                int carID = Convert.ToInt32(Request.QueryString["CarID"]); // Assuming carID is passed in the query string

                // Retrieve pickup and return dates from the Reservation table
                DateTime pickupDate;
                DateTime returnDate;

                // Retrieve rental fee from the Vehicle table
                decimal rentalFee;


                string paymentStatus = Request.QueryString["paymentStatus"];

                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    if (paymentStatus.Equals("failed", StringComparison.OrdinalIgnoreCase))
                    {
                        // Display payment failed message
                        Payment_failed.Text = "Payment failed. Please try again or choose a different payment method.";
                    }
                    // You can handle other status values if needed
                }

                // Perform database queries to get the required information
                string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Retrieve pickup and return dates from the Reservation table
                        string reservationQuery = "SELECT TOP 1 PickupDate, ReturnDate FROM Reservation WHERE Email = @Email ORDER BY ReservationID DESC";
                        SqlCommand reservationCmd = new SqlCommand(reservationQuery, conn);
                        reservationCmd.Parameters.AddWithValue("@Email", email);

                        SqlDataReader reservationReader = reservationCmd.ExecuteReader();

                        if (reservationReader.Read())
                        {
                            pickupDate = Convert.ToDateTime(reservationReader["PickupDate"]);
                            returnDate = Convert.ToDateTime(reservationReader["ReturnDate"]);
                        }
                        else
                        {
                            // Handle the case where reservation data is not found
                            return;
                        }

                        reservationReader.Close();

                        // Retrieve rental fee from the Vehicle table
                        string vehicleQuery = "SELECT Price FROM Vehicle WHERE VehicleID = @CarID";
                        SqlCommand vehicleCmd = new SqlCommand(vehicleQuery, conn);
                        vehicleCmd.Parameters.AddWithValue("@CarID", carID);

                        object rentalFeeResult = vehicleCmd.ExecuteScalar();

                        if (rentalFeeResult != null && rentalFeeResult != DBNull.Value)
                        {
                            rentalFee = Convert.ToDecimal(rentalFeeResult);
                        }
                        else
                        {
                            // Handle the case where rental fee is not found
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        Response.Write($"Exception occurred: {ex.Message}");
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                // Calculate the total amount
                TimeSpan rentalPeriod = returnDate - pickupDate;
                totalAmount = rentalFee * (decimal)rentalPeriod.TotalHours;

                // Display the total amount or use it as needed
                lblTotalAmount.Text = $"{totalAmount:C}";

                // Store total amount in session
                Session["TotalAmount"] = totalAmount;
            }
        }
        private bool ValidateCreditCardNumber(string creditCardNumber)
        {
            // Remove spaces and dashes from the credit card number
            string cleanedCreditCardNumber = Regex.Replace(creditCardNumber, @"[\s-]", "");

            // Define regular expressions for Mastercard, Visa, American Express, and RuPay
            string mastercardPattern = @"^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$";
            string visaPattern = @"^4[0-9]{12}(?:[0-9]{3})?$";
            string amexPattern = @"^3[47][0-9]{13}$";
            string rupayPattern = @"^60[0-9]{14}$|^65[0-9]{14}$|^81[0-9]{14}$|^82[0-9]{14}$";

            // Check if the credit card number matches any of the patterns
            if (Regex.IsMatch(cleanedCreditCardNumber, mastercardPattern))
            {
                // It's a Mastercard
                return true;
            }
            else if (Regex.IsMatch(cleanedCreditCardNumber, visaPattern))
            {
                // It's a Visa
                return true;
            }
            else if (Regex.IsMatch(cleanedCreditCardNumber, amexPattern))
            {
                // It's an American Express
                return true;
            }
            else if (Regex.IsMatch(cleanedCreditCardNumber, rupayPattern))
            {
                // It's a RuPay card
                return true;
            }

            // If no pattern matches, it's an invalid credit card number
            return false;
        }

        private bool IsCardHolderNameValid(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return true;
        }
        protected void btnPayNowCreditCard_Click(object sender, EventArgs e)
        {
            string creditCardNumber = txtCardNumber.Text.Trim();
            string cvv = txtCvv.Text.Trim();
            string holdername = txtCardholderName.Text;
            if (string.IsNullOrEmpty(holdername))
            {
                ErrorMessage.Text = "Please Enter your details";
            }
            // Get the selected expiry month and year
            string expiryMonth = ddlExpiryMonth.SelectedValue;
            string expiryYear = ddlExpiryYear.SelectedValue;
            Payment_failed.Text = "";
            // Validate CVV/CVC based on the card type
            if (ValidateCreditCardNumber(creditCardNumber) && ValidateCvv(cvv, creditCardNumber))
            {
                // Check if the selected expiry date is later than today's date
                if (IsExpiryDateValid(expiryMonth, expiryYear) && IsCardHolderNameValid(holdername))
                {
                    // Credit card information is valid, generate and store OTP

                    Session["PaymentOption"] = "CreditCard";
                    // Pass totalAmount to otp.aspx
                    // Redirect to the OTP page
                    // Redirect to the OTP page with total amount
                    int carID = Convert.ToInt32(Request.QueryString["CarID"]);
                    decimal tm = (decimal)Session["TotalAmount"];
                    Response.Redirect($"otp.aspx?carID={carID}&totalAmount={tm}");
                }
                else
                {
                    // Invalid expiry date, show an error message
                    ErrorMessage.Text = "Invalid expiry date. Please select a valid expiry date. Or fill Card holder name";

                    // Set the display style of the credit card form to "block"
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowCreditCardForm", "showPaymentForm('creditCard');", true);
                }
            }
            else
            {
                // Invalid credit card information, show an error message
                ErrorMessage.Text = "Invalid credit card information Or Invalid Cvv. Please enter valid details.";

                // Set the display style of the credit card form to "block"
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowCreditCardForm", "showPaymentForm('creditCard');", true);
            }
        }



        private bool ValidateCvv(string cvv, string creditCardNumber)
        {
            // Check if the CVV/CVC length is valid based on the card type
            if (Regex.IsMatch(creditCardNumber, @"^5[1-5]")) // Mastercard
            {
                return Regex.IsMatch(cvv, @"^\d{3}$"); // Should contain 3 numeric characters
            }
            else if (Regex.IsMatch(creditCardNumber, @"^4")) // Visa
            {
                return Regex.IsMatch(cvv, @"^\d{3}$"); // Should contain 3 numeric characters
            }
            else if (Regex.IsMatch(creditCardNumber, @"^3[47]")) // American Express
            {
                return Regex.IsMatch(cvv, @"^\d{4}$"); // Should contain 4 numeric characters
            }

            return false;
        }


        private bool IsExpiryDateValid(string expiryMonth, string expiryYear)
        {
            // Check if the selected expiry date is later than today's date
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            int selectedYear;
            int selectedMonth;

            if (int.TryParse(expiryYear, out selectedYear) && int.TryParse(expiryMonth, out selectedMonth))
            {
                if (selectedYear > currentYear || (selectedYear == currentYear && selectedMonth >= currentMonth))
                {
                    return true;
                }
            }

            return false;
        }
        private bool ValidateUpiId(string upiId)
        {
            // UPI ID format: alphanumeric with '@', optional '.', and '-'
            string upiPattern = @"^[A-Za-z0-9]+@[A-Za-z0-9]+(\.[A-Za-z0-9]+)*(-[A-Za-z0-9]+)*$";

            return Regex.IsMatch(upiId, upiPattern);
        }


        protected void btnPayNowDebitCard_Click(object sender, EventArgs e)
        {
            string debitCardNumber = txtDebitCardNumber.Text.Trim();
            string debitCvv = txtDebitCvv.Text.Trim();
            string holdername = txtDebitCardholderName.Text;
            if (string.IsNullOrEmpty(holdername))
            {
                ErrorMessage.Text = "Please Enter your details";
            }
            // Get the selected expiry month and year
            string debitExpiryMonth = ddlDebitExpiryMonth.SelectedValue;
            string debitExpiryYear = ddlDebitExpiryYear.SelectedValue;
            Payment_failed.Text = "";

            // Validate CVV/CVC based on the card type
            if (ValidateCreditCardNumber(debitCardNumber) && ValidateCvv(debitCvv, debitCardNumber))
            {
                // Check if the selected expiry date is later than today's date
                if (IsExpiryDateValid(debitExpiryMonth, debitExpiryYear) && IsCardHolderNameValid(holdername))
                {
                    // Debit card information is valid, generate and store OTP


                    Session["PaymentOption"] = "DebitCard";
                    // Pass totalAmount to otp.aspx
                    // Redirect to the OTP page with total amount
                    int carID = Convert.ToInt32(Request.QueryString["CarID"]);
                    decimal tm = (decimal)Session["TotalAmount"];
                    Response.Redirect($"otp.aspx?carID={carID}&totalAmount={tm}");
                }
                else
                {
                    // Invalid expiry date, show an error message
                    DebitErrorMessage.Text = "Invalid expiry date. Please select a valid expiry date.or fill Card Holdeer name";

                    // Set the display style of the debit card form to "block"
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowDebitCardForm", "showPaymentForm('debitCard');", true);
                }
            }
            else
            {
                // Invalid debit card information, show an error message
                DebitErrorMessage.Text = "Invalid debit card information Or Invalid Cvv. Please enter valid details..";

                // Set the display style of the debit card form to "block"
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowDebitCardForm", "showPaymentForm('debitCard');", true);
            }
        }


        protected void btnPayNowUPI_Click(object sender, EventArgs e)
        {
            string upiId = txtUpiId.Text.Trim();
            if(string.IsNullOrEmpty(upiId))
            {
                UPIErrorMessage.Text = "Please enter your UPI ID ";
            }
            Payment_failed.Text = "";
            // Validate UPI ID
            if (ValidateUpiId(upiId))
            {
                // UPI ID is valid, proceed to OTP verification
                Session["EnteredUPI"] = upiId; // Store UPI ID for verification on the OTP page
                Session["PaymentOption"] = "UPI";
                // Pass totalAmount to otp.aspx
                // Redirect to the OTP page with total amount
                int carID = Convert.ToInt32(Request.QueryString["CarID"]);
                decimal tm = (decimal)Session["TotalAmount"];
                Response.Redirect($"otp.aspx?carID={carID}&totalAmount={tm}");
            }
            else
            {
                // Invalid UPI ID, show an error message
                UPIErrorMessage.Text = "Invalid UPI ID. Please enter a valid UPI ID.";

                // Set the display style of the UPI form to "block"
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowUpiForm", "showPaymentForm('upi');", true);
            }
        }



        private void UpdateReservationTable(string email, int carID, decimal totalAmount)
        {
            // Update Reservation table with TotalAmount and PaymentStatus for the latest reservation
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Retrieve the latest ReservationID for the specified user and vehicle
                    string latestReservationQuery = "SELECT TOP 1 ReservationID FROM Reservation WHERE Email = @Email AND VehicleID = @CarID ORDER BY ReservationID DESC";
                    SqlCommand latestReservationCmd = new SqlCommand(latestReservationQuery, conn);
                    latestReservationCmd.Parameters.AddWithValue("@Email", email);
                    latestReservationCmd.Parameters.AddWithValue("@CarID", carID);

                    object latestReservationResult = latestReservationCmd.ExecuteScalar();

                    if (latestReservationResult != null && latestReservationResult != DBNull.Value)
                    {
                        int latestReservationID = Convert.ToInt32(latestReservationResult);
                        Session["LatestReservagtionId"] = latestReservationID;
                        // Update the latest reservation
                        string updateQuery = "UPDATE Reservation SET TotalAmount = @TotalAmount, PaymentStatus = 'Cash' WHERE ReservationID = @ReservationID";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        updateCmd.Parameters.AddWithValue("@ReservationID", latestReservationID);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update successful
                            // You can add any additional logic or messages here
                        }
                        else
                        {
                            // Update failed, handle accordingly
                            // You can add any additional logic or messages here
                        }
                    }
                    else
                    {
                        // No reservation found for the specified user and vehicle
                        // Handle accordingly
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Response.Write($"Exception occurred during update: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            // Retrieve necessary information from Session or database
            string email = Session["Email"].ToString(); // Assuming user email is stored in Session
            int carID = Convert.ToInt32(Request.QueryString["CarID"]); // Assuming carID is passed in the query string

            // Retrieve total amount from Session
            decimal totalAmount = (decimal)Session["TotalAmount"];

            // Update Reservation table
            UpdateReservationTable(email, carID, totalAmount);

            // Redirect to homepage
            Response.Redirect("homepage.aspx");
        }


    }
}