<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="otp.aspx.cs" Inherits="CarRentWeb.otp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter OTP</title>
    <link rel="stylesheet" href="css/otp.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="timer"></div>
            <asp:Label ID="txtemail" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:Label ID="txtcarid" runat="server" Text="Label" Visible="false"></asp:Label>

            <asp:Label ID="message2" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="message" runat="server" Text="Enter 6 digit OTP"></asp:Label>

            <asp:TextBox ID="txtOTP" runat="server" MaxLength="6"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:Label ID="confirmationLabel" runat="server" Text="" CssClass="confirmation-message"></asp:Label>

            <asp:Label ID="lblReservationID" runat="server" Text="" Visible="false" ></asp:Label>
            <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblPaymentStatus" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Other" runat="server" Text="" Visible="false"></asp:Label>

        </div>
        <script src="js/otp.js" ></script>
    </form>
</body>
</html>
