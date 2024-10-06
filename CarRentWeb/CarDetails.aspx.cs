using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentWeb
{
    public partial class CarDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["CarID"] != null)
                {
                    int carID = Convert.ToInt32(Request.QueryString["CarID"]);

                    // Update availability status before populating car details
                    //UpdateAvailabilityStatusIfNeeded(carID);

                    PopulateCarDetails(carID);
                    UpdateLoginMessage();
                }
                else
                {
                    // Handle case where car ID is not provided
                    Response.Redirect("Search.aspx");
                }

            }
        }


        private void PopulateCarDetails(int carID)
        {
            // Query the database to retrieve car details based on carID
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query the car table to get details
                    string query = "SELECT * FROM Vehicle WHERE VehicleID = @CarID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CarID", carID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Populate labels or other controls with retrieved data
                        lblType.Text = reader["Type"].ToString();
                        lblModel.Text =reader["Model"].ToString();
                        lblBrand.Text = reader["Brand"].ToString();
                        lblYear.Text = reader["Year"].ToString();
                        lblAvailable.Text = reader["AvailabilityStatus"].ToString();
                        lblAvailableFrom.Text = reader["AvailableFrom"].ToString();
                        lblPrice.Text = reader["Price"].ToString() + "/hrs";
                        lblLicense.Text = reader["LicensePlate"].ToString();
                        lblMileage.Text = reader["Mileage"].ToString();
                        // Add more labels for other details

                        Car.Text = reader["Brand"].ToString() + " " + reader["Model"];

                        string mainImage = reader["MainImage"].ToString();
                        string otherImages = reader["OtherImages"].ToString();

                        // Call JavaScript function to create slideshow for other images
                        string script = "<script type='text/javascript'>createSlideshow('" + otherImages + "');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "createSlideshow", script);

                        string availabilityStatus = reader["AvailabilityStatus"].ToString();

                        if (availabilityStatus == "Yes")
                        {
                            // If the availability status is 'Yes', hide the lblAvailableFrom label
                            lblAvailableFrom.Visible = true;
                        }
                    }
                    else
                    {
                        // Handle case where car ID is not found
                        Response.Redirect("Search.aspx");
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    // Redirect to an error page or back to the search page
                    Response.Redirect("Search.aspx");
                    Response.Write("error " + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void UpdateLoginMessage()
        {
            if (Session["Email"] != null)
            {
                // User is logged in, update the message or hide the container
                loginMessageContainer.Visible = false;
            }
            else
            {
                // User is not logged in, update the message
                loginMessageContainer.InnerHtml = "You need to login first in order to continue.";
            }
        }
        protected void btnRentNow_Click(object sender, EventArgs e)
        {
            int carID = Convert.ToInt32(Request.QueryString["CarID"]);


            if (Session["Email"] != null)
            {
                // User is logged in, redirect to Booking.aspx
                Response.Redirect("Booking.aspx?CarID=" + carID);
            }
            else
            {
                // User is not logged in, redirect to index.aspx
                Session["register_to_login"] = "true";
                Session["CarIDForRedirect"] = Request.QueryString["CarID"];
                
                Response.Redirect("index.aspx");
            }
        }



    }
}
