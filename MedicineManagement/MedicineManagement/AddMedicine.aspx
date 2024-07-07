<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddMedicine.aspx.cs" Inherits="MedicineManagement.AddMedicine" %>

<asp:content id="Content1" contentplaceholderid="Content" runat="server">
    <script>
        $(document).ready(function() {
            $.validator.addMethod("match", function(value, element)   
            {  
                return this.optional(element) || /^[0-9]{5}$/i.test(value);  
            }, "Please enter 5 digit invoice number."); 

            $.validator.addMethod("CheckDropDownList", function (value, element) {  
                if (value == '0')  
                    return false;  
                else  
                    return true; 
        
            },
            "Please select from dropdown list .");
            $("#form1").validate({
                rules: {
                           
                    <%=txtMedicineName.UniqueID %>:
               {
                   required:true 
               },
                    <%= txtPrice.UniqueID %>:
                {
                                
                    required: true,
                    digits: true
                },
                    <%= ddlBrand.UniqueID %>:
                {
                                
                    CheckDropDownList: true
                    
                }
                },
                messages: 
                        {
                            <%= txtMedicineName.UniqueID %>: 
                      {
                          required:'Medicine Name is Required.'
                      },
                            <%= txtPrice.UniqueID %>: 
                      {
                          required:'Price is Required.'
                      },
                    
                            <%= ddlBrand.UniqueID %>:
                    {
                                
                        CheckDropDownList:'Please select Manufacturer Name'
                  
                    }
                        }
            });
        });

        function CalculatePrice(){
            var quantity=document.getElementById('txtMinQty').value;
            var unitPrice=document.getElementById('txtUnitCost').value;
            var totalCost=parseInt(quantity)*parseFloat(unitPrice);
            document.getElementById('txtTotalCost').value=parseFloat(totalCost);
        }
</script>
    <style>
        label.error {
            color: red;
        }
    </style>
    <div class="container">
        <h2>Medicine Master Registration From</h2>
        <table class="form">
            <tr>
                <td>Medicine Name</td>
                <td>
                    <asp:TextBox ID="txtMedicineName" CssClass="textboxs" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hdnMedicineId" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtMedicineName" Display="Dynamic" ForeColor="Red" ErrorMessage="Please enter medicine name" />
                </td>
            </tr>
             <tr>
                <td>Medicine Description</td>
                <td>
                    <asp:TextBox ID="txtMedicineDescription" TextMode="MultiLine" Rows="5" CssClass="textareas" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtMedicineDescription"
                         Display="Dynamic" ForeColor="Red" ErrorMessage="Please enter Medicine description" />
                </td>
            </tr>
            <tr>
                <td>Medicine Category</td>
                <td>
                    <asp:DropDownList ID="ddlCategory" CssClass="textboxs" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" Display="Dynamic" 
                        ForeColor="Red" ErrorMessage="Please select Medicine category" />
                </td>
            </tr>
            <tr>
                <td>Medicine Brand/Company</td>
                <td>
                    <asp:DropDownList ID="ddlBrand" CssClass="textboxs" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvBrand" runat="server" ControlToValidate="ddlBrand" Display="Dynamic" 
                        ForeColor="Red" ErrorMessage="Please select Medicine brand" />
                </td>
            </tr>
            <tr>
                <td>Price</td>
                <td>
                    <asp:TextBox ID="txtPrice" ClientIDMode="Static" TextMode="Number" CssClass="textboxs" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMinQuantity" runat="server" ControlToValidate="txtPrice" Display="Dynamic" ForeColor="Red" ErrorMessage="Please enter price" />
                </td>
            </tr>
            <tr>
                <td>Product Image</td>
                <td>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                    <asp:HiddenField ID="hdnFileUpload" runat="server" />
                </td>
            </tr>
        </table>
        <div class="btns">
            <asp:Button ID="BtnUpdate" CssClass="btn-warning" runat="server" Text="Update" OnClick="BtnUpdate_Click" />
            <asp:Button ID="BtnSave" CssClass="btn-success" runat="server" Text="Save" OnClick="BtnSave_Click" />
            <asp:HiddenField ID="hdnMedicine" runat="server"/>
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
                    <asp:BoundField DataField="MedicineName" HeaderText="Name" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Amount" HeaderText="Price" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="ManufacturerName" HeaderText="Brand" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                    <asp:TemplateField HeaderText="Image" >
                        <ItemTemplate>
                            <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# "../images/ProductImages/"+ Eval("MedicineImagePath") %>' Height="70px" Width="100px" />
                            <asp:HiddenField ID="hdnProductImage" runat="server" Value='<%# Eval("MedicineImagePath") %>' />
                            <asp:HiddenField ID="hdnManufacturerId" runat="server" Value='<%# Eval("ManufacturerId") %>' />
                            <asp:HiddenField ID="hdnMedicineId" runat="server" Value='<%# Eval("MedicineId") %>' />
                            <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%# Eval("CategoryId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
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
</asp:content>
