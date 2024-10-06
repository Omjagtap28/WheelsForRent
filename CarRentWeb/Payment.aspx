<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="CarRentWeb.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/Payment.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Secure Payment Gateway</h2>
            <div class="secure-payment-notice">
                Your payment is secure with us. We use encryption to protect your personal information.
            </div>
            <div class="payment-card-logos">
                <img src="images/Logos/visa.png" alt="Visa" title="Visa" />
                <img src="images/Logos/mastercard.png" alt="Mastercard" title="Mastercard" />
                <img src="images/Logos/americanexpress.png" alt="American Express" title="American Express" />
                <img src="images/Logos/upi.png" alt="UPI" title="UPI" />
            </div>



            <div class="total-amount">
                Total Amount:
                <asp:Label ID="lblTotalAmount" runat="server" Text="Your Total Amount Here"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Payment_failed" runat="server" Text=""></asp:Label>
            </div>

            <div class="payment-options">
                <button type="button" onclick="showPaymentForm('debitCard')">Debit Card</button><br />
                <button type="button" onclick="showPaymentForm('creditCard')">Credit Card</button><br />
                <button type="button" onclick="showPaymentForm('upi')">UPI</button><br />
                <button type="button" onclick="showPaymentForm('cashOnDelivery')">Cash on Delivery</button><br />
            </div>


            <!-- Debit Card Payment Form -->
            <div id="debitCardForm" class="payment-form" runat="server">
                <!-- Debit Card payment form fields -->
                <div>
                    <label for="txtDebitCardNumber">Debit Card Number:</label>
                    <asp:TextBox ID="txtDebitCardNumber" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <label for="ddlDebitExpiryMonth">Expiry Date:</label>
                    <asp:DropDownList ID="ddlDebitExpiryMonth" runat="server" CssClass="card-input">
                        <asp:ListItem Text="Month" Value="" />
                        <asp:ListItem Text="01" Value="01" />
                        <asp:ListItem Text="02" Value="02" />
                        <asp:ListItem Text="03" Value="03" />
                        <asp:ListItem Text="04" Value="04" />
                        <asp:ListItem Text="05" Value="05" />
                        <asp:ListItem Text="06" Value="06" />
                        <asp:ListItem Text="07" Value="07" />
                        <asp:ListItem Text="08" Value="08" />
                        <asp:ListItem Text="09" Value="09" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="11" Value="11" />
                        <asp:ListItem Text="12" Value="12" />

                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlDebitExpiryYear" runat="server" CssClass="card-input">
                        <asp:ListItem Text="Year" Value="" />
                        <asp:ListItem Text="2024" Value="2024" />
                        <asp:ListItem Text="2025" Value="2025" />

                    </asp:DropDownList>
                </div>

                <div>
                    <label for="txtDebitCvv">CVV / CVC:</label>
                    <asp:TextBox ID="txtDebitCvv" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <label for="txtDebitCardholderName">Cardholder's Name:</label>
                    <asp:TextBox ID="txtDebitCardholderName" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <asp:Label ID="DebitErrorMessage" runat="server" Text=""></asp:Label>
                </div>

                <div>
                    <asp:Button ID="btnPayNowDebitCard" runat="server" Text="Pay Now" OnClick="btnPayNowDebitCard_Click" />
                </div>
            </div>



            <!-- Credit Card Payment Form -->
            <div id="creditCardForm" class="payment-form" runat="server">
                <div>
                    <label for="txtCardNumber">Credit Card Number:</label>
                    <asp:TextBox ID="txtCardNumber" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <label for="ddlExpiryMonth">Expiry Date:</label>
                    <asp:DropDownList ID="ddlExpiryMonth" runat="server" CssClass="card-input">

                        <asp:ListItem Text="Month" Value="" />
                        <asp:ListItem Text="01" Value="01" />
                        <asp:ListItem Text="02" Value="02" />
                        <asp:ListItem Text="03" Value="03" />
                        <asp:ListItem Text="04" Value="04" />
                        <asp:ListItem Text="05" Value="05" />
                        <asp:ListItem Text="06" Value="06" />
                        <asp:ListItem Text="07" Value="07" />
                        <asp:ListItem Text="08" Value="08" />
                        <asp:ListItem Text="09" Value="09" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="11" Value="11" />
                        <asp:ListItem Text="12" Value="12" />

                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlExpiryYear" runat="server" CssClass="card-input">

                        <asp:ListItem Text="Year" Value="" />
                        <asp:ListItem Text="2024" Value="2024" />
                        <asp:ListItem Text="2025" Value="2025" />

                    </asp:DropDownList>
                </div>

                <div>
                    <label for="txtCvv">CVV / CVC:</label>
                    <asp:TextBox ID="txtCvv" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <label for="txtCardholderName">Cardholder's Name:</label>
                    <asp:TextBox ID="txtCardholderName" runat="server" CssClass="card-input" />
                </div>

                <div>
                    <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
                </div>

                <div>
                    <asp:Button ID="Button1" runat="server" Text="Pay Now" OnClick="btnPayNowCreditCard_Click" />
                </div>
            </div>

            <!-- UPI Payment Form -->
            <div id="upiForm" class="payment-form" runat="server">
                <!-- UPI payment form fields -->
                <div style="margin-bottom: 10px;">
                    <label for="txtUpiId">UPI ID:</label>
                    <asp:TextBox ID="txtUpiId" runat="server" CssClass="upi-input" />
                </div>
                <asp:Label ID="UPIErrorMessage" runat="server" Text=""></asp:Label>
                <div>
                    <asp:Button ID="btnPayNowUPI" runat="server" Text="Pay Now" OnClick="btnPayNowUPI_Click" />
                </div>
            </div>

            <!-- Cash on Delivery Payment Form -->
            <div id="cashOnDeliveryForm" class="payment-form" runat="server">
                <!-- Cash on Delivery payment information -->
                <p>
                    Thank you for choosing Cash on Delivery. Your payment will be collected in cash during the delivery of the rented vehicle.
                </p>
                <p>
                    Please have the exact amount ready for a smooth transaction.
                </p>
                <p>
                    If you have any questions, feel free to contact our customer support.
                </p>
                <div>
                    <asp:Button ID="Book" runat="server" Text="Book" OnClick="btnBook_Click" />
                </div>

            </div>
        </div>
        <script src="js/Payment.js">
        </script>
    </form>
</body>
</html>
