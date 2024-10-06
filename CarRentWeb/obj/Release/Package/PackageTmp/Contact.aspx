<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="CarRentWeb.Contact" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/styles.css">
    <title>Contact Us</title>

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
                <li><a href="index.aspx">Home</a></li>
                <li><a href="about.aspx">About Us</a></li>
                <li><a href="rides.aspx">My Rides</a></li>
                <li><a href="Feedback.aspx">Feedback</a></li>
                <li><a href="Search.aspx">Search Cars</a></li>
                <li id="homeLink" runat="server"><a href="Contact.aspx">Contact Us</a></li>
                <li>
                    <a id="showFormLink" href="#" onclick="showFormLink_Click()">
                        <img src="images/user-icon.png" />
                    </a>
                </li>
            </ul>
        </nav>
    </header>
    <div class="Contactcontainer">
        <div class="upper-part">
            <div class="upper-left">
                <h2>Get In Touch</h2>
                <h2>___________________________________</h2>
                <p>Feel free to reach out to us using the contact form below or through the provided email and phone number.</p>

                <form runat="server" id="contactForm">
                    <div class="one-line">
                        <label for="name">Name:</label>
                        <asp:TextBox runat="server" ID="txtName" placeholder="Your Name"></asp:TextBox>

                        <label for="email">Email:</label>
                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Your Email"></asp:TextBox>
                    </div>
                    <label for="subject">Subject:</label>
                    <asp:TextBox runat="server" ID="txtSubject" TextMode="SingleLine" placeholder="Your Subject"></asp:TextBox>

                    <label for="message">Message:</label>
                    <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" Rows="4" placeholder="Your Message"></asp:TextBox>

                    <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
                    <div class="contactbtnSubmit">
                        <asp:Button runat="server" ID="btnSubmit" Text="Send Message" OnClick="btnSubmit_Click" />
                    </div>
                </form>
            </div>
            <div class="upper-right">
                <h2>Contact Details</h2>
                <h2>___________________________________</h2>
                <p>Feel free to contact us during our business hours:</p>
                <ul class="contact-details">
                    <li>Phone: 7779998881</li>
                    <li>Email: omjagtap39@gmail.com</li>
                    <li>Address: Your Car Rental Address Goes Here</li>
                </ul>
                <h2>Frequently Asked Questions</h2>

                <div class="faq-item">
                    <h3 class="question">Q: How can I make a reservation? <span class="arrow">
                        <img src="images/down_arrow.png" /></span></h3>
                    <div class="answer">
                        <p>A: You can make a reservation by visiting our website and using the online booking form. Alternatively, you can contact our customer service team via phone or email.</p>
                    </div>
                </div>
                <div class="faq-item">
                    <h3 class="question">Q: What types of cars do you offer? <span class="arrow">
                        <img src="images/down_arrow.png" /></span></h3>
                    <div class="answer">
                        <p>A: We offer a variety of cars including sedans, SUVs, vans, and luxury vehicles. You can view our available inventory on our website or contact us for more information.</p>
                    </div>
                </div>
                <div class="faq-item">
                    <h3 class="question">Q: Do you offer insurance for rental cars? <span class="arrow">
                        <img src="images/down_arrow.png" /></span></h3>
                    <div class="answer">
                        <p>A: Yes, we offer insurance options for rental cars. Our customer service team can provide details about our insurance coverage and help you choose the right option for your needs.</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="lower-part">
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15079.232577321622!2d72.937027!3d19.14925!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3be7c9188c85c4b5%3A0x119fdb619bce059c!2sStatue%20of%20Liberty%2C%20New%20York%2C%20NY!5e0!3m2!1sen!2sin!4v1645278499317!5m2!1sen!2sin&zoom=15" allowfullscreen></iframe>
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
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const questions = document.querySelectorAll('.question');
            questions.forEach(function (question) {
                question.addEventListener('click', function () {
                    const arrow = this.querySelector('.arrow');
                    const answer = this.nextElementSibling;

                    if (answer.classList.contains('active')) {
                        // If the clicked answer is already active, hide it
                        arrow.classList.remove('active');
                        answer.classList.remove('active');
                    } else {
                        // Close all answers
                        const allAnswers = document.querySelectorAll('.answer');
                        allAnswers.forEach(function (answer) {
                            answer.classList.remove('active');
                        });
                        // Show the clicked question's answer
                        arrow.classList.add('active');
                        answer.classList.add('active');
                    }
                });
            });
        });
    </script>
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


