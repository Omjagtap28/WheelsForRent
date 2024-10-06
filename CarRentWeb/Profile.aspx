<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="CarRentWeb.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
    <link rel="stylesheet" href="css/profile.css" />
    <link rel="stylesheet" href="css/styles.css" />
</head>
<body>
    <form id="Profile_form" runat="server">
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
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="images/logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>
        <div class="ProfileContainer">
            <div class="profile_pic_email">
                <div class="profile_pic" alt="profile_image"></div>
                <div class="profile_email">
                    <asp:Label ID="txtEmail" runat="server" Text=""></asp:Label>
                </div>

            </div>
            <div class="profile_details">
                <h2>User Profile</h2>
                <div class="profile_detail">
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" ReadOnly="true" PlaceHolder="********" Visible="true" /><br />
                    </div>
                </div>
                <div class="profile_detail">
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtFirstName">First Name:</asp:Label>
                        <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="true" MaxLength="10" /><br />
                    </div>
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtLastName">Last Name:</asp:Label>
                        <asp:TextBox ID="txtLastName" runat="server" ReadOnly="true" MaxLength="10" /><br />
                    </div>
                </div>
                <div class="profile_detail">
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtPhoneNo">Phone Number:</asp:Label>
                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="true" MaxLength="12" /><br />
                    </div>
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtAddress">Address:</asp:Label>
                        <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" MaxLength="30" /><br />
                    </div>
                </div>
                <div class="profile_detail">
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="securityQuestion">Security Question:</asp:Label>
                        <asp:DropDownList ID="securityQuestion" runat="server" Width="300px">
                            <asp:ListItem Value="1">What is your bestfriend name?</asp:ListItem>
                            <asp:ListItem Value="2">What is the name of your first pet?</asp:ListItem>
                            <asp:ListItem Value="3">In which city were you born?</asp:ListItem>
                            <asp:ListItem Value="4">What is your favorite book?</asp:ListItem>
                            <asp:ListItem Value="5">What is the name of your best childhood friend?</asp:ListItem>
                            <asp:ListItem Value="6">What is the model of your first car?</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="profile_detail_d">
                        <asp:Label runat="server" AssociatedControlID="txtAnswer">Security Answer:</asp:Label>
                        <asp:TextBox ID="txtAnswer" runat="server" ReadOnly="true" /><br />
                    </div>
                </div>

                <div class="profile_detail_btns">
                    <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
                    <div class="profile_detail_b">

                        <asp:Button ID="profileEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                        <asp:Button ID="profileSubmit" runat="server" Text="Submit" OnClick="SubmitButton_Click" Visible="false" />
                        <asp:Button ID="profileCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
                    </div>
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
    <script src="js/logout.js"></script>
</body>
</html>
