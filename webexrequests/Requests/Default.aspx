<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="/css/custom.css" rel="stylesheet" />--%>
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />
    <style>
        ul.ui-autocomplete {
            width: 30px;
            background-color: white;
            max-height: 300px;
            overflow-y: auto;
            overflow-x: hidden;
            z-index: 500 !important;
            position: relative;
        }

        .footer-auto {
            background: gray;
            color: white;
        }

        .modal-content {
            top: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">

        <div id="displayContent" style="text-align: center; font-size: large;">
            <asp:Label ID="lblAuthorizedUser" Visible="false" runat="server"></asp:Label>
        </div>
        <div style="text-align: center; font-size: large;">
            <asp:Label ID="lblSuccess" Visible="false" runat="server"></asp:Label>
        </div>
        <div class="col-md-10">
            <div class="form-horizontal" id="RequestForm" runat="server">
                <div class="page-header" style="margin-top: 0px; margin-bottom: 10px;">
                    <h3 class="">Submit a WebEx Request</h3>
                </div>


                <div class="form-group">
                    <asp:Label ID="lblFirstName" runat="server" CssClass="control-label col-md-4" Text="First Name" AssociatedControlID="txtFirstName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblLastName" runat="server" CssClass="control-label col-md-4" Text="Last Name" AssociatedControlID="txtLastName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblUserName" runat="server" CssClass="control-label col-md-4" Text="User Name (SSO)" AssociatedControlID="txtUserName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblFiscalOfficer" runat="server" CssClass="control-label col-md-4" AssociatedControlID="txtFiscalOfficer">Fiscal Officer
                    </asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFiscalOfficer" runat="server" CssClass="form-control" Placeholder="Enter fiscal officer's last name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblMoCode" runat="server" CssClass="control-label col-md-4" Text="MoCode" AssociatedControlID="txtMoCode"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtMoCode" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblComments" runat="server" CssClass="control-label col-md-4" Text="Comments" AssociatedControlID="txtComments"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-5 col-md-7">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="col-md-3 btn btn-primary" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>

        <script>
            $(document).ready(function () {
                $("#txtFiscalOfficer").autocomplete({
                    minlength: 2,
                    position: {
                        my: "right top",
                        at: "right bottom",
                        collision: "none"
                    },
                    autofocus: true,
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Default.aspx/GetCompletionList",
                            data: "{'name':'" + $("#txtFiscalOfficer").val() + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                            }
                        });
                        $('.ui-helper-hidden-accessible').remove();
                    }
                });
            });            

            $('#form1').validate({
                ignore: ":hidden",
                rules: {
                    '<%=txtFirstName.UniqueID %>': {
                    required: true,
                    maxlength: 20
                },
                '<%=txtLastName.UniqueID %>': {
                    required: true,
                    maxlength: 20
                },
                '<%=txtUserName.UniqueID %>': {
                    required: true
                },
                '<%=txtFiscalOfficer.UniqueID %>': {
                    required: true
                },
                '<%=txtMoCode.UniqueID %>': {
                    required: true,
                    maxlength: 20
                }
            },
            messages: {
                '<%=txtFirstName.UniqueID %>': { required: "Please enter First Name" },
                '<%=txtLastName.UniqueID %>': { required: "Please enter Last Name" },
                '<%=txtUserName.UniqueID %>': { required: "Please enter User Name" },
                '<%=txtFiscalOfficer.UniqueID %>': { required: "Please enter Fiscal Officer Name" },
                '<%=txtMoCode.UniqueID %>': { required: "Please enter MoCode" }
            },

            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
                } else {
                    error.insertAfter(element);
                }
            }
        });
       
            function showAlert(description) {

                $("#myModal").modal('show');
                $(".modal-body #bookId").html(description);
                $(".modal-body #bookId").css("word-wrap", "break-word");
            }

            function FiscalOfficerAlert(description) {

                $("#myModal").modal('show');
                $(".modal-body #bookId").html(description);
                $(".modal-body #bookId").css("word-wrap", "break-word");
                $(".modal-title").html('Error in Fiscal Officer field');
            }

        </script>


        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #1c84c6">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="color: white">Request Submitted </h4>
                    </div>
                    <div class="modal-body">
                        <p id="bookId"></p>
                    </div>
                    <div class="modal-footer">
                        <a class="btn btn-default btn-xs pull-right" href="Default.aspx">Close</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

