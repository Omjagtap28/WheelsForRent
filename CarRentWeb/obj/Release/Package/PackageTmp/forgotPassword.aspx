<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotPassword.aspx.cs" Inherits="CarRentWeb.forgotPassword" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/styles.css">
    <!-- Include the same stylesheet as in index.aspx -->
    <title>Forgot Password</title>
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
                <li id="homeLink" runat="server"><a href="rides.aspx">My Rides</a></li>
                <li><a href="Feedback.aspx">Feedback</a></li>
                <li><a href="Search.aspx">Search Cars</a></li>
                <li><a href="Contact.aspx">Contact Us</a></li>
                <li><a id="showFormLink">
                    <img src="images/user-icon.png"></a></li>

            </ul>
        </nav>
    </header>

    <div class="Container">
        <!-- Use the same container class as in index.aspx -->
        <div class="Window">
            <div class="containerJ">
                <h2>Forgot Password</h2>
                <form id="forgotPasswordForm" runat="server">
                    <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label><br />

                    <asp:Label ID="forgotEmailLabel" runat="server" Text="Enter Email :"></asp:Label>
                    <asp:TextBox runat="server" ID="forgotEmail" placeholder="Enter your email"></asp:TextBox>
                    <asp:Button ID="submitEmailButton" runat="server" Text="Submit" OnClick="submitEmailButton_Click" />

                    <asp:Label ID="securityQuestionLabel" runat="server" Visible="false"></asp:Label>

                    <asp:TextBox runat="server" ID="securityAnswerTextbox" Visible="false" placeholder="Enter your answer"></asp:TextBox>
                    <asp:Button ID="verifyAnswerButton" runat="server" Text="Verify Answer" OnClick="verifyAnswerButton_Click" Visible="false" />
                    <asp:Label ID="newPasswordLabel" runat="server" Text="New Password:" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" ID="newPasswordTextbox" Visible="false" TextMode="Password"></asp:TextBox>

                    <asp:Label ID="confirmPasswordLabel" runat="server" Text="Confirm Password:" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" ID="confirmPasswordTextbox" Visible="false" TextMode="Password"></asp:TextBox>

                    <asp:Button ID="resetPasswordButton" runat="server" Text="Reset Password" OnClick="resetPasswordButton_Click" Visible="false" />

                </form>
            </div>
        </div>
    </div>

    <!-- Add this section for car brands after the testimonials section -->
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


    <script src="js/script.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</body>
</html>
