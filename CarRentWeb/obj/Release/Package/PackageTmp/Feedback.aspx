<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="CarRentWeb.Feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/styles.css" />
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
                    <li id="Li1" runat="server"><a href="rides.aspx">My Rides</a></li>
                    <li id="homeLink" runat="server"><a href="Feedback.aspx">Feedback</a></li>
                    <li><a href="Search.aspx">Search Cars</a></li>
                    <li><a href="Contact.aspx">Contact Us</a></li>
                    <li>
                        <a href="Profile.aspx">
                            <img src="images/user-icon.png" />
                        </a>
                    </li>
                </ul>
            </nav>
        </header>
        <div class="Feedback_container">
            <div class="Container_f">
                <h2 id="formTitle">Feedback</h2>


                <asp:Label ID="l_rating" runat="server" Text="Rate us on a scale :"></asp:Label>
                <asp:DropDownList runat="server" ID="rating" placeholder="Select Rating">
                    <asp:ListItem Text="" Value="" />
                    <asp:ListItem Text="Very Poor" Value="1" />
                    <asp:ListItem Text="Poor" Value="2" />
                    <asp:ListItem Text="Average" Value="3" />
                    <asp:ListItem Text="Good" Value="4" />
                    <asp:ListItem Text="Excellent" Value="5" />
                </asp:DropDownList>

                <asp:Label ID="l_comment" runat="server" Text="Comment :"></asp:Label>
                <asp:TextBox runat="server" ID="comment" TextMode="MultiLine" placeholder="Enter your Comment"></asp:TextBox>


                <asp:Label ID="ErrorMessage" runat="server" Text="" Style="margin-left: 25%;"></asp:Label>
                <asp:Button ID="submitFeedbackButton" runat="server" Text="Submit Feedback" CssClass="subFeedbackButton" OnClick="submitFeedbackButton_Click" />

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
    <script src="js/script.js"></script>
</body>
</html>
