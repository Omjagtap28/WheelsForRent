<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarDetails.aspx.cs" Inherits="CarRentWeb.CarDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/styles.css" />
    <script src="js/slideshow.js"></script>
    <!-- Load slideshow.js first -->
    <script src="js/script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                    <li id="homeLink" runat="server"><a href="rides.aspx">My Rides</a></li>
                    <li><a href="Feedback.aspx">Feedback</a></li>
                    <li><a href="Search.aspx">Search Cars</a></li>
                    <li><a href="Contact.aspx">Contact Us</a></li>
                    <li><a id="showFormLink">
                        <img src="images/user-icon.png" /></a></li>
                </ul>
            </nav>
        </header>
        <!-- ---------------------------------------------------------------------------  -->

        <div class="CarDetailsContainer">
            <asp:Label ID="Car" runat="server" Text="Car Details"></asp:Label>
            <div class="img_container" runat="server">
            </div>


            <div class="car-details">

                <div class="details">

                    <div class="column">
                        <div class="detail">
                            <label for="lblBrand">Brand:</label>
                            <asp:Label ID="lblBrand" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblModel">Model:</label>
                            <asp:Label ID="lblModel" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblType">Type:</label>
                            <asp:Label ID="lblType" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblMileage">Mileage:</label>
                            <asp:Label ID="lblMileage" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblYear">Year:</label>
                            <asp:Label ID="lblYear" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblAvailable">Availability:</label>
                            <asp:Label ID="lblAvailable" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="column">
                        <div class="detail">
                            <label for="lblAvailableFrom">Available from:</label>
                            <asp:Label ID="lblAvailableFrom" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblLicense">License:</label>
                            <asp:Label ID="lblLicense" runat="server" CssClass="car-label" Text=""></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblSpecifications">Vehicle Specifications:</label>
                            <asp:Label ID="lblSpecifications" runat="server" CssClass="car-label" Text="4 Seats"></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblInterior">Interior Features:</label>
                            <asp:Label ID="lblInterior" runat="server" CssClass="car-label" Text="BT"></asp:Label>
                        </div>

                        <div class="detail">
                            <label for="lblExterior">Exterior Features:</label>
                            <asp:Label ID="lblExterior" runat="server" CssClass="car-label" Text="Sun Roof"></asp:Label>
                        </div>
                    </div>
                </div>


                <div class="price-section">
                    <label for="lblPrice">Price:</label>
                    <asp:Label ID="lblPrice" runat="server" CssClass="car-label price-label" Text=""></asp:Label>
                    <div id="loginMessageContainer" runat="server" class="login-message-container" style="display: none; position: absolute; background-color: #f8d7da; color: #721c24; padding: 10px; border: 1px solid #f5c6cb; border-radius: 5px;"></div>
                    <asp:Button ID="btnRentNow" runat="server" Text="Rent Now" OnClick="btnRentNow_Click" onmouseover="showLoginMessage()" onmouseout="hideLoginMessage()" CssClass="btnRentNow" Style="margin-top: 10px; border: 2px solid black;" />
                    <asp:Label ID="ErrorMessage" runat="server" CssClass="error-message"></asp:Label>
                </div>
            </div>
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
    </form>
</body>
</html>
