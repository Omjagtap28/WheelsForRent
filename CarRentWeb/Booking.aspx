<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="CarRentWeb.Booking" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking</title>
    <link rel="stylesheet" href="css/Booking.css" />
</head>
<body>
    <div class="container">
        <form id="bookingForm" runat="server">
            
            <h2>Customer Information</h2>
            <div>
                <label for="txtFirstName">First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtLastName">Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtEmail">Email Address:</label>
                <asp:TextBox ID="txtEmail" runat="server" type="email" ReadOnly="true"></asp:TextBox>
            </div>
            <div>
                <label for="txtPhone">Phone Number:</label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="Address">Address:</label>
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
            <p>Note: You can update your customer details later through your <a href="profile.aspx">profile page</a>.</p>
            <h2>Rental Details</h2>
            <div class="time-slots">
                <asp:Label ID="availableTimeSlots" runat="server" Text="The car is booked for this dates"></asp:Label>
                <div id="divBookingHistory" runat="server" class="time-slot-list">
                    <!-- Available time slots will be dynamically added here -->
                    <asp:Label ID="AvailableSlots" runat="server"></asp:Label>
                </div>
            </div>
            <p>Please choose a pickup time within the available time slots.</p>
            <p>Note: Our business hours are from 5:00 AM to 12:00 AM. You can only book a car during this period.</p>
            <div>
                <label for="txtPickupDateTime">Pickup Date and Time:</label>
                <asp:TextBox ID="txtPickupDateTime" runat="server" type="datetime-local"></asp:TextBox>
            </div>
            
            <div>
                <label for="txtReturnDateTime">Return Date and Time:</label>
                <asp:TextBox ID="txtReturnDateTime" runat="server" type="datetime-local"></asp:TextBox>
            </div>
            <div>
                <label for="txtPickupLocation">Pickup Location:</label>
                <asp:TextBox ID="txtPickupLocation" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="chkSameReturnLocation">Return Location is same as Pickup Location:</label>
                <asp:CheckBox ID="chkSameReturnLocation" runat="server" onclick="handleCheckboxClick()" />
            </div>
            <div>
                <label for="txtReturnLocation">Return Location:</label>
                <asp:TextBox ID="txtReturnLocation" runat="server"></asp:TextBox>
            </div>
            <h2>Additional Information</h2>
            <div>
                <label for="txtLicenseNumber">Driver's License Number:</label>
                <asp:TextBox ID="txtLicenseNumber" runat="server"></asp:TextBox>
            </div>

            <asp:Label ID="ErrorMessage" runat="server" CssClass="error-message" Text=""></asp:Label>
            <div>
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn-submit" Text="Submit Booking" OnClick="btnSubmit_Click" />
            </div>
        </form>
    </div>
    <script>
        function handleCheckboxClick() {
            var chkSameReturnLocation = document.getElementById('<%= chkSameReturnLocation.ClientID %>');
            var txtPickupLocation = document.getElementById('<%= txtPickupLocation.ClientID %>');
            var txtReturnLocation = document.getElementById('<%= txtReturnLocation.ClientID %>');
            if (chkSameReturnLocation.checked) {
                // If checkbox is checked, set Return Location to Pickup Location
                txtReturnLocation.value = txtPickupLocation.value;
                txtReturnLocation.readOnly = true;
            } else {
                // If checkbox is unchecked, clear Return Location
                txtReturnLocation.value = '';
                txtReturnLocation.readOnly = false;
            }
        }
    </script>
</body>
</html>
