using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static CarRentWeb.Search;

namespace CarRentWeb
{
    public class Reservation
    {
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Reservation(DateTime pickupDate, DateTime returnDate)
        {
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }
    }
    public partial class Booking : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/index.aspx");
            }
            int carID = Convert.ToInt32(Request.QueryString["CarID"]);
            DeleteEmptyReservations(Session["Email"].ToString());

            
            // Set the email and make the field read-only
            if (!IsPostBack) // Only set the value if it's not a postback
            {
                carID = Convert.ToInt32(Request.QueryString["CarID"]);
                string availabilityStatus = GetAvailabilityStatus(carID);
                DateTime availableFromDate = GetAvailableFromDate(carID);
                
                // Set the minimum date for returnDateTime based on availability status
                if (availabilityStatus == "Booked")
                {
                    txtPickupDateTime.Attributes.Add("min", availableFromDate.AddDays(1).ToString("yyyy-MM-ddTHH:mm"));
                    txtReturnDateTime.Attributes.Add("min", availableFromDate.AddDays(1).AddHours(1.5).ToString("yyyy-MM-ddTHH:mm"));
                }
                else
                {
                    txtReturnDateTime.Attributes.Add("min", DateTime.Now.AddHours(4).ToString("yyyy-MM-ddTHH:00"));
                }
                txtEmail.Text = Session["Email"].ToString();
                txtPickupDateTime.Text = DateTime.Now.AddHours(2).ToString("yyyy-MM-ddTHH:00");
                txtReturnDateTime.Text = DateTime.Now.AddHours(4).ToString("yyyy-MM-ddTHH:00");// Default pickup date to current date
                                                                                               // Check if the return location is the same as the pickup location
                if (chkSameReturnLocation.Checked)
                {
                    txtReturnLocation.Text = txtPickupLocation.Text;
                }

                // check wheter user exits or not then fill auto
                string email = Session["Email"].ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Check if user details are present in the User table
                        string checkUserQuery = "SELECT FirstName, LastName, PhoneNo, Address FROM [User] WHERE Email=@Email";
                        SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, conn);
                        checkUserCmd.Parameters.AddWithValue("@Email", email);

                        SqlDataReader reader = checkUserCmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // User details found, populate the form fields and make them read-only
                            txtFirstName.Text = reader["FirstName"].ToString();
                            txtLastName.Text = reader["LastName"].ToString();
                            txtPhone.Text = reader["PhoneNo"].ToString();
                            txtAddress.Text = reader["Address"].ToString();

                            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtPhone.Text == "" || txtAddress.Text == "")
                            {
                                // Disable editing of user details
                                txtFirstName.ReadOnly = false;
                                txtLastName.ReadOnly = false;
                                txtPhone.ReadOnly = false;
                                txtAddress.ReadOnly = false;
                            }
                            else
                            {
                                // Disable editing of user details
                                txtFirstName.ReadOnly = true;
                                txtLastName.ReadOnly = true;
                                txtPhone.ReadOnly = true;
                                txtAddress.ReadOnly = true;
                            }
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = "Exception occurred: " + ex.Message;
                        return;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            // Corrected method call to match the actual method name
            List<Reservation> reservations = GetExistingReservations(carID);

            // Create a <ul> element to hold the booking history
            HtmlGenericControl ulElement = new HtmlGenericControl("ul");

            // Loop through the available reservation times and create <li> elements
            foreach (var reservation in reservations)
            {
                // Create an <li> element for each reservation
                HtmlGenericControl liElement = new HtmlGenericControl("li");
                string startTime = reservation.PickupDate.ToString("dd-MM-yyyy HH:mm");
                string endTime = reservation.ReturnDate.ToString("dd-MM-yyyy HH:mm");
                liElement.InnerText = $"From: {startTime} to {endTime}";

                // Add the <li> element to the <ul> element
                ulElement.Controls.Add(liElement);
            }

            // Add the <ul> element to the placeholder <div> element
            divBookingHistory.Controls.Add(ulElement);


        }
        private string GetAvailabilityStatus(int carID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT AvailabilityStatus FROM Vehicle WHERE VehicleID = @CarID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);

                    object result = cmd.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return string.Empty;
                }
            }
        }
        private void DeleteEmptyReservations(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Delete reservations with empty PaymentStatus and TotalAmount for the current user
                    string deleteQuery = "DELETE FROM Reservation WHERE Email = @Email AND (PaymentStatus IS NULL OR TotalAmount IS NULL)";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@Email", email);

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

        private DateTime GetAvailableFromDate(int carID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT AvailableFrom FROM Vehicle WHERE VehicleID = @CarID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);

                    object result = cmd.ExecuteScalar();

                    return (result != null && result != DBNull.Value) ? Convert.ToDateTime(result) : DateTime.MinValue;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return DateTime.MinValue;
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate form fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPickupDateTime.Text) ||
                string.IsNullOrWhiteSpace(txtReturnDateTime.Text) ||
                string.IsNullOrWhiteSpace(txtPickupLocation.Text) ||
                string.IsNullOrWhiteSpace(txtReturnLocation.Text) ||
                string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
            {
                // Handle validation error, display a message or redirect as needed
                ErrorMessage.Text = "All fields are required.";
                return;
            }

            // Validate Indian phone number format
            if (!IsValidIndianPhoneNumber(txtPhone.Text))
            {
                // Handle phone number format error, display a message or redirect as needed
                ErrorMessage.Text = "Enter the right phone no format";
                return;
            }

            // User table--------------------------------------------
            int carID = Convert.ToInt32(Request.QueryString["CarID"]);
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string email = Session["Email"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Update the User table with additional information
                    string updateQuery = "UPDATE [User] SET FirstName=@FirstName, LastName=@LastName, PhoneNo=@Phone, Address=@Address WHERE Email=@Email";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);

                    updateCmd.Parameters.AddWithValue("@FirstName", firstName);
                    updateCmd.Parameters.AddWithValue("@LastName", lastName);
                    updateCmd.Parameters.AddWithValue("@Phone", phone);
                    updateCmd.Parameters.AddWithValue("@Address", address);
                    updateCmd.Parameters.AddWithValue("@Email", email);

                    updateCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }

            //Reservation Table -----------------------------------------------------
            DateTime pickupDateTime = DateTime.ParseExact(txtPickupDateTime.Text, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            DateTime returnDateTime = DateTime.ParseExact(txtReturnDateTime.Text, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);

            // Check if the selected time slots are available
            if (!IsReservationTimeAvailable(carID, pickupDateTime, returnDateTime))
            {
                // Display an error message and prevent booking
                ErrorMessage.Text = "The selected pickup and return times overlap with an existing reservation.";
                return;
            }


            // Validate that returnDateTime is later than pickupDateTime
            if (returnDateTime <= pickupDateTime)
            {
                ErrorMessage.Text = "Return date and time must be later than pickup date and time.";
                return;
            }

            // Validate that the difference between pickup and return is at least two hours
            if ((returnDateTime - pickupDateTime).TotalHours < 2)
            {
                ErrorMessage.Text = "The difference between pickup and return must be at least two hours.";
                return;
            }

            // Check reservation availability
            if (!IsReservationTimeAvailable(carID, pickupDateTime, returnDateTime))
            {
                ErrorMessage.Text = "The selected pickup and return times overlap with an existing reservation.";
                return;
            }

            // Validate maximum advance booking period (3 months)
            DateTime maxAdvanceBookingDate = DateTime.Now.AddMonths(1);
            if (pickupDateTime > maxAdvanceBookingDate)
            {
                ErrorMessage.Text = "Maximum advance booking period allowed is 1 months.";
                return;
            }

            // Validate maximum rental period (3 months)
            DateTime maxRentalPeriod = pickupDateTime.AddMonths(1);
            if (returnDateTime > maxRentalPeriod)
            {
                ErrorMessage.Text = "Maximum allowed rental period is 1 months.";
                return;
            }

            // Validate driver's license format
            string licenseNumber = txtLicenseNumber.Text.Trim();
            if (!IsValidLicenseFormat(licenseNumber))
            {
                ErrorMessage.Text = "Invalid driver's license format. Please enter a valid license number.";
                return;
            }
            // Check if pickup and return times are within the allowed range (after 5 am and before 12 am)
            if (pickupDateTime.Hour < 5 || returnDateTime.Hour >= 0 && returnDateTime.Hour < 5)
            {
                ErrorMessage.Text = "Pickup and return times must be between 5 am and 12 am.";
                return;
            }

            string pickupLocation = txtPickupLocation.Text.Trim();
            string returnLocation = txtReturnLocation.Text.Trim();



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Insert data into the Reservation table
                    string insertQuery = "INSERT INTO Reservation (VehicleID, Email, PickupDate, ReturnDate, PickupLocation, ReturnLocation, LicenseNo) VALUES (@VehicleID, @Email, @PickupDate, @ReturnDate, @PickupLocation, @ReturnLocation, @LicenseNo)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);

                    insertCmd.Parameters.AddWithValue("@VehicleID", carID);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@PickupDate", pickupDateTime);
                    insertCmd.Parameters.AddWithValue("@ReturnDate", returnDateTime);
                    insertCmd.Parameters.AddWithValue("@PickupLocation", pickupLocation);
                    insertCmd.Parameters.AddWithValue("@ReturnLocation", returnLocation);
                    insertCmd.Parameters.AddWithValue("@LicenseNo", licenseNumber);


                    insertCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }
            // availability status updation ------------------------
            // Get the maximum return date for reservations of the same vehicle
            DateTime maxReturnDate;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string maxReturnDateQuery = "SELECT MAX(ReturnDate) FROM Reservation WHERE VehicleID = @VehicleID AND ReturnDate > @ReturnDate";
                SqlCommand maxReturnDateCmd = new SqlCommand(maxReturnDateQuery, conn);
                maxReturnDateCmd.Parameters.AddWithValue("@VehicleID", carID);
                maxReturnDateCmd.Parameters.AddWithValue("@ReturnDate", returnDateTime);
                object maxReturnDateResult = maxReturnDateCmd.ExecuteScalar();
                maxReturnDate = (maxReturnDateResult != DBNull.Value && maxReturnDateResult != null) ? Convert.ToDateTime(maxReturnDateResult) : DateTime.MinValue;
            }

            DateTime availableFromDate;

            // If there's no previous reservation or the maximum return date is at the maximum SQL Server DateTime value, set the availableFromDate to the current date
            if (maxReturnDate == DateTime.MinValue || maxReturnDate >= DateTime.MaxValue.AddDays(-1))
            {
                availableFromDate = DateTime.Now;
            }
            else
            {
                // Otherwise, set the availableFromDate to one day after the maximum return date
                availableFromDate = maxReturnDate.AddDays(1);
            }
            // Check if availableFromDate falls between 12 am to 5 am
            availableFromDate = availableFromDate.AddHours(3);

            if (availableFromDate.Hour >= 0 && availableFromDate.Hour < 5)
            {
                // Subtract the current hour and add 5 hours to set it to 5 am on the same date
                availableFromDate = availableFromDate.AddHours(5 - availableFromDate.Hour);
            }



            // Adjust availableFromDate by adding 3 hours

            // Update the Vehicle table with the new availability status and available from date
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Update the Vehicle table
                    string updateVehicleQuery = "UPDATE Vehicle SET AvailabilityStatus = @AvailabilityStatus, AvailableFrom = @AvailableFrom WHERE VehicleID = @VehicleID";
                    SqlCommand updateVehicleCmd = new SqlCommand(updateVehicleQuery, conn);

                    // Set the parameters for the update query
                    updateVehicleCmd.Parameters.AddWithValue("@AvailabilityStatus", "Yes"); // Update with the appropriate status
                    updateVehicleCmd.Parameters.AddWithValue("@AvailableFrom", availableFromDate); // Update with the next available date
                    updateVehicleCmd.Parameters.AddWithValue("@VehicleID", carID);

                    // Execute the update query
                    updateVehicleCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }

            
            Response.Redirect("Payment.aspx?CarID=" + carID);
            // Continue processing the form submission
        }


        private bool IsValidIndianPhoneNumber(string phoneNumber)
        {
            // Validate Indian phone number format here
            // You can use a regular expression or any other validation logic

            // For example, a basic regex pattern for an Indian mobile number
            // assuming 10 digits and starting with 7, 8, or 9
            var regex = new System.Text.RegularExpressions.Regex(@"^[789]\d{9}$");
            return regex.IsMatch(phoneNumber);
        }

        private bool IsValidLicenseFormat(string licenseNumber)
        {
            // Define the regular expression for the Indian driving license format
            var regex = new System.Text.RegularExpressions.Regex(@"^[A-Z]{2}[0-9]{2}[0-9]{4}[0-9]{7}$");

            // Check if the entered license number matches the pattern
            return regex.IsMatch(licenseNumber);
        }

        private bool IsReservationTimeAvailable(int carID, DateTime pickupDateTime, DateTime returnDateTime)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Reservation WHERE VehicleID = @CarID " +
                                   "AND ((PickupDate < @ReturnDate AND ReturnDate > @PickupDate) " + // Check for overlap
                                   "OR (PickupDate <= @PickupDate AND ReturnDate >= @ReturnDate))"; // Check if entirely contained
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);
                    cmd.Parameters.AddWithValue("@PickupDate", pickupDateTime);
                    cmd.Parameters.AddWithValue("@ReturnDate", returnDateTime);

                    int count = (int)cmd.ExecuteScalar();

                    return count == 0;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        private List<Reservation> GetExistingReservations(int carID)
        {
            List<Reservation> reservations = new List<Reservation>();

            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Retrieve reservations within the next month, including ongoing reservations
                    DateTime currentDate = DateTime.Now;
                    string query = "SELECT PickupDate, ReturnDate FROM Reservation WHERE VehicleID = @CarID " +
                                   "AND ReturnDate >= @CurrentDate";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);
                    cmd.Parameters.AddWithValue("@CurrentDate", currentDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime pickupDate = Convert.ToDateTime(reader["PickupDate"]);
                        DateTime returnDate = Convert.ToDateTime(reader["ReturnDate"]);

                        // Check if the reservation overlaps with the current time
                        if (pickupDate <= currentDate && returnDate >= currentDate)
                        {
                            reservations.Add(new Reservation(pickupDate, returnDate));
                        }
                        else if (pickupDate > currentDate) // Only add upcoming reservations
                        {
                            reservations.Add(new Reservation(pickupDate, returnDate.AddHours(2))); // Add 2 hours buffer
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ErrorMessage.Text = "Exception occurred: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return reservations;
        }

    }
}
