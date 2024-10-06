using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentWeb
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("/HomePage.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }

            if (Session["Email"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else
            {

                // Fetch user details from the database
                string email = Convert.ToString(Session["Email"]);
                string firstName = GetUserFirstName(email);

                // Check and delete reservations with empty PaymentStatus and TotalAmount
                DeleteEmptyReservations(email);

                if (!string.IsNullOrEmpty(firstName))
                {
                    wel_label.Text = "Welcome";
                    wel_email.Text = firstName;
                }
                else
                {
                    // If the first name is empty, use a part of the email before the '@' symbol
                    int atIndex = email.IndexOf('@');
                    string userName = (atIndex != -1) ? email.Substring(0, atIndex) : email;

                    wel_email.Text = userName;
                }

                if (Session["LatestReservagtionId"] != null)
                {
                    int confirmedReservationID = Convert.ToInt32(Session["LatestReservagtionId"]);

                    // Display the confirmation message in the div
                    lblBookingConfirmation.Text = $"Your booking is confirmed, and your booking ID is {confirmedReservationID}";
                    bookingConfirmation.Visible = true;

                    // Clear the session variable after displaying the message
                    Session.Remove("LatestReservagtionId");
                }
                // Dynamically display the user's upcoming reservation
                DisplayUpcomingReservation(Session["Email"].ToString());
            }
        }
        private void DisplayUpcomingReservation(string userEmail)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string upcomingQuery = "SELECT TOP 1 R.ReservationID, R.Email, R.TotalAmount, R.PickupDate, R.ReturnDate, R.PaymentStatus, " +
                                           "V.LicensePlate, V.Model, V.Type, V.MainImage " +
                                           "FROM Reservation R " +
                                           "INNER JOIN Vehicle V ON R.VehicleID = V.VehicleID " +
                                           "WHERE R.Email = @Email AND R.ReturnDate >= GETDATE() " +
                                           "ORDER BY R.PickupDate ASC";
                    SqlCommand cmd = new SqlCommand(upcomingQuery, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string imageName = reader["MainImage"].ToString();
                        string imageURL = "Car_Images/" + imageName + ".jpg"; // Constructing the image URL

                        // Create a div to display the upcoming reservation
                        string reservationHtml = $@"
                    <div class='reservation'>
                        <div class='reservation-image'>
                            <img src='{imageURL}' alt='Car Image' />
                        </div>
                        <div class='reservation-content'>
                            <h3>Reservation ID: {reader["ReservationID"]}</h3>
                            <p>Vehicle Details:</p>
                            <p>License Plate: {reader["LicensePlate"]}</p>
                            <p>Model: {reader["Model"]}</p>
                            <p>Type: {reader["Type"]}</p>
                            <p>Email: {reader["Email"]}</p>
                            <p>Total Amount: {reader["TotalAmount"]}</p>
                            <p>Date: {reader["PickupDate"]} - {reader["ReturnDate"]}</p>
                            <p>Payment Status: {reader["PaymentStatus"]}</p>
                        </div>
                    </div>";

                        // Append the reservationHtml to the upcomingReservation div
                        upcomingReservation.Controls.Add(new LiteralControl(reservationHtml));
                    }
                    else
                    {
                        // If no upcoming reservation found, display a message and hide the link
                        upcomingReservation.Controls.Add(new LiteralControl("<p>No upcoming reservations</p>"));
                        view_more_link.Visible = false;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }


        // Function to get user's first name from the database
        private string GetUserFirstName(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT FirstName FROM User WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    object result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToString(result) : null;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    Console.WriteLine(ex.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        // Function to delete reservations with empty PaymentStatus and TotalAmount
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

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("index.aspx");
        }
    }
}
