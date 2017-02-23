<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="ExportData.aspx.cs" Inherits="Requests_ExportData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script>

        $(function () {
            $("#txtStartDate").datepicker({
                numberOfMonths: 1,
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate() + 1);
                    $("#txtEndDate").datepicker("option", "minDate", dt);
                }
            });

            $("#txtEndDate").datepicker({

                numberOfMonths: 1,
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate() - 1);
                    $("#txtStartDate").datepicker("option", "maxDate", dt);
                }
            });
        });
        function NoDataAlert() {
            bootbox.alert({
                title: " Alert !!!",
                message: "There is no data in the selected date range",
                animate: true,
                style: "Width:500px; margin-top:100px;",
                callback: function () {
                    window.location.href = "ExportData.aspx";
                }
            });
        }

    </script>
    <style>
        .modal-content {
            top: 150px;
        }
    </style>

    <form id="form1" runat="server">

        <div class="col-md-10">
            <div class="form-horizontal" id="RequestForm" runat="server">
                <div class="page-header" style="margin-top: 0px; margin-bottom: 10px;">
                    <h3 class="">Export Data</h3>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblFirstName" runat="server" CssClass="control-label col-md-4" Text="Start Date" AssociatedControlID="txtStartDate"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtStartDate" Style="color: red;" ErrorMessage="Please select start date" />

                </div>

                <div class="form-group">
                    <asp:Label ID="lblLastName" runat="server" CssClass="control-label col-md-4" Text="End Date" AssociatedControlID="txtEndDate"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEndDate" Style="color: red;" ErrorMessage="Please select end date" />

                </div>


                <div class="form-group">
                    <div class="col-md-offset-5 col-md-7">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="col-md-3 btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

