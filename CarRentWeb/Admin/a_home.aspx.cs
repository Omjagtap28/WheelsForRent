using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace CarRentWeb.Admin
{
    public partial class a_home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null)
            {
                Response.Redirect("~/index.aspx");
            }
            else
            {
                if (Request.Url.AbsolutePath.EndsWith("/a_home.aspx"))
                {
                    // Add the CSS class to highlight the home page link
                    homeLink.Attributes["class"] = "highlight-homepage";
                }
                Time.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy HH:mm:ss");
                Admin();
                up_email.Text = Convert.ToString(Session["Admin"]);
            }

            if (!IsPostBack)
            {
                // Call a method to get and display the total number of customers
                DisplayTotalCustomers();


                CalculateTotalEarning();

                CalculateLastMonthEarning();
                // Call a method to generate and display the tape graph
                GenerateTapeGraph();



                // Call CalculateBookingUtilization method to get the booking utilization
                double bookingUtilization = CalculateBookingUtilization();

                // RegisterStartupScript to call gaugeChart.js with bookingUtilization parameter
                string script = $"window.onload = function() {{ gaugeChart({bookingUtilization}); }};";
                ClientScript.RegisterStartupScript(this.GetType(), "RenderGaugeChart", script, true);
            }
        }


        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/index.aspx");
        }
        private void DisplayTotalCustomers()
        {
            // Your connection string, replace it with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // Your SQL query to get the total number of customers
            string query = "SELECT COUNT(*) FROM [User]"; // Assuming your customers table is named "Customers"
            string ratingQuery = "SELECT CAST(AVG(CAST(Rating AS DECIMAL(10,2))) AS DECIMAL(10,2)) FROM [Feedback]\r\n";
            string carsQuery = "SELECT COUNT(*) FROM [VEHICLE]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                try
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Execute the query and get the total count
                        int totalCustomers = (int)command.ExecuteScalar();
                        int tc = totalCustomers;
                        // Display the total number of customers
                        t_users.Text = Convert.ToString(tc);
                    }

                    using (SqlCommand command = new SqlCommand(carsQuery, conn))
                    {
                        int totalCars = (int)command.ExecuteScalar();
                        int tc = totalCars;
                        Session["TotalCars"] = tc;
                        t_cars.Text = Convert.ToString(tc);
                    }

                    //ratings ----------------

                    using (SqlCommand ratingCommand = new SqlCommand(ratingQuery, conn))
                    {
                        object result = ratingCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {

                            decimal averageRating = Convert.ToDecimal(result);

                            // Display the average rating
                            t_rating.Text = averageRating.ToString("0.00");
                        }
                        else
                        {
                            t_rating.Text = "N/A"; // or any default value if there's no rating
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You may want to log the error or show an error message to the user
                    // For simplicity, I'm just printing the error to the console
                    Console.WriteLine("Error: " + ex.Message);
                    t_users.Text = "Error retrieving total customers." + ex.Message;
                    t_rating.Text = "Error retrieving average rating." + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        

        private void GenerateTapeGraph()
        {
            // Your connection string, replace it with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // Your SQL query to get feedback data
            string feedbackQuery = "SELECT Rating, COUNT(*) AS Count FROM Feedback GROUP BY Rating";

            List<FeedbackData> feedbackDataList = new List<FeedbackData>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(feedbackQuery, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int rating = Convert.ToInt32(reader["Rating"]);
                            int count = Convert.ToInt32(reader["Count"]);
                            feedbackDataList.Add(new FeedbackData(rating, count));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You may want to log the error or show an error message to the user
                    // For simplicity, I'm just printing the error to the console
                    Console.WriteLine("Error: " + ex.Message);
                    ErrorMessage.Text = "Error retrieving feedback data: " + ex.Message;
                }
            }

            // Construct the JavaScript function call with the feedbackData array
            string script = "GenerateTapeGraph([";

            // Iterate through each feedback data object and construct the JavaScript object
            for (int i = 0; i < feedbackDataList.Count; i++)
            {
                script += "{ rating: " + feedbackDataList[i].Rating + ", count: " + feedbackDataList[i].Count + " }";

                // Add comma if it's not the last item
                if (i < feedbackDataList.Count - 1)
                {
                    script += ", ";
                }
            }

            script += "]);";

            // Register the script with the page
            ClientScript.RegisterStartupScript(this.GetType(), "GenerateTapeGraph", script, true);
        }
        // Method to calculate and display the total earning
        private void CalculateTotalEarning()
        {
            // Your connection string, replace it with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // Your SQL query to get the total earning
            string earningQuery = "SELECT ISNULL(SUM(TotalAmount), 0) AS TotalEarning FROM Reservation";

            // Initialize total earning variable
            decimal totalEarning = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Execute query to get the total earning
                    using (SqlCommand command = new SqlCommand(earningQuery, conn))
                    {
                        // ExecuteScalar method to get the total earning
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // Convert the result to decimal
                            totalEarning = Convert.ToDecimal(result);
                        }
                    }

                    // Display the total earning
                    t_earning.Text = totalEarning.ToString("C"); // "C" format specifier for currency
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You may want to log the error or show an error message to the user
                    // For simplicity, I'm just printing the error to the console
                    Console.WriteLine("Error: " + ex.Message);
                    t_earning.Text = "Error retrieving total earning." + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void CalculateLastMonthEarning()
        {
            // Your connection string, replace it with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // Get the start and end dates of the last month
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // SQL query to get the total earning for the last month
            string earningQuery = @"
        SELECT ISNULL(SUM(TotalAmount), 0) AS TotalEarning 
        FROM Reservation 
        WHERE PickUpDate >= @StartDate AND ReturnDate <= @EndDate";

            // Initialize total earning variable
            decimal lastMonthEarning = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Execute query to get the total earning for the last month
                    using (SqlCommand command = new SqlCommand(earningQuery, conn))
                    {
                        // Add parameters to the SQL command
                        command.Parameters.AddWithValue("@StartDate", firstDayOfMonth);
                        command.Parameters.AddWithValue("@EndDate", lastDayOfMonth);

                        // ExecuteScalar method to get the total earning
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // Convert the result to decimal
                            lastMonthEarning = Convert.ToDecimal(result);
                        }
                    }

                    // Display the total earning for the last month
                    t_mon_earning.Text = lastMonthEarning.ToString("C"); // "C" format specifier for currency
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // You may want to log the error or show an error message to the user
                    // For simplicity, I'm just printing the error to the console
                    Console.WriteLine("Error: " + ex.Message);
                    t_mon_earning.Text = "Error retrieving last month earning." + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }




        public class FeedbackData
        {
            public int Rating { get; set; }
            public int Count { get; set; }

            public FeedbackData(int rating, int count)
            {
                Rating = rating;
                Count = count;
            }
        }

        // Gauge chart ---------------------------------------------------------------------------------------
        private double CalculateBookingUtilization()
        {
            // Assume you have a method to retrieve the total number of vehicles available for booking
            int totalVehicles = Convert.ToInt32(Session["TotalCars"]);

            // Assume you have a method to retrieve the number of booked vehicles
            int bookedVehicles = GetBookedVehicles();

            // Calculate the booking utilization percentage
            double bookingUtilization = (double)bookedVehicles / totalVehicles * 100;

            // Return the booking utilization percentage
            return bookingUtilization;
        }

        private int GetBookedVehicles()
        {
            int bookedVehicles = 0;

            // Connection string for your database
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // SQL query to count the number of distinct vehicle IDs for the current date
            string query = $@"
        SELECT COUNT(DISTINCT VehicleId) 
        FROM Reservation 
        WHERE CONVERT(DATE, PickUpDate) <= CONVERT(DATE, GETDATE()) 
        AND (CONVERT(DATE, ReturnDate) >= CONVERT(DATE, GETDATE()) OR ReturnDate IS NULL)";

            // Create and open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the command and get the result
                    bookedVehicles = (int)command.ExecuteScalar();
                }
            }

            return bookedVehicles;
        }

        public void Admin()
        {
            // Provide the correct connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // SQL query to retrieve the admin email
            string query = "SELECT Email FROM Admin";

            // Create a new SqlConnection and SqlCommand objects
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();

                    // Execute the query to retrieve the admin email
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string adminEmail = result.ToString();
                        Session["Admin"] = adminEmail;
                    }
                    else
                    {
                        // Handle the case where no email is found
                        Session["Admin"] = "No email found";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("Error: " + ex.Message);
                    // You may want to log the error or show an error message to the user
                }
            }
        }


    }
}