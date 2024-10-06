<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a_home.aspx.cs" Inherits="CarRentWeb.Admin.a_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="styles.css" />
    <script src="canvasjs.min.js"></script>
    <script>
        function gaugeChart(bookingUtilization) {
            var gaugeChart = new CanvasJS.Chart('gaugeChartContainer', {
                animationEnabled: true,
                title: {
                    text: 'Booking Utilization'
                },
                axisY: {
                    minimum: 0,
                    maximum: 100,
                    suffix: '%'
                },
                legend: {
                    cursor: 'pointer',
                    verticalAlign: 'top',
                    fontSize: 16,
                    itemclick: function (e) {
                        e.dataSeries.visible = !(typeof (e.dataSeries.visible) === 'undefined' || e.dataSeries.visible);
                        gaugeChart.render();
                    }
                },
                data: [{
                    type: 'doughnut',
                    yValueFormatString: '#,##0.00\"%\"',
                    indexLabel: '{y}',
                    showInLegend: true,
                    legendText: 'Unbooked Cars',
                    dataPoints: [
                        { y: (100 - bookingUtilization), color: '#5cbae6' }, // Representing unbooked cars
                        { y: bookingUtilization, color: '#e6e6e6', legendText: 'Booked Cars' } // Representing booked cars
                    ]
                }]
            });

            gaugeChart.render();
        }


    </script>
    <script>
        console.log("outside");
        function GenerateTapeGraph(feedbackData) {
            var dataPoints = [];
            console.log("inside");
            // Convert feedback data to CanvasJS data points format
            for (var i = 0; i < feedbackData.length; i++) {
                dataPoints.push({ y: feedbackData[i].count, label: "Rating " + feedbackData[i].rating });
            }

            var chart = new CanvasJS.Chart("tapeGraphContainer", {
                animationEnabled: true,
                theme: "light2",
                title: {
                    text: "Feedback Ratings"
                },
                axisY: {
                    title: "Number of Feedbacks"
                },
                axisX: {
                    title: "Rating"
                },
                data: [{
                    type: "column",
                    dataPoints: dataPoints
                }]
            });

            chart.render();
        }
    </script>
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
                    <li id="homeLink" runat="server"><a href="a_home.aspx">Home</a></li>
                    <li><a href="a_customers.aspx">Customers</a></li>
                    <li><a href="a_cars.aspx">Cars</a></li>
                    <li><a href="a_feedback.aspx">Feedback</a></li>
                    <li><a href="a_services.aspx">Reservations</a></li>
                    <li><a href="a_contacts.aspx">ContactHistory</a></li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmLogout();" OnClick="LogOut_Click"><img src="logout.png"/></asp:LinkButton></li>
                </ul>
            </nav>
        </header>


        <!-- -------------------------------------------------------- -->


        <div class="container">
            <div class="welcome">
                <h1>Welcome Admin!</h1>
                <asp:Label ID="Time" runat="server" Text=""></asp:Label>
                <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
            </div>
            <div class="summary-stats">
                <div class="summary-stat">
                    <p>Total Customers:</p>
                    <asp:Label ID="t_users" runat="server" Text=""></asp:Label>
                </div>
                <div class="summary-stat">
                    <p>Total Cars:</p>
                    <asp:Label ID="t_cars" runat="server" Text=""></asp:Label>
                </div>
                <div class="summary-stat">
                    <p>Average Rating:</p>
                    <asp:Label ID="t_rating" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="summary-stats">
                <div class="summary-stat">
                    <p>Total Earning :</p>
                    <asp:Label ID="t_earning" runat="server" Text=""></asp:Label>
                </div>

                <div class="summary-stats">
                    <div class="summary-stat">
                        <p>Last Month Earning :</p>
                        <asp:Label ID="t_mon_earning" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>


            <div class="graph-container">
                <div id="tapeGraphContainer" class="graph"></div>
                <div id="gaugeChartContainer" class="graph"></div>
            </div>
            <!-- Other content for admin panel -->
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
    <!-- Replace the existing script block with this -->
    <script src="script.js"></script>

</body>
</html>
