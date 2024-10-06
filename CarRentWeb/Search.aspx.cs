using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CarRentWeb
{
    public partial class Search : Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("/Search.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }

            if (!IsPostBack)
            {
                BindCarGallery("");
            }
        }

        private void BindCarGallery(string searchKeyword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            string query = "SELECT VehicleID, MainImage, Brand, Model, Price, AvailableFrom,LicensePlate,Year FROM Vehicle";

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query += $" WHERE Brand LIKE '%{searchKeyword}%' OR Model LIKE '%{searchKeyword}%'";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) // If search result is empty
                {
                    all_cars.Text = "All Cars";


                    reader.Close();
                    // Query to retrieve all cars
                    SqlCommand allCarsCommand = new SqlCommand("SELECT VehicleID, MainImage, Brand, Model, Price, AvailableFrom,Year,LicensePlate FROM Vehicle", connection);
                    SqlDataReader allCarsReader = allCarsCommand.ExecuteReader();

                    while (allCarsReader.Read())
                    {
                        string vehicleID = allCarsReader["VehicleID"].ToString();
                        string mainImage = allCarsReader["MainImage"].ToString();
                        string brand = allCarsReader["Brand"].ToString();
                        string model = allCarsReader["Model"].ToString();
                        string price = allCarsReader["Price"].ToString();
                        string year = allCarsReader["Year"].ToString();
                        string license = allCarsReader["LicensePlate"].ToString();
                        DateTime availableFrom = Convert.ToDateTime(allCarsReader["AvailableFrom"]); // Convert to DateTime

                        // Calculate AvailableFrom for each car
                        availableFrom = CalculateAvailableFrom(Convert.ToInt32(vehicleID));

                        // Update AvailableFrom in the database
                        UpdateAvailableFromInDatabase(Convert.ToInt32(vehicleID), availableFrom);

                        // Dynamically create car items and add them to the gallery
                        carGallery.Controls.Add(CreateCarItem(vehicleID, mainImage, brand, model, price, availableFrom.ToString("dd-MM-yyyy HH:mm"),year,license)); // Convert to string
                    }

                    allCarsReader.Close();
                }
                else
                {
                    all_cars.Text = searchKeyword + " Cars";

                    while (reader.Read())
                    {
                        string vehicleID = reader["VehicleID"].ToString();
                        string mainImage = reader["MainImage"].ToString();
                        string brand = reader["Brand"].ToString();
                        string model = reader["Model"].ToString();
                        string price = reader["Price"].ToString();
                        string year = reader["Year"].ToString();
                        string license = reader["LicensePlate"].ToString();
                        //DateTime availableFrom = Convert.ToDateTime(reader["AvailableFrom"]); // Convert to DateTime

                        // Calculate AvailableFrom for each car
                        DateTime availableFrom = CalculateAvailableFrom(Convert.ToInt32(vehicleID));

                        // Update AvailableFrom in the database
                        UpdateAvailableFromInDatabase(Convert.ToInt32(vehicleID), availableFrom);

                        // Dynamically create car items and add them to the gallery
                        carGallery.Controls.Add(CreateCarItem(vehicleID, mainImage, brand, model, price, availableFrom.ToString("dd-MM-yyyy HH:mm"),year,license)); // Convert to string
                    }
                }

                reader.Close();
            }
        }



        private HtmlGenericControl CreateCarItem(string vehicleID, string mainImage, string brand, string model, string price, string availableFrom, string year,string license)
        {
            HtmlGenericControl carItemDiv = new HtmlGenericControl("div");
            carItemDiv.Attributes["class"] = "car-item";

            // Create image element with a link to CarDetails.aspx
            HtmlAnchor imageLink = new HtmlAnchor();
            imageLink.HRef = $"CarDetails.aspx?CarID={vehicleID}";

            HtmlImage image = new HtmlImage();
            image.Src = $"Car_Images/{mainImage}.jpg" ;
            image.Alt = "Car Image";
            image.Style["max-width"] = "100%";
            image.Style["height"] = "200px";
            image.Style["border"] = "2px solid #333";
            image.Style["border-radius"] = "8px";
            image.Style["margin-bottom"] = "10px";

            // Add image to image link
            imageLink.Controls.Add(image);

            // Add image link to car item div
            carItemDiv.Controls.Add(imageLink);

            // Create description div
            HtmlGenericControl descriptionDiv = new HtmlGenericControl("div");
            descriptionDiv.Attributes["class"] = "car-description";
            descriptionDiv.InnerHtml = $"<div class=\"car-summary\">\r\n            <img src=\"images/yellowcar.png\" />&nbsp; &nbsp;\r\n            <p><b>{brand} &nbsp; {model}</b></p>\r\n        </div>\r\n        <div class=\"car-summary\">\r\n            <img src=\"images/yellowrupee.png\" />&nbsp; &nbsp;\r\n            <p>Price: {price}/hr</p>\r\n        </div>\r\n        <div class=\"car-summary\">\r\n            <img src=\"images/yellow_cal.png\" />&nbsp; &nbsp;\r\n            <p>Year : {year}</p>\r\n        </div>\r\n        <div class=\"car-summary\">\r\n            <img src=\"images/yellow_license.png\" />&nbsp; &nbsp;\r\n            <p>License : {license}</p>\r\n        </div>\r\n        <div class=\"car-summary\">\r\n            <img src=\"images/clock.png\" />&nbsp; &nbsp;\r\n            <p>Available From: {availableFrom}</p>\r\n        </div>\r\n        <div class=\"car-summary-link\">\r\n            <a href=\"CarDetails.aspx?CarID={vehicleID}\">More Details...</a>\r\n        </div>";

            // Add description div to car item div
            carItemDiv.Controls.Add(descriptionDiv);

            return carItemDiv;
        }





        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            BindCarGallery(search);
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

                    string query = "SELECT PickupDate, ReturnDate FROM Reservation WHERE VehicleID = @CarID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime pickupDate = Convert.ToDateTime(reader["PickupDate"]);
                        DateTime returnDate = Convert.ToDateTime(reader["ReturnDate"]);
                        reservations.Add(new Reservation(pickupDate, returnDate));
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

        private DateTime CalculateAvailableFrom(int carID)
        {
            // Retrieve existing reservations and sort them by pickup date
            List<Reservation> reservations = GetExistingReservations(carID)
                .Where(r => r.ReturnDate >= DateTime.Today) // Filter out reservations that are in the past
                .OrderBy(r => r.PickupDate)
                .ToList();

            DateTime currentTime = DateTime.Now;
            DateTime maxDate = DateTime.Now.AddMonths(3); // Maximum date allowed for booking

            // Business hours start time
            TimeSpan businessStart = new TimeSpan(6, 0, 0); // 6:00 AM
                                                            // Business hours end time
            TimeSpan businessEnd = new TimeSpan(23, 0, 0); // 11:00 PM

            
            // Add a buffer of 2 hours to the current time
            currentTime = currentTime.AddHours(2);

            // Check if there are any reservations after adding the buffer
            var nextReservation = reservations.FirstOrDefault(r => r.PickupDate <= currentTime && r.ReturnDate >= currentTime);

            currentTime = currentTime.AddHours(-2);

            if (nextReservation != null)
            {
                // If there is a reservation, check if the return date falls within business hours
                if (nextReservation.ReturnDate.TimeOfDay <= businessEnd)
                {
                    currentTime = nextReservation.ReturnDate.AddHours(2); // Add a buffer of 2 hours
                }
                else
                {
                    // Find the next business day at 6 am
                    currentTime = nextReservation.ReturnDate.Date.AddDays(1).Add(businessStart);
                }
            }
            else
            {
                // If there are no reservations, check if the current time is within business hours
                if (currentTime.TimeOfDay >= businessStart && currentTime.TimeOfDay <= businessEnd)
                {
                    // If current time is within business hours, add a buffer of 2 hours
                    currentTime = currentTime.AddHours(2);
                }
                else
                {
                    // If current time is outside business hours, find the next business day at 6 am
                    currentTime = currentTime.Date.AddDays(1).Add(businessStart);
                }
            }

            // Ensure the calculated date does not exceed the maximum booking period
            return currentTime <= maxDate ? currentTime : maxDate;
        }


        private void UpdateAvailableFromInDatabase(int vehicleID, DateTime availableFrom)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE Vehicle SET AvailableFrom = @AvailableFrom WHERE VehicleID = @VehicleID", connection);
                command.Parameters.AddWithValue("@AvailableFrom", availableFrom);
                command.Parameters.AddWithValue("@VehicleID", vehicleID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}