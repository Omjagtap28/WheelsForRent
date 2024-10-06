<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_contacts.aspx.cs" Inherits="CarRentWeb.Admin.a_contacts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contacts</title>
    <link rel="stylesheet" href="styles.css" />
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
                    <img src="car2.jpg" alt="car rental" height="65" width="140" />
                </div>
                <ul class="ul2">
                    <li><a href="a_home.aspx">Home</a></li>
                    <li><a href="a_customers.aspx">Customers</a></li>
                    <li id="homeLink" runat="server"><a href="a_cars.aspx">Cars</a></li>
                    <li><a href="a_feedback.aspx">Feedback</a></li>
                    <li><a href="a_services.aspx">Reservations</a></li>
                    <li><a href="a_contacts.aspx">ContactHistory</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>

        <div class="Container">
            <h2>Contact Messages</h2>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ContactID, Email" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:BoundField DataField="ContactID" HeaderText="ContactID" InsertVisible="False" ReadOnly="True" SortExpression="ContactID" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WheelsForRentConnectionString %>" SelectCommand="SELECT * FROM [Contact]" DeleteCommand="DELETE FROM [Contact] WHERE Email = @Email">
                    <DeleteParameters>
                        <asp:Parameter Name="Email" Type="String" />
                    </DeleteParameters>
                </asp:SqlDataSource>
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
