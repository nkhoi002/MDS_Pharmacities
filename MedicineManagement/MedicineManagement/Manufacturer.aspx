<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Manufacturer.aspx.cs" Inherits="InvontryManagmentSystem.Manufacturer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <div class="container">
        <h1>Supplier Registration Form</h1>
        <table class="form">
            
            <tr>
                <td>Manufacturer Name</td>
                <td>
                    <asp:TextBox ID="txtManufacturerName" CssClass="textboxs" runat="server" ></asp:TextBox></td>
                <asp:HiddenField ID="hdnManufacturerId" runat="server" />
                <td>
                    <asp:RequiredFieldValidator ID="RfvSupplierName" runat="server"
                        ErrorMessage="Manufacturer Name is Required" ForeColor="Red"
                        ControlToValidate="txtManufacturerName"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Address</td>
                <td>
                    <asp:TextBox ID="txtAddress" CssClass="textboxs" runat="server"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RfvAddress" runat="server"
                        ErrorMessage="Address is Required" ForeColor="Red"
                        ControlToValidate="txtAddress"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>PhoneNumber</td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="textboxs" runat="server" ></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVPhoneNumber" runat="server"
                        ErrorMessage="Phone Number is Required" ForeColor="Red"
                        ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REVPhoneNumber"
                       runat="server" ErrorMessage="Invalid Phone Number."
                     ControlToValidate ="txtPhoneNumber"
                     ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$" />
                </td>
            </tr>
            <tr>
                <td>Email Address</td>
                <td>
                    <asp:TextBox ID="txtEmail" CssClass="textboxs" runat="server" TextMode="Email"></asp:TextBox></td>
                <td>
                   <asp:RegularExpressionValidator ID="validateEmail" ForeColor="Red"
                       runat="server" ErrorMessage="Invalid Email Address."
                     ControlToValidate ="txtEmail"
                     ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" />
                    <asp:RequiredFieldValidator ID="RfvEmail" runat="server"
                        ErrorMessage="Email Address is Required" ForeColor="Red"
                        ControlToValidate="txtEmail">
                    </asp:RequiredFieldValidator>
                   </td>
            </tr>
        </table>
        <div class="btns">
            <asp:Button ID="BtnSave" CssClass="btn" runat="server" Text="Save" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" CssClass="btn" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnDelete" CssClass="btn" runat="server" Text="Delete" OnClick="BtnDelete_Click" />
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
                    <asp:BoundField DataField="ManufacturerID"
                        HeaderText="Manufacturer ID"
                        InsertVisible="False" ReadOnly="True"
                        SortExpression="ManufacturerID" />
                    <asp:BoundField DataField="ManufacturerName"
                        HeaderText="Manufacturer Name"
                        SortExpression="ManufacturerName" />
                    <asp:BoundField DataField="Address"
                        HeaderText="Address"
                        SortExpression="Address" />
                    <asp:BoundField DataField="PhoneNumber"
                        HeaderText="Phone Number"
                        SortExpression="PhoneNumber" />
                    <asp:BoundField DataField="Email"
                        HeaderText="Email Address"
                        SortExpression="" />
                    
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
