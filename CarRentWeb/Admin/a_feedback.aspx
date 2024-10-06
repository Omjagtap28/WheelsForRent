<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_feedback.aspx.cs" Inherits="CarRentWeb.Admin.a_feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <title>Index</title>
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
                    <li><a href="a_cars.aspx">Cars</a></li>
                    <li id="homeLink" runat="server"><a href="a_feedback.aspx">Feedback</a></li>
                    <li><a href="a_services.aspx">Reservations</a></li>
                    <li><a href="a_contacts.aspx">ContactHistory</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>
        <div class="Container">
            <h2>All Feedbacks of Users</h2>
            <div>
                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" OnRowDeleted="GridView1_RowDeleted" DataKeyNames="FeedbackID">
                    <Columns>
                        <asp:BoundField DataField="FeedbackID" HeaderText="Feedback ID" SortExpression="FeedbackID" ReadOnly="True" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Rating" HeaderText="Rating" SortExpression="Rating" />
                        <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WheelsForRentConnectionString %>"
                    SelectCommand="SELECT FeedbackID, Rating, Comment, Email, Date FROM [Feedback]"
                    DeleteCommand="DELETE FROM [Feedback] WHERE [FeedbackID] = @FeedbackID">
                    <DeleteParameters>
                        <asp:Parameter Name="FeedbackID" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
                <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="White" BackColor="Green"></asp:Label>
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
