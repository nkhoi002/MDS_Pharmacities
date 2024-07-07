<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="MedicineManagement.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <h2>All Registered User List</h2><hr />
    <div class="Grid">
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            BorderStyle="None"
            CssClass="Grid"
            EditRowStyle-ForeColor="#0066FF"
            Width="100%"
            BackColor="White"
            BorderColor="#CCCCCC"
            BorderWidth="1px"
            CellPadding="3"
            Height="20px"
            AllowPaging="True"
            PageSize="5"
            OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="UserId"
                    HeaderText="ID"
                    InsertVisible="False" ReadOnly="True"
                    SortExpression="" />
                <asp:BoundField DataField="FullName"
                    HeaderText="Name"
                    SortExpression="FullName" />
                <asp:BoundField DataField="UserName"
                    HeaderText="UserName"
                    SortExpression="UserName" />
                <asp:BoundField DataField="Email"
                    HeaderText="Email-Address"
                    SortExpression="" />
                <asp:BoundField DataField="Password"
                    HeaderText="Password"
                    SortExpression="Password" />
                <asp:BoundField DataField="ContactNumber"
                    HeaderText="Contact Number"
                    SortExpression="ContactNumber" />
                <asp:BoundField DataField="Address"
                    HeaderText="Address"
                    SortExpression="Address" />
            </Columns>

            <EditRowStyle ForeColor="#0066FF" />

            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

        </asp:GridView>
    </div>
</asp:Content>
