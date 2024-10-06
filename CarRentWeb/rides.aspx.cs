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
    public partial class rides : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath.EndsWith("/rides.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }
            if (!IsPostBack)
            {
                if (Session["Email"] == null)
                {
                    Session["PlzLogin"] = "PlzLogin";
                    Response.Redirect("index.aspx");
                }
                else
                {
                    DisplayUpcomingReservations(Session["Email"].ToString());
                    DisplayHistoryReservations(Session["Email"].ToString());
                }
            }
        }

        private void DisplayUpcomingReservations(string userEmail)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string upcomingQuery = "SELECT R.ReservationID, R.Email, R.TotalAmount, R.PickupDate, R.ReturnDate, R.PaymentStatus, " +
                                           "V.LicensePlate, V.Model, V.Type, V.MainImage " +
                                           "FROM Reservation R " +
                                           "INNER JOIN Vehicle V ON R.VehicleID = V.VehicleID " +
                                           "WHERE R.Email = @Email AND R.ReturnDate >= GETDATE() " +
                                           "ORDER BY R.PickupDate ASC";
                    SqlCommand cmd = new SqlCommand(upcomingQuery, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    upcomingReservationsRepeater.DataSource = reader;
                    upcomingReservationsRepeater.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }


        private void DisplayHistoryReservations(string userEmail)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string historyQuery = "SELECT R.ReservationID, R.Email, R.TotalAmount, R.PickupDate, R.ReturnDate, R.PaymentStatus, " +
                                          "V.LicensePlate, V.Model, V.Type, V.MainImage " +
                                          "FROM Reservation R " +
                                          "INNER JOIN Vehicle V ON R.VehicleID = V.VehicleID " +
                                          "WHERE R.Email = @Email AND R.ReturnDate < GETDATE() " +
                                          "ORDER BY R.PickupDate ASC";
                    SqlCommand cmd = new SqlCommand(historyQuery, conn);
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    SqlDataReader reader = cmd.ExecuteReader();

                    upcomingHistoryRepeater.DataSource = reader;
                    upcomingHistoryRepeater.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }



        protected void DeleteReservation_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int reservationID = Convert.ToInt32(btnDelete.CommandArgument);

            DeleteReservation(reservationID);
            DisplayUpcomingReservations(Session["Email"].ToString());
            DisplayHistoryReservations(Session["Email"].ToString());
        }

        private void DeleteReservation(int reservationID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WheelsForRentConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@ReservationID", reservationID);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Deletion successful
                    }
                    else
                    {
                        // Deletion failed
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
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
