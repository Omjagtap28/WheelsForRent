<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CarRentWeb.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="css/styles.css" />
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
                <li id="homeLink" runat="server"><a href="index.aspx">Home</a></li>
                <li><a href="about.aspx">About Us</a></li>
                <li><a href="rides.aspx">My Rides</a></li>
                <li><a href="Feedback.aspx">Feedback</a></li>
                <li><a href="Search.aspx">Search Cars</a></li>
                <li><a href="Contact.aspx">Contact Us</a></li>
                <li><a id="showFormLink" href="index.aspx">
                    <img src="images/user-icon.png" /></a></li>
            </ul>
        </nav>
    </header>
    <div class="Container">
        <div class="Window">
            <div class="containerJ">
                <h2 id="formTitle">Register</h2>
                <br />
                <form id="registerForm" runat="server">
                    <label for="registerUsername">Email:</label>
                    <asp:TextBox ID="registerUsername" runat="server" placeholder="Enter the username" />

                    <label for="registerPassword">Password:</label>
                    <asp:TextBox ID="registerPassword" runat="server" placeholder="Enter the password" TextMode="Password" />
                    <label for="registerPassword_c">Confirm Password:</label>
                    <asp:TextBox ID="registerPassword_c" runat="server" placeholder="Re-Enter the password" TextMode="Password" />
                    <label for="securityQuestion">Security Question:</label>
                    <asp:DropDownList ID="securityQuestion" runat="server" Width="300px">
                        <asp:ListItem Value="1">What is your bestfriend name?</asp:ListItem>
                        <asp:ListItem Value="2">What is the name of your first pet?</asp:ListItem>
                        <asp:ListItem Value="3">In which city were you born?</asp:ListItem>
                        <asp:ListItem Value="4">What is your favorite book?</asp:ListItem>
                        <asp:ListItem Value="5">What is the name of your best childhood friend?</asp:ListItem>
                        <asp:ListItem Value="6">What is the model of your first car?</asp:ListItem>
                    </asp:DropDownList>

                    <label for="securityAnswer">Answer:</label>
                    <asp:TextBox ID="securityAnswer" runat="server" placeholder="Enter your answer" />

                    <asp:Button ID="registerButton" runat="server" Text="Register" OnClick="registerButton_Click" BackColor="#0BB5FF" ForeColor="White" />
                    <asp:Label ID="ErrorMessage" runat="server" BackColor="Green" ForeColor="White"></asp:Label>

                    <asp:Button ID="Register_or_Login" runat="server" Text="Login" OnClick="Register_or_Login_Click" />

                </form>
            </div>
        </div>
        <div class="text">
            Your ride, your style - Unleash luxury on the road!
            <br />
            <p>Start your unforgettable journey with "Wheels For Rent"</p>
        </div>
        <div class="buttons">
            <button id="b1"></button>
            <button id="b2"></button>
        </div>
    </div>
    <div class="middle">
        <p>Please Remember your Password and Security Answer !</p>
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
    <script src="js/try.js"></script>

</body>
</html>

