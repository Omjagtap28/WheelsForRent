<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="CarRentWeb.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/styles.css">
    <title>Index</title>
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
                <img src="images/car2.jpg" alt="car rental" height="65" width="140">
            </div>
            <ul class="ul2">
                <li id="homeLink" runat="server"><a href="index.aspx">Home</a></li>
                <li><a href="about.aspx">About Us</a></li>
                <li id="Li1" runat="server"><a href="rides.aspx">My Rides</a></li>
                <li><a href="Feedback.aspx">Feedback</a></li>
                <li><a href="Search.aspx">Search Cars</a></li>
                <li><a href="Contact.aspx">Contact Us</a></li>
                <li><a id="showFormLink">
                    <img src="images/user-icon.png" /></a></li>
            </ul>
        </nav>
    </header>
    <!-- ----------------------------------------------------------------------------->
    <div class="BackgroundContainer">
        <div class="Container">
            <div class="Window">
                <div id="loginFormContainer" class="hidden" runat="server">
                    <div class="containerJ">
                        <h2 id="formTitle">Login</h2>
                        <br />
                        <form id="loginForm" runat="server" action="#">

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <!-- controls here -->
                                    <label for="loginUsername">Email:</label>
                                    <asp:TextBox runat="server" ID="loginUsername" placeholder="Enter the Email"></asp:TextBox>

                                    <label for="loginPassword">Password:</label>
                                    <asp:TextBox runat="server" ID="loginPassword" placeholder="Enter the password" TextMode="Password"></asp:TextBox>
                                    <asp:HyperLink ID="forgot_password" runat="server" Text="Forgot Password?" NavigateUrl="~/forgotPassword.aspx"></asp:HyperLink>
                                    <br />
                                    <div class="radios">
                                        <div class="radio">
                                            <asp:Label ID="Customer" Text="Customer" runat="server"></asp:Label>
                                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="UserType" Height="10px" Width="10px" />
                                        </div>
                                        <div class="radio">
                                            <asp:Label ID="Admin" Text="Admin" runat="server"></asp:Label>
                                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="UserType" Height="10px" Width="10px" /><br />
                                        </div>
                                    </div>
                                    <br />

                                    <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" BackColor="#0BB5FF" ForeColor="White"></asp:Button>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:Button ID="Register_or_Login" runat="server" Text="Register " OnClick="Register_or_Login_Click" />
                        </form>
                    </div>
                </div>
            </div>
            <!-- ---------------------------------------------------------------------------  -->
            <div class="text">
                Your ride, your style - Unleash luxury on the road!
            <br>
                <p>Start your unforgettable journey with "Wheels For Rent"</p>
            </div>
            <div class="buttons">
                <button id="b1"></button>
                <button id="b2"></button>
            </div>
        </div>
    </div>

    <section class="featured-cars">
        <h2>Featured Cars</h2>
        <div class="carousel-container">
            <div class="carousel">
                <!-- Displaying featured cars here -->
                <div class="car">
                    <img src="Car_Images/KIA_SEL_1.jpg" alt="Car 1">
                    <h3>KIA Seltos</h3>
                    <p>Price: 700/hrs</p>
                    <p>Year : 2018</p>
                    <p>Type: SUV</p>
                    <a href="CarDetails.aspx?carID=410">View Details</a>
                </div>
                <div class="car">
                    <img src="Car_Images/MAR_DZI_1.jpg" alt="Car 1">
                    <h3>Maruti Dzire</h3>
                    <p>Price: 500/hrs</p>
                    <p>Year : 2018</p>
                    <p>Type: Sedan</p>
                    <a href="CarDetails.aspx?carID=415">View Details</a>
                </div>
                <div class="car">
                    <img src="Car_Images/TYT_INN_1.jpg" alt="Car 1">
                    <h3>Toyota Innova</h3>
                    <p>Price: 800/hrs</p>
                    <p>Year : 2016</p>
                    <p>Type: MUV</p>
                    <a href="CarDetails.aspx?carID=417">View Details</a>
                </div>
                <div class="car">
                    <img src="Car_Images/TYT_COR_1.jpg" alt="Car 1">
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
    <h2 style="font-size: 2.5rem; color: #333; text-align: center; margin-bottom: 30px; text-decoration: underline;">Customer Testimonials</h2>
    <section class="testimonials">

        <!-- Displaying customer testimonials here -->

        <div class="testimonial">
            <div class="testimonial-content">
                <div class="testimonial-image">
                    <img src="Car_Images/Test.jpg" alt="John Doe">
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
                    <img src="Car_Images/Test2.jpg" alt="Jane Smith">
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
                    <img src="Car_Images/Test3.jpg" alt="Jane Smith">
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
                    <img src="Car_Images/Test4.jpg" alt="Jane Smith">
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
                <img src="Cars/suzuki.png" alt="suzuki">
                <p>Suzuki</p>
            </div>
            <div class="brand">
                <img src="Cars/honda-logo.png" alt="honda">
                <p>Honda</p>
            </div>
            <div class="brand">
                <img src="Cars/hyundai.png" alt="Hyundai">
                <p>Hyundai</p>
            </div>
            <div class="brand">
                <img src="Cars/KIA.png" alt="KIA">
                <p>KIA</p>
            </div>
            <div class="brand">
                <img src="Cars/tata.png" alt="TATA">
                <p>TATA</p>
            </div>
            <div class="brand">
                <img src="Cars/toyota.png" alt="Toyota">
                <p>Toyota</p>
            </div>
        </div>
    </section>

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



    <!-- ---------------------------------------------------------------------------  -->

    <script src="js/script.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="js/try.js"></script>


</body>
</html>
