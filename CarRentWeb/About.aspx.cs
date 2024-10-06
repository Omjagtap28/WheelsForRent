using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CarRentWeb
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Url.AbsolutePath.EndsWith("/about.aspx"))
            {
                // Add the CSS class to highlight the home page link
                homeLink.Attributes["class"] = "highlight-homepage";
            }
        }

    }
}