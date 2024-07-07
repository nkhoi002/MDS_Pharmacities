<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="SearchMedicine.aspx.cs" Inherits="MedicineManagement.SearchMedicine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .zoom {
            transition: transform 0.2s; /* Animation */
        }

        .zoom:hover {
            transform: scale(1.5); /* (150% zoom) */
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#txtMedicineName").autocomplete({
                source: function (request, response) {
                    var param = { searchTerm: $('#txtMedicineName').val() };
                    $.ajax({
                        url: "SearchMedicine.aspx/GetMedicineList",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.MedicineName + '-' + item.CategoryName + ' (' + item.ManufacturerName + ')'
                                    , val: item.MedicineId
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hdnMedicineId]").val(i.item.val);
                    $('#divOrderDetails').attr('style', 'display:block');
                    var param = { medicineId: i.item.val };
                    $.ajax({
                        url: "SearchMedicine.aspx/GetMedicineDetails",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            $('#lblMedicine').text(data.d.MedicineName);
                            $('#lblCompany').text(data.d.ManufacturerName);
                            $('#lblCategory').text(data.d.CategoryName);
                            $('#lblUnitPrice').text(data.d.Amount);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                        }
                    });
                },
                minLength: 2
            });
        });

        $('#txtQuantity').change(function () {
            var quantity = $('#txtQuantity').val();
            var unitPrice = $('#lblUnitPrice').text();
            var totalPrice = parseFloat(quantity) * parseFloat(unitPrice);
            $('#lblTotalPrice').text(totalPrice);
        });

        function MakeOrder(event) {
            if ($('#txtQuantity').val() === undefined || $('#txtQuantity').val() === '' || $('#lblTotalPrice').text() === undefined || $('#lblTotalPrice').text() === '') {
                $('lblErrorMessage').text('Please enter quantity');
                return false;
            } else {
                window.location.replace("PaymentDetails.aspx?Amount=" + $('#lblTotalPrice').text() + "&MedicineId=" + $('#hdnMedicineId').val() + "&Quantity=" + $('#txtQuantity').val() + "&UnitPrice=" + $('#lblUnitPrice').text());
            }
        }

        function AddToCart(event) {
            if ($('#txtQuantity').val() === undefined || $('#txtQuantity').val() === '' || $('#lblTotalPrice').text() === undefined || $('#lblTotalPrice').text() === '') {
                $('lblErrorMessage').text('Please enter quantity');
                return false;
            } else {
                // Implement your Add to Cart logic here
                alert("Item added to cart!");
            }
        }

        MakeOrderByGrid = function (medicineId) {
            $("[id$=hdnMedicineId]").val(medicineId);
            $('#divOrderDetails').attr('style', 'display:block');
            var param = { medicineId: medicineId };
            $.ajax({
                url: "SearchMedicine.aspx/GetMedicineDetails",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $('#ContentPlaceHolder1_divMedicineGrid').attr('style', 'display:none');
                    $('#lblMedicine').text(data.d.MedicineName);
                    $('#lblCompany').text(data.d.ManufacturerName);
                    $('#lblCategory').text(data.d.CategoryName);
                    $('#lblUnitPrice').text(data.d.Amount);
                    $('#imgMedicine').attr('src', 'images/ProductImages/' + data.d.MedicineImagePath);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>&nbsp;</div>
    <div class="panel panel-info">
        <div class="panel-heading">
            <h2>Make Order Form</h2>
        </div>
        <div class="panel-body">
            <div class="example">
                <div class="col-lg-11"><input type="text" placeholder="Search Medicine.." class="form-control pull-right" name="search" id="txtMedicineName" /></div>
                <div class="col-lg-1"><button type="button" class="btn btn-success" ><i class="fa fa-search">Search</i></button></div>
                <asp:HiddenField ID="hdnMedicineId" runat="server" ClientIDMode="Static" />
            </div><br /><br />
            <div id="divMedicineGrid"  runat="server" style="display: none;"> 
                <asp:Literal ID="ltrlMedical" runat="server"></asp:Literal>
            </div>
            <div class="container" id="divOrderDetails" style="display: none">
                <table class="table table-stripped">
                    <tr>
                        <td>Medicine</td>
                        <td>:<asp:Label ID="lblMedicine" runat="server" ClientIDMode="Static" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align:middle">Medicine Image</td>
                        <td>:<img id="imgMedicine" class="zoom" src="images/ProductImages/300px-No_image_available.svg.png" style="width:40%;max-height:100px;min-height:170px;" /></td>
                    </tr>
                    <tr>
                        <td>Company/Brand</td>
                        <td>:<asp:Label ID="lblCompany" runat="server" ClientIDMode="Static" /></td>
                    </tr>
                    <tr>
                        <td>Category</td>
                        <td>:<asp:Label ID="lblCategory" runat="server" ClientIDMode="Static" /></td>
                    </tr>
                    <tr>
                        <td>Price</td>
                        <td>:<asp:Label ID="lblUnitPrice" runat="server" Text="" ClientIDMode="Static" /></td>
                    </tr>
                    <tr>
                        <td>Quantity</td>
                        <td><asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server" TextMode="Number" ClientIDMode="Static" />
                            <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>Total Price</td>
                        <td>:<asp:Label ID="lblTotalPrice" Text="" runat="server" ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <a id="btnSave" onclick="MakeOrder();" class="btn btn-warning">Make Order Online</a>
                            <a id="btnDirectPayment" onclick="DirectPayment();" class="btn btn-success">Direct payment </a>
                            <a id="btnAddToCart" onclick="AddToCart();" class="btn btn-primary">Add to Cart</a>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#txtMedicineName").autocomplete({
                source: function (request, response) {
                    var param = { searchTerm: $('#txtMedicineName').val() };
                    $.ajax({
                        url: "SearchMedicine.aspx/GetMedicineList",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.MedicineName + '-' + item.CategoryName + ' (' + item.ManufacturerName + ')'
                                    , val: item.MedicineId
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hdnMedicineId]").val(i.item.val);
                    $('#divOrderDetails').attr('style', 'display:block');
                    var param = { medicineId: i.item.val };
                    $.ajax({
                        url: "SearchMedicine.aspx/GetMedicineDetails",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            $('#ContentPlaceHolder1_divMedicineGrid').attr('style', 'display:none');
                            $('#lblMedicine').text(data.d.MedicineName);
                            $('#lblCompany').text(data.d.ManufacturerName);
                            $('#lblCategory').text(data.d.CategoryName);
                            $('#lblUnitPrice').text(data.d.Amount);
                            $('#imgMedicine').attr('src', 'images/ProductImages/' + data.d.MedicineImagePath);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                        }
                    });
                },
                minLength: 2
            });
        });

        $('#txtQuantity').change(function () {
            var quantity = $('#txtQuantity').val();
            var unitPrice = $('#lblUnitPrice').text();
            var totalPrice = parseFloat(quantity) * parseFloat(unitPrice);
            $('#lblTotalPrice').text(totalPrice);
        })

        function MakeOrder(event) {
            if ($('#txtQuantity').val() === undefined || $('#txtQuantity').val() === '' || $('#lblTotalPrice').text() === undefined || $('#lblTotalPrice').text() === '') {
                $('lblErrorMessage').text('Please enter quantity');
                return false;
            } else {
                window.location.replace("PaymentDetails.aspx?Amount=" + $('#lblTotalPrice').text() + "&MedicineId=" + $('#hdnMedicineId').val() + "&Quantity=" + $('#txtQuantity').val() + "&UnitPrice=" + $('#lblUnitPrice').text());
            }
        }

        function AddToCart(event) {
            if ($('#txtQuantity').val() === undefined || $('#txtQuantity').val() === '' || $('#lblTotalPrice').text() === undefined || $('#lblTotalPrice').text() === '') {
                $('lblErrorMessage').text('Please enter quantity');
                return false;
            } else {
                // Implement your Add to Cart logic here
                alert("Item added to cart!");
            }
        }

        MakeOrderByGrid = function (medicineId) {
            $("[id$=hdnMedicineId]").val(medicineId);
            $('#divOrderDetails').attr('style', 'display:block');
            var param = { medicineId: medicineId };
            $.ajax({
                url: "SearchMedicine.aspx/GetMedicineDetails",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $('#ContentPlaceHolder1_divMedicineGrid').attr('style', 'display:none');
                    $('#lblMedicine').text(data.d.MedicineName);
                    $('#lblCompany').text(data.d.ManufacturerName);
                    $('#lblCategory').text(data.d.CategoryName);
                    $('#lblUnitPrice').text(data.d.Amount);
                    $('#imgMedicine').attr('src', 'images/ProductImages/' + data.d.MedicineImagePath);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var err = eval("(" + XMLHttpRequest.responseText + ")");
                    alert(err.Message)
                }
            });
        }
    </script>
</asp:Content>