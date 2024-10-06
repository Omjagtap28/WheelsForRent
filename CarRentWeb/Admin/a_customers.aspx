<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_customers.aspx.cs" Inherits="CarRentWeb.Admin.a_customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title>Index</title>
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
                    <img src="car2.jpg" alt="car rental" height="65" width="140" />
                </div>
                <ul class="ul2">
                    <li><a href="a_home.aspx">Home</a></li>
                    <li id="homeLink" runat="server"><a href="a_customers.aspx">Customers</a></li>
                    <li><a href="a_cars.aspx">Cars</a></li>
                    <li><a href="a_feedback.aspx">Feedback</a></li>
                    <li><a href="a_services.aspx">Reservations</a></li>
                    <li><a href="a_contacts.aspx">ContactHistory</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>
        <div class="Container">
            <h2>All Customers : </h2>

            <div class="selected_car"></div>
            <div class="all_cars">
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        DataSourceID="SqlDataSource1" DataKeyNames="UserID" OnRowDeleted="GridView1_RowDeleted">
                        <Columns>
                            <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" />
                            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" />
                            <asp:BoundField DataField="Password" HeaderText="Password" ReadOnly="true" />
                            <asp:BoundField DataField="Question" HeaderText="Security Question" ReadOnly="true" />
                            <asp:BoundField DataField="Answer" HeaderText="Answer" ReadOnly="true" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" ReadOnly="true" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="true" />
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone Number" ReadOnly="true" />
                            <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="true" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:WheelsForRentConnectionString %>"
                        SelectCommand="SELECT * FROM [User]"
                        DeleteCommand="DELETE FROM [User] WHERE UserID = @UserID">
                        <DeleteParameters>
                            <asp:Parameter Name="UserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
        <section class="footer">
            <div class="Logos">
                <div class="logo">
                    <a href="#">
                        <img src="icons8-facebook-48(1).png" alt="Facebook" /></a>
                    <a href="#">
                        <img src="icons8-whatsapp-48.png" alt="WhatsApp" /></a>
                    <a href="#">
                        <img src="icons8-instagram-48.png" alt="Instagram" /></a>
                    <a href="#">
                        <img src="icons8-twitter-48.png" alt="Twitter" /></a>
                    <a href="https://youtu.be/1-VFkunADw0?si=Fie3JXzulaa5ywwD">
                        <img src="icons8-youtube-48.png" alt="YouTube" /></a>
                </div>
            </div>
            <div class="text2">
                &copy; 2023 Wheels For Rent | All Rights Reserved
            </div>
        </section>
    </form>
    <script src="script.js"></script>
</body>
</html>
