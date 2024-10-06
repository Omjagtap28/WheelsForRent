using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentWeb.Admin
{
    public partial class a_cars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null)
            {
                Response.Redirect("~/index.aspx");
            }
            else
            {
                if (Request.Url.AbsolutePath.EndsWith("/a_cars.aspx"))
                {
                    // Add the CSS class to highlight the home page link
                    homeLink.Attributes["class"] = "highlight-homepage";
                }
                up_email.Text = Convert.ToString(Session["Admin"]);
                CalculateRevenueByBrand();
            }
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/index.aspx");
        }
        private void CalculateRevenueByBrand()
        {
            // Your connection string, replace it with your actual connection string
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            // SQL query to calculate revenue by car brand
            string revenueQuery = @"
        SELECT 
            SUM(R.TotalAmount) AS Revenue, 
            V.Brand
        FROM 
            Reservation R
        INNER JOIN Vehicle V ON R.VehicleId = V.VehicleId
        WHERE 
            V.Brand IN ('Maruti', 'Honda', 'Mahindra', 'Tata', 'Hyundai', 'KIA')
        GROUP BY 
            V.Brand";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Execute the query
                    using (SqlCommand command = new SqlCommand(revenueQuery, conn))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Loop through the results and display revenue for each car brand
                        while (reader.Read())
                        {
                            string brand = reader["Brand"].ToString();
                            decimal revenue = Convert.ToDecimal(reader["Revenue"]);

                            // Assign the revenue to respective labels
                            if (brand == "Maruti")
                            {
                                Maruti_Car.Text = revenue.ToString("C"); // Display revenue for Maruti
                            }
                            if (brand == "Honda")
                            {
                                Honda_Car.Text = revenue.ToString("C"); // Display revenue for Honda
                            }
                            if (brand == "Mahindra")
                            {
                                Mahindra_Car.Text = revenue.ToString("C"); // Display revenue for Honda
                            }
                            if (brand == "TATA")
                            {
                                Tata_Car.Text = revenue.ToString("C"); // Display revenue for Honda
                            }
                            if (brand == "KIA")
                            {
                                Kia_Car.Text = revenue.ToString("C"); // Display revenue for Honda
                            }
                            if (brand == "Hyundai")
                            {
                                Hyundai_Car.Text = revenue.ToString("C"); // Display revenue for Honda
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine("Error: " + ex.Message);
                    ErrorMessage.Text = "Error: " + ex.Message;
                    // You can display an error message or handle it as per your application's requirements
                }
                finally
                {
                    conn.Close();
                }
            }
        }






    }
}