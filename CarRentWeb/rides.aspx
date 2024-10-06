<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rides.aspx.cs" Inherits="CarRentWeb.rides" EnableViewState="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/styles.css" />
    <script>
        function confirmDelete(reservationID) {
            return confirm("Are you sure you want to delete this reservation?");
        }
    </script>

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
                    <li id="homeLink" runat="server"><a href="rides.aspx">My Rides</a></li>
                    <li><a href="Feedback.aspx">Feedback</a></li>
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

        <!-- Div for upcoming reservations -->
        <div class="upcoming-reservations" runat="server">
            <h2>Upcoming Reservations</h2>
            <asp:Repeater ID="upcomingReservationsRepeater" runat="server">
                <ItemTemplate>
                    <div class="reservation">
                        <div class="reservation-image">
                            <img src='<%# "Car_Images/" + Eval("MainImage") + ".jpg" %>' alt="Car Image" />
                        </div>
                        <div class="reservation-content">
                            <h3>Reservation ID:
                                <asp:Literal runat="server" Text='<%# Eval("ReservationID") %>'></asp:Literal></h3>
                            <p>
                                Vehicle Details:
                                <br />
                                License Plate:
                                <asp:Literal runat="server" Text='<%# Eval("LicensePlate") %>'></asp:Literal><br />
                                Model:
                                <asp:Literal runat="server" Text='<%# Eval("Model") %>'></asp:Literal><br />
                                Type:
                                <asp:Literal runat="server" Text='<%# Eval("Type") %>'></asp:Literal>
                            </p>
                            <p>
                                Email:
                                <asp:Literal runat="server" Text='<%# Eval("Email") %>'></asp:Literal>
                            </p>
                            <p>
                                Total Amount:
                                <asp:Literal runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Literal>
                            </p>
                            <p>
                                Date: 
                        <asp:Literal runat="server" Text='<%# Eval("PickupDate", "{0:HH:mm dd-MM-yyyy}") %>'></asp:Literal>
                                - 
                        <asp:Literal runat="server" Text='<%# Eval("ReturnDate", "{0:HH:mm dd-MM-yyyy}") %>'></asp:Literal>
                            </p>
                            <p>
                                Payment Status:
                                <asp:Literal runat="server" Text='<%# Eval("PaymentStatus") %>'></asp:Literal>
                            </p>
                            <asp:Button ID="btnDeleteReservation" runat="server" Text="Delete Reservation" OnClientClick='<%# "return confirmDelete(" + Eval("ReservationID") + ");" %>' CssClass="delete-button" CommandArgument='<%# Eval("ReservationID") %>' OnClick="DeleteReservation_Click" />

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>



        <!-- Div for reservation history -->
        <div class="reservation-history" runat="server">
            <h2>Reservation History</h2>
            <asp:Repeater ID="upcomingHistoryRepeater" runat="server">
                <ItemTemplate>
                    <div class="reservation">
                        <div class="reservation-image">
                            <img src='<%# "Car_Images/" + Eval("MainImage") + ".jpg" %>' alt="Car Image" />
                        </div>
                        <div class="reservation-content">
                            <h3>Reservation ID:
                                <asp:Literal runat="server" Text='<%# Eval("ReservationID") %>'></asp:Literal></h3>
                            <p>
                                Vehicle Details:
                                <br />
                                License Plate:
                                <asp:Literal runat="server" Text='<%# Eval("LicensePlate") %>'></asp:Literal><br />
                                Model:
                                <asp:Literal runat="server" Text='<%# Eval("Model") %>'></asp:Literal><br />
                                Type:
                                <asp:Literal runat="server" Text='<%# Eval("Type") %>'></asp:Literal>
                            </p>
                            <p>
                                Email:
                                <asp:Literal runat="server" Text='<%# Eval("Email") %>'></asp:Literal>
                            </p>
                            <p>
                                Total Amount:
                                <asp:Literal runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Literal>
                            </p>
                            <p>
                                Date: 
                        <asp:Literal runat="server" Text='<%# Eval("PickupDate", "{0:HH:mm dd-MM-yyyy}") %>'></asp:Literal>
                                - 
                        <asp:Literal runat="server" Text='<%# Eval("ReturnDate", "{0:HH:mm dd-MM-yyyy}") %>'></asp:Literal>
                            </p>
                            <p>
                                Payment Status:
                                <asp:Literal runat="server" Text='<%# Eval("PaymentStatus") %>'></asp:Literal>
                            </p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
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
</body>
</html>
