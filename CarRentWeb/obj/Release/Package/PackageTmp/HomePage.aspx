<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="CarRentWeb.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="css/styles.css" />
    <link rel="stylesheet" href="css/Home.css" />
    <title>Home</title>
</head>
<body>
    <form runat="server" id="main">
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
                    <li id="homeLink" runat="server"><a href="index.aspx">Home</a></li>
                    <li><a href="about.aspx">About Us</a></li>
                    <li><a href="rides.aspx">My Rides</a></li>
                    <li><a href="Feedback.aspx">Feedback</a></li>
                    <li><a href="Search.aspx">Search Cars</a></li>
                    <li><a href="Contact.aspx">Contact Us</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="images/logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>
        <div class="Home_Container" runat="server" id="Home_Container">
            <div id="bookingConfirmation" runat="server" class="confirmationDiv" visible="false">
                <asp:Label ID="lblBookingConfirmation" runat="server" Text=""></asp:Label>
            </div>
            <div class="welcome_email">
                <asp:Label ID="wel_label" runat="server" Text="Welcome "></asp:Label>
                <br />
                <asp:Label ID="wel_email" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
            </div>


        </div>
        <div id="upcomingReservation" runat="server">
            <h2>Upcoming Reservation</h2>


            <div id="view_more_link" runat="server">
                <a href="rides.aspx">View More Reservations</a>
            </div>
        </div>
        <!-- Link to view more reservations -->



        <section class="featured-cars">
            <h2>Featured Cars</h2>
            <div class="carousel-container">
                <div class="carousel">
                    <!-- Displaying featured cars here -->
                    <div class="car">
                        <img src="Car_Images/KIA_SEL_1.jpg" alt="Car 1" />
                        <h3>KIA Seltos</h3>
                        <p>Price: 700/hrs</p>
                        <p>Year : 2018</p>
                        <p>Type: SUV</p>
                        <a href="CarDetails.aspx?carID=410">View Details</a>
                    </div>
                    <div class="car">
                        <img src="Car_Images/MAR_DZI_1.jpg" alt="Car 1" />
                        <h3>Maruti Dzire</h3>
                        <p>Price: 500/hrs</p>
                        <p>Year : 2018</p>
                        <p>Type: Sedan</p>
                        <a href="CarDetails.aspx?carID=415">View Details</a>
                    </div>
                    <div class="car">
                        <img src="Car_Images/TYT_INN_1.jpg" alt="Car 1" />
                        <h3>Toyota Innova</h3>
                        <p>Price: 800/hrs</p>
                        <p>Year : 2016</p>
                        <p>Type: MUV</p>
                        <a href="CarDetails.aspx?carID=417">View Details</a>
                    </div>
                    <div class="car">
                        <img src="Car_Images/TYT_COR_1.jpg" alt="Car 1" />
                        <h3>Toyota Corolla</h3>
                        <p>Price: 600/hrs</p>
                        <p>Year : 2015</p>
                        <p>Type: Sedan</p>
                        <a href="CarDetails.aspx?carID=418">View Details</a>
                    </div>
                    <!-- Repeating the above structure for each featured car -->
                </div>
            </div>
        </section>

        <!-- Our Services Section -->
        <section class="services">
            <h2>Our Services</h2>
            <!-- Describing our services here -->
            <ul>
                <li>Extended Business Hours: We're available from 5:00 AM to 12:00 AM to assist you.</li>
                <li>Wide Range of Vehicles: Choose from our diverse selection of cars, including sedans, SUVs, and luxury vehicles.</li>
                <li>Flexible Booking Options: Book your rental online or through our mobile app at your convenience.</li>
                <li>Affordable Rates: Enjoy competitive pricing and special discounts for long-term rentals.</li>
                <li>Comprehensive Insurance Coverage: Drive with peace of mind knowing you're protected with our insurance options.</li>
                <li>24/7 Roadside Assistance: Get assistance anytime, anywhere in case of emergencies or breakdowns.</li>
                <li>Clean and Well-Maintained Cars: Our fleet undergoes regular maintenance and cleaning for your safety and comfort.</li>
                <li>Easy Payment Options: Pay for your rental securely using various payment methods, including credit/debit cards and digital wallets.</li>
                <li>Additional Accessories: Enhance your driving experience with optional add-ons like GPS navigation systems and child seats.</li>
            </ul>
        </section>


        <!-- Customer Testimonials Section -->
        <h2>Customer Testimonials</h2>
        <section class="testimonials">

            <!-- Displaying customer testimonials here -->

            <div class="testimonial">
                <div class="testimonial-content">
                    <div class="testimonial-image">
                        <img src="Car_Images/Test.jpg" alt="John Doe" />
                    </div>
                    <div class="testimonial-details">
                        <h3>Rajesh Kumar</h3>
                        <p>"Great service! Highly recommended."</p>
                    </div>
                </div>
            </div>
            <div class="testimonial">
                <div class="testimonial-content">
                    <div class="testimonial-image">
                        <img src="Car_Images/Test2.jpg" alt="Jane Smith" />
                    </div>
                    <div class="testimonial-details">
                        <h3>Siddharth Singh</h3>
                        <p>"Amazing experience! Will definitely rent again."</p>
                    </div>
                </div>
            </div>
            <div class="testimonial">
                <div class="testimonial-content">
                    <div class="testimonial-image">
                        <img src="Car_Images/Test3.jpg" alt="Jane Smith" />
                    </div>
                    <div class="testimonial-details">
                        <h3>Nisha Patel</h3>
                        <p>"Amazing experience! Will definitely rent again."</p>
                    </div>
                </div>
            </div>
            <div class="testimonial">
                <div class="testimonial-content">
                    <div class="testimonial-image">
                        <img src="Car_Images/Test4.jpg" alt="Jane Smith" />
                    </div>
                    <div class="testimonial-details">
                        <h3>Arjun Mehta</h3>
                        <p>"Amazing experience! Will definitely rent again."</p>
                    </div>
                </div>
            </div>
            <!-- Repeating the testimonials as needed here-->
        </section>



        <!-- Our Car Brands Section -->
        <section class="car-brands">
            <h2>Our Car Brands</h2>
            <div class="brands-container">
                <!-- Displaying car brands logos/images here -->
                <div class="brand">
                    <img src="Cars/suzuki.png" alt="suzuki" />
                    <p>Suzuki</p>
                </div>
                <div class="brand">
                    <img src="Cars/honda-logo.png" alt="honda" />
                    <p>Honda</p>
                </div>
                <div class="brand">
                    <img src="Cars/hyundai.png" alt="Hyundai" />
                    <p>Hyundai</p>
                </div>
                <div class="brand">
                    <img src="Cars/KIA.png" alt="KIA" />
                    <p>KIA</p>
                </div>
                <div class="brand">
                    <img src="Cars/tata.png" alt="TATA" />
                    <p>TATA</p>
                </div>
                <div class="brand">
                    <img src="Cars/toyota.png" alt="Toyota" />
                    <p>Toyota</p>
                </div>
            </div>
        </section>


        <!-- Same content end -->



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
        <script src="js/logout.js"></script>
        <script>
            window.onload = function () {
                hideConfirmationMessage('<%= bookingConfirmation.ClientID %>');
            };
        </script>
    </form>
</body>
</html>
