<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PaymentDetails.aspx.cs" Inherits="BikesWebApp.PaymentDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Details</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous" />
    <style>
        body {
            background-image: url('https://treobangron.com.vn/wp-content/uploads/2022/09/background-phong-lam-viec-34.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .center-table {
            height: 100%;
            width: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .container {
            width: 80%; /* Đặt chiều rộng của container chứa table */
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            background: rgba(255, 255, 255, 0.9); /* Optional: Add a slight white background with transparency */
        }
        .form {
            width: 100%;
        }
        .form td {
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="center-table">
            <div class="container">
                <h2>Payment Details</h2>
                <table class="form">
                    <tr>
                        <td>Payment Type</td>
                        <td>
                            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select Payment Type--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Credit Card" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Debit Card" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvPaymentType" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="ddlPaymentType" ErrorMessage="Please select payment type" />
                        </td>
                    </tr>
                    <tr>
                        <td>Card Number</td>
                        <td>
                            <asp:TextBox ID="txtCardNumber" MaxLength="16" runat="server" CssClass="form-control" placeholder="16 Digits Card Number" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCardNumber" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtCardNumber" ErrorMessage="Please enter 16 digit card number" />
                        </td>
                    </tr>
                    <tr>
                        <td>CVV</td>
                        <td>
                            <asp:TextBox ID="txtCVV" MaxLength="3" runat="server" CssClass="form-control" placeholder="Enter CVV Number (CVV)" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCVV" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtCVV" ErrorMessage="Please enter 3 digit CVV number printed on card" />
                        </td>
                    </tr>
                    <tr>
                        <td>Expiry Month Year</td>
                        <td>
                            <asp:TextBox ID="txtExpiryMonth" MaxLength="7" runat="server" CssClass="form-control" placeholder="Enter Expiry Month Year (MM-YYYY)" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvExpiryMonth" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtExpiryMonth" ErrorMessage="Please enter expiry month and year printed on card" />
                        </td>
                    </tr>
                    <tr>
                        <td>Name On Card</td>
                        <td>
                            <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="form-control" placeholder="Name On Card" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvNameOnCard" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtNameOnCard" ErrorMessage="Please enter your name printed on card" />
                        </td>
                    </tr>
                    <tr>
                        <td>Transaction Amount</td>
                        <td>
                            <asp:TextBox ID="txtAmount" ReadOnly="true" runat="server" CssClass="form-control" placeholder="Amount Rs." />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rfvAmount" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtAmount" ErrorMessage="Please enter your transaction amount" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnPayment" runat="server" Text="Make Payment" CssClass="btn btn-primary btn-block" OnClick="btnPayment_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <button type="button" class="btn btn-primary" onclick="window.location.href='SearchMedicine.aspx';">Back To Search Medicine</button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
