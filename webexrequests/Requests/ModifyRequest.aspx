<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyRequest.aspx.cs" Inherits="ModifyRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/custom.css" rel="stylesheet" />
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
     <%--<link href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />--%>
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
    <%-- <script type="text/javascript">
         $(document).ready(function () {
             $("#myTable").DataTable({

                 responsive: true,
                 "bFilter": true,
                 "columnDefs": [
                     { "orderable": true, "targets": [2, 3] }
                 ], "columns": [
                     { "data": "First Name" },
                     { "data": "Last Name" },
                     { "data": "SSO" },
                     { "data": "MoCode" },
                     { "data": "Comments" },
                     { "data": "Actions" }
                 ]
             });
         });
       </script>--%>

    <div id="displayContent" style="text-align: center; font-size: large;">
        <asp:Label ID="lblStatus" Visible="false" runat="server"></asp:Label>
    </div>

    <div class="col-md-10">

        <form id="modifyRequest" runat="server">
            <div class="form-horizontal" id="RequestForm" runat="server">
                <div class="form-group">
                    <asp:Label ID="lblFirstName" runat="server" CssClass="control-label col-md-4" Text="First Name" AssociatedControlID="txtFirstName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblLastName" runat="server" CssClass="control-label col-md-4" Text="Last Name" AssociatedControlID="txtLastName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblUserName" runat="server" CssClass="control-label col-md-4" Text="User Name (SSO)" AssociatedControlID="txtUserName"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblFiscalOfficer" runat="server" CssClass="control-label col-md-4" Text="Fiscal Officer" AssociatedControlID="txtFiscalOfficer"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFiscalOfficer" runat="server" CssClass="form-control" Placeholder="Enter fiscal officer's last name" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblMoCode" runat="server" CssClass="control-label col-md-4" Text="MoCode" AssociatedControlID="txtMoCode"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtMoCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblComments" runat="server" CssClass="control-label col-md-4" Text="Comments" AssociatedControlID="txtComments"></asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="col-md-offset-3 col-md-2 btn btn-primary" OnClick="btnModify_Click" />
                    <asp:Button ID="btnModifyandApprove" runat="server" Text="Modify and Approve" CssClass="col-md-offset-1 col-md-3 btn btn-primary" OnClick="btnModifyandApprove_Click" />
                    <a class="col-md-offset-1 col-md-2 btn btn-warning" href="AdminHome.aspx">Cancel</a>
                </div>
            </div>
        </form>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtFiscalOfficer").autocomplete({
                minlength: 2,
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
        
        $('#modifyRequest').validate({
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
    </script>

    
</asp:Content>

