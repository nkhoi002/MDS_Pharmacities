<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoryManagement.aspx.cs" Inherits="MedicineManagement.CategoryManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <h2>Category Registration Form</h2>
        <table class="form">
            
            <tr>
                <td>Category Name</td>
                <td>
                    <asp:TextBox ID="txtCategoryName" CssClass="textboxs" runat="server" ></asp:TextBox></td>
                <asp:HiddenField ID="hdnCategoryId" runat="server" />
                <td>
                    <asp:RequiredFieldValidator ID="RfvSupplierName" runat="server"
                        ErrorMessage="Category Name is Required" ForeColor="Red"
                        ControlToValidate="txtCategoryName"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Description</td>
                <td>
                    <asp:TextBox ID="txtDescription" CssClass="textboxs" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RfvAddress" runat="server"
                        ErrorMessage="Descreption is Required" ForeColor="Red"
                        ControlToValidate="txtDescription"></asp:RequiredFieldValidator></td>
            </tr>
        </table>
        <div class="btns">
             <asp:Button ID="BtnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" OnClick="BtnDelete_Click" />
            <asp:Button ID="BtnUpdate" CssClass="btn btn-warning" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="BtnSave_Click" />
        </div>
        <div class="Grid">
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                AutoGenerateSelectButton="True"
                BorderStyle="None"
                CssClass="Grid"
                EditRowStyle-ForeColor="#0066FF"
                Width="650px"
                BackColor="White"
                BorderColor="#CCCCCC"
                BorderWidth="1px"
                CellPadding="3"
                Height="20px"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                AllowPaging="True"
                PageSize="5"
                OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="CategoryId"
                        HeaderText="Category ID"
                        InsertVisible="False" ReadOnly="True"
                        SortExpression="CategoryId" />
                    <asp:BoundField DataField="CategoryName"
                        HeaderText="Category Name"
                        SortExpression="CategoryName" />
                    <asp:BoundField DataField="Description"
                        HeaderText="Description"
                        SortExpression="Description" />
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
    </div>
</asp:Content>
