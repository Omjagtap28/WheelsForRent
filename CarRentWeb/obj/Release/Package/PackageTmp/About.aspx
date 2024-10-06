<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CarRentWeb.About" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/styles.css">
    <link rel="stylesheet" href="css/about.css">
    <title>Index</title>
</head>
<body>
    <form>
        <header>
            <nav class="nav1">
                <ul>
                    <li>7779998881</li>
                    <asp:Label ID="up_email" runat="server" Text="omjagtap39@gmail.com"></asp:Label>
                </ul>
            </nav>
            <nav class="nav2">
                <div class="Logo">
                    <img src="images/car2.jpg" alt="car rental" height="65" width="140">
                </div>
                <ul class="ul2">
                    <li><a href="index.aspx">Home</a></li>
                    <li id="homeLink" runat="server"><a href="about.aspx">About Us</a></li>
                    <li id="Li1" runat="server"><a href="rides.aspx">My Rides</a></li>
                    <li><a href="Feedback.aspx">Feedback</a></li>
                    <li><a href="Search.aspx">Search Cars</a></li>
                    <li><a href="Contact.aspx">Contact Us</a></li>
                    <li>
                        <a id="showFormLink" href="#" onclick="showFormLink_Click()">
                            <img src="images/user-icon.png" />
                        </a>
                    </li>

                </ul>
            </nav>
        </header>


        <div class="about-container">
            <div class="about_us">
                <h1>About Us</h1>
                <div class="black">
                    <p>
                        Welcome to Wheels For Rent - your ultimate destination for renting cars of your choice!
                    </p>
                    <p>
                        At Wheels For Rent, we are committed to providing you with the best car rental experience, offering a wide range of vehicles to suit your needs and preferences.
Whether you need a compact car for a city trip, a spacious SUV for a family vacation, or a luxury vehicle for a special occasion, we have got you covered.
Our team of dedicated professionals strives to ensure your satisfaction by delivering clean, well-maintained cars and excellent customer service.
With flexible booking options, affordable rates, and comprehensive insurance coverage, you can enjoy peace of mind and convenience every time you rent with us.
Experience the joy of driving your dream car with Wheels For Rent!
Contact us now to book your next ride!
                    </p>
                </div>

            </div>

            <h2>How to Rent a Car</h2>
            <section class="video-container">

                <div class="video">
                    <video controls width="100%">
                        <!-- Set width to 100% to make the video responsive -->
                        <source src="Vid/WheelsForRent.mp4" type="video/mp4">
                        Your browser does not support the video tag.
                    </video>
                </div>

                <div class="rental-process">
                    <h3>Rental Process</h3>
                    <p>Renting a car with Wheels For Rent is quick and easy. Follow these simple steps to book your next ride:</p>
                    <ol>
                        <li>Choose Your Vehicle: Browse our wide selection of vehicles and choose the one that suits your needs.</li>
                        <li>Make a Reservation: Select your pickup and drop-off dates, locations, and any additional options you may need.</li>
                        <li>Complete Your Booking: Fill out the required information and confirm your reservation.</li>
                        <li>Pick Up Your Car: Visit our rental location at the scheduled time to pick up your reserved vehicle.</li>
                        <li>Enjoy Your Ride: Hit the road and enjoy the freedom and flexibility of driving your rented car.</li>
                        <li>Return the Car: Bring back the vehicle to our rental location at the end of your rental period.</li>
                    </ol>
                    <p>With Wheels For Rent, renting a car is a hassle-free experience. Start your journey today!</p>
                </div>
            </section>


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
