using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace CarRentWeb.Admin
{
    public partial class a_customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IsAdmin"] == null)
            {
                Response.Redirect("~/index.aspx");
            }
            else
            {
                if (Request.Url.AbsolutePath.EndsWith("/a_customers.aspx"))
                {
                    // Add the CSS class to highlight the home page link
                    homeLink.Attributes["class"] = "highlight-homepage";
                }
                up_email.Text = Convert.ToString(Session["Admin"]);
            }
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/index.aspx");
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                // Handle the exception, e.g., display an error message
                e.ExceptionHandled = true;
                // Display a custom error message or log the error
            }
            else
            {
                // Optionally notify the admin of successful deletion
                ErrorMessage.Text = "Deleted Successfully !";
                // e.g., display a success message
            }
        }
    }
}