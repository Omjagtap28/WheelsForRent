<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_cars.aspx.cs" Inherits="CarRentWeb.Admin.a_cars" %>

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

            <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
            <div class="cars_race">
                <h3>Car Brands Revenue(All)</h3>
                <div>
                    <label for="Maruti_Car">Maruti : </label>
                    <asp:Label ID="Maruti_Car" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label for="Honda">Honda : </label>
                    <asp:Label ID="Honda_Car" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label for="Mahindra">Mahindra : </label>
                    <asp:Label ID="Mahindra_Car" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label for="TATA">TATA : </label>
                    <asp:Label ID="Tata_Car" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label for="KIA">KIA : </label>
                    <asp:Label ID="Kia_Car" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <label for="Hyundai">Hyundai : </label>
                    <asp:Label ID="Hyundai_Car" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <h2>All Cars : </h2>
            <div>

                <div class="GridViewContainer">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"></asp:GridView>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\LocalDbDemo;Initial Catalog=WheelsForRent;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Vehicle]"></asp:SqlDataSource>

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
