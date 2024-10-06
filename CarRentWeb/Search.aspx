<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="CarRentWeb.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/styles.css" />
    <title>Search</title>
</head>
<body>
    <header>
        <nav class="nav1">
            <ul>
                <li>7779998881</li>
                <asp:Label ID="up_email" runat="server" Text="omjagtap39@gmail.com"></asp:Label>
            </ul>
        </nav>
        <nav class="nav2">
            <div class="Logo">
                <img src="images/car2.jpg" alt="car rental" height="65" width="140" />
            </div>
            <ul class="ul2">
                <li><a href="index.aspx">Home</a></li>
                <li><a href="about.aspx">About Us</a></li>
                <li id="Li1" runat="server"><a href="rides.aspx">My Rides</a></li>
                <li><a href="Feedback.aspx">Feedback</a></li>
                <li id="homeLink" runat="server"><a href="Search.aspx">Search Cars</a></li>
                <li><a href="Contact.aspx">Contact Us</a></li>
                <li>
                    <a id="showFormLink" href="#" onclick="showFormLink_Click()">
                        <img src="images/user-icon.png" />
                    </a>
                </li>
            </ul>
        </nav>
    </header>
    <div class="ServicesContainer">
        <form id="SearchForm" runat="server">
            <div id="search-container">
                <asp:TextBox ID="txtSearch" runat="server" Text="" placeholder="Eg. Maruti or Honda"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
            <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
            <asp:Label ID="all_cars" runat="server" Text="All Cars"></asp:Label>
            <section class="searches">
                <!-- Inside the .car-gallery div -->
                <div class="car-gallery" id="carGallery" runat="server">
                    <!-- Car items will be dynamically generated here -->
                </div>
            </section>
        </form>
    </div>
    <section class="footer">
        <div class="Logos">
            <div class="logo">
                <a href="https://www.facebook.com">
                    <img src="images/icons8-facebook-48(1).png" alt="Facebook"></a>
                <a href="https://www.whatsapp.com">
                    <img src="images/icons8-whatsapp-48.png" alt="WhatsApp"></a>
                <a href="https://www.instagram.com">
                    <img src="images/icons8-instagram-48.png" alt="Instagram"></a>
                <a href="https://www.twitter.com">
                    <img src="images/icons8-twitter-48.png" alt="Twitter"></a>
                <a href="https://www.youtube.com">
                    <img src="images/icons8-youtube-48.png" alt="YouTube"></a>
            </div>
        </div>
        <div class="text2">
            &copy; 2023 Wheels For Rent | OM JAGTAP
        </div>
    </section>

    <script src="js/services.js"></script>
    <script type="text/javascript">
        function showFormLink_Click() {
            // Redirect to profile.aspx if user is logged in, otherwise to index.aspx
            var isLoggedIn = <%= Session["Email"] != null ? "true" : "false" %>;

            if (isLoggedIn) {
                window.location.href = "profile.aspx";
            } else {
                window.location.href = "index.aspx";
            }
        }
    </script>
</body>
</html>

