<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="BikesWebApp.OrderList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h2>Order List</h2><hr />
    <asp:GridView ID="gvOrderList" Width="100%" HorizontalAlign="Center" runat="server" DataKeyNames="OrderId" AllowPaging="true" EmptyDataText="No record found"
        AutoGenerateColumns="false" OnPageIndexChanging="gvOrderList_PageIndexChanging" CssClass="table table-stripped" >
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="OrderId" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OrderDate" HeaderText="Order Date " ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FullName" HeaderText="Customer Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Medicine" HeaderText="Medicine" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Price" HeaderText="Rate" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</asp:Content>
