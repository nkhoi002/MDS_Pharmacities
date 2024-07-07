<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="InventoryManagmentSystem.UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3>User Registration Form</h3>
            </div>
            <div class="panel-body">
                <table class="table table-stripped">
                    <tr>
                        <td>Full Name</td>
                        <td>
                            <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <asp:HiddenField ID="hdnUserId" runat="server" />
                        <td>
                            <asp:RequiredFieldValidator ID="rfvFullName" runat="server"
                                ErrorMessage="UserName is Required" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtFullName">

                            </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>User Name</td>
                        <td>
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RfvUserName" runat="server"
                                ErrorMessage="UserName is Required" ForeColor="Red"
                                ControlToValidate="txtUserName">

                            </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Email Address&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RfvEmail" runat="server" Display="Dynamic"
                                ErrorMessage="Email Address is Required" ForeColor="Red"
                                ControlToValidate="txtEmail">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="validateEmail"
                                runat="server" ErrorMessage="Invalid email." Display="Dynamic"
                                ControlToValidate="txtEmail" ForeColor="Red"
                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" /></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td>
                            <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RfvPassword" runat="server"
                                ErrorMessage="Password is Required" ForeColor="Red"
                                ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Confirm Password</td>
                        <td>
                            <asp:TextBox ID="txtConfirmpassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:CompareValidator ID="cmprConfirmPass" runat="server" Display="Dynamic"
                                ErrorMessage="Do not match password" ForeColor="Red" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmpassword"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RfvConfirmPassword" runat="server"
                                ErrorMessage="Confrim your password" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtConfirmpassword"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Contact Number</td>
                        <td>
                            <asp:TextBox ID="txtContact" CssClass="form-control" runat="server" ></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="Contact number is required" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtContact"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Address</td>
                        <td>
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="Address is mandetory" ForeColor="Red" Display="Dynamic"
                                ControlToValidate="txtAddress"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
                <div class="btns">
                    <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="BtnSave_Click" />
                </div>
               
            </div>
        </div>


    </div>
</asp:Content>

