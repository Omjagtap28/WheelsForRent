<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_services.aspx.cs" Inherits="CarRentWeb.Admin.a_services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="styles.css" />
    <title></title>
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
                    <li><a href="a_customers.aspx">Customers</a></li>
                    <li><a href="a_cars.aspx">Cars</a></li>
                    <li><a href="a_feedback.aspx">Feedback</a></li>
                    <li id="homeLink" runat="server"><a href="a_services.aspx">Reservations</a></li>
                    <li><a href="a_contacts.aspx">ContactHistory</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>
        <div class="Container">
            <h2>All Reservations Made by Users</h2>
            <!-- GridView for displaying reservations -->
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnRowDeleted="GridView1_RowDeleted" DataKeyNames="ReservationID">
                <Columns>
                    <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" ReadOnly="True" />
                    <asp:BoundField DataField="VehicleID" HeaderText="Vehicle ID" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PickupDate" HeaderText="Pickup Date" />
                    <asp:BoundField DataField="ReturnDate" HeaderText="Return Date" />
                    <asp:BoundField DataField="PickupLocation" HeaderText="Pickup Location" />
                    <asp:BoundField DataField="ReturnLocation" HeaderText="Return Location" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" />
                    <asp:BoundField DataField="LicenseNo" HeaderText="License Number" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>

            <!-- Data source for the GridView -->
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WheelsForRentConnectionString %>"
                SelectCommand="SELECT * FROM [Reservation]"
                DeleteCommand="DELETE FROM [Reservation] WHERE [ReservationID] = @ReservationID">
                <DeleteParameters>
                    <asp:Parameter Name="ReservationID" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>

            <!-- Error message label -->
            <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="White" BackColor="Green"></asp:Label>
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
