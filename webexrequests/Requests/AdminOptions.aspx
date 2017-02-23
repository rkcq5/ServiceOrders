<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="AdminOptions.aspx.cs" Inherits="Requests_AdminOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-header" style="margin-top: 0px; margin-bottom: 0px;">
        <h3 class="">Admins        
                    <a class="btn btn-success btn-sm pull-right" style="margin-top: -10px;" data-toggle="modal" data-target="#newProposal"><i class="fa fa-plus"></i>&nbsp;Add Admin</a>
        </h3>
    </div>
    <%--<div class="panel panel-default">
        <div class="panel-heading" style="padding-bottom: 19px;">
            <div class="panel-title lineh32">
                <span>Admins</span>
                <div class="pull-right">
                    <a class="btn btn-success btn-sm pull-right" data-toggle="modal" data-target="#newProposal"><i class="fa fa-plus"></i>&nbsp;Add Admin</a>
                </div>
            </div>
        </div>
    </div>--%>
    <style>
        .modal-content {
            top: 150px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myTable").DataTable({

                responsive: true,
            });

            $("#btnAddAdmin").click(function () {
                var sso = $("#SSO").val();
                var EmplId = $("#EmplId").val();

                $.ajax({
                    method: "POST",
                    url: "AdminOptions.aspx/AddAdmin?SSO=" + sso + "&EmplId=" + EmplId,
                    contentType: "application/json; charset=utf-8",
                    success: function (r) {
                        location.reload(true);
                    },
                    failure: function () {
                        alert("failed");
                    }

                });

            });


            $('#AddAdminForm').validate({
                ignore: ":hidden",
                rules: {
                    '<%=SSO.UniqueID %>': {
                    required: true,
                    maxlength: 20
                },
                '<%=EMPLID.UniqueID %>': {
                    required: true,
                    maxlength: 20
                }
            },

                messages: {
                    '<%=SSO.UniqueID %>': { required: "Please enter SSO" },
                '<%=EMPLID.UniqueID %>': { required: "Please enter Employee ID" }
            },

                errorPlacement: function (error, element) {
                    if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.insertAfter(element);
                    }
                }
            });

        });

        function DeleteAdmin(emplId, sso) {

            bootbox.confirm({
                message: "Are you sure you want to delete (" + sso + ")?",
                animate: true,
                buttons: {
                    cancel: {
                        label: "Cancel",
                        className: "btn-primary btn-xs",
                    },
                    confirm: {
                        label: "Delete",
                        className: "btn-warning btn-xs",
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            type: "POST",
                            url: "AdminOptions.aspx/DeleteAdmin?emplId=" + emplId,
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                location.reload(true);
                            },
                            failure: function (response) {

                            }
                        });
                    }
                }
            });

        }

        

    </script>

    <div class="panel-body" style="background-color: white" id="divWebExRequests" runat="server">
    </div>

 <%--   <div class="modal fade" id="myModal" role="dialog">

        <div class="modal-dialog">


            <div class="modal-content">
                <div class="modal-header" style="background-color: #1c84c6">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Description</h4>
                </div>
                <div class="modal-body">
                    <p id="bookId"></p>
                    <input type="hidden" id="sso" name="sso" value="" />
                </div>
                <div class="modal-footer">
                    <a href="#" id="btn_proceed" class="btn btn-success">Confirm</a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>--%>

    <div id="newProposal" class="modal fade" tabindex="-1" role="dialog">

        <form id="AddAdminForm" runat="server">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Add Admin</h4>
                    </div>

                    <div class="modal-body">
                        <div id="slide1" class="slide form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-4">SSO</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="SSO" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <%--<input type="text" class="form-control" id="SSO" name="SSO" ClientIDMode="Static" />--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-4">Employee ID</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="EMPLID" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    <%--<input type="text" class="form-control" id="EmplId" name="EmplId" ClientIDMode="Static"/>--%>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Close</button>
                        <button class="btn btn-success btn-sm" id="btnAddAdmin" title="Add Admin">Add Admin</button>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script src="../common/_js/jquery.dataTables.min.js"></script>
</asp:Content>

