<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PaymentReports.aspx.cs" Inherits="BikesWebApp.PaymentReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h2>Payment List</h2><hr />
    <asp:GridView ID="gvPaymentReport" Width="100%" HorizontalAlign="Center" runat="server" DataKeyNames="OrderId" AllowPaging="true" EmptyDataText="No record found"
        AutoGenerateColumns="false" OnPageIndexChanging="gvPayment_PageIndexChanging" CssClass="table table-stripped" >
        <Columns>
            <asp:BoundField DataField="PaymentId" HeaderText="Id" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NameOnCard" HeaderText="NameOnCard" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OrderNumber" HeaderText="Order Number" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PaymentType" HeaderText="Payment Type" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreditCardNo" HeaderText="Card" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date " ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
