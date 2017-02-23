<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="AdminHome.aspx.cs" Inherits="Requests_AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div class="page-header" style="margin-top: 0px; margin-bottom: 0px;">
        <h3 class="">Request Details</h3>
    </div>
    <style>
        .modal-content {
            top: 150px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#myTable").DataTable({
               
                scrollCollapse: false,
              
                responsive: true
              
            });

            $("#btnExportData").click(function () {
                var startDate = $("#startDate").val();
                var endDate = $("#endDate").val();

                $.ajax({
                    method: "POST",
                    url: "AdminHome.aspx/ExportData?startDate=" + startDate + "&endDate=" + endDate,
                    contentType: "application/json; charset=utf-8",
                    success: function (r) {
                        alert(r);
                    },
                    failure: function () {
                        alert("failed");
                    }
                });

            });
            $(function () {
                $("#startDate, #endDate").datepicker({
                    dateformat: 'mmm-dd-yy'

                });
            });
        });

        function viewHistory(id) {

            $.ajax({
                type: "POST",
                url: "AdminHome.aspx/getVersions?id=" + id,
                contentType: "application/json; charset=utf-8",
                success: function (r) {
                    viewDescription(r, id)
                },
                failure: function (response) {
                },
            });
        }

        function viewDescription(description, id) {

            $("#myModal").modal('show');
            $(".modal-body #bookId").html(description.d);
            $(".modal-body #bookId").css("width", "auto");
            $(".modal-body #bookId").css("word-wrap", "break-word");
            $(".modal-header #reqno").text(id);
        }


        function DeleteRequest(id) {
            bootbox.confirm({
                message: "Are you sure you want to delete request number " + id + "?",
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
                            url: "AdminHome.aspx/DeleteRequest?id=" + id,
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

        function EditRequest(SSO, status, date) {
            bootbox.alert({
                message: SSO + " has previously " + status + " this request on " + date + ". You cannot modify this request.",
                animate: true,
                style: "Width:500px;"
            });
        }

    </script>
    <div class="panel-body" style="background-color: white" id="divWebExRequests" runat="server">
    </div>



    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #1c84c6">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="color: white" title="sdkjx">Request Details :
                        <asp:Label ID="reqno" runat="server" ClientIDMode="Static"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <p id="bookId"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>



    <%--  <div id="newProposal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Export Data</h4>
                </div>
                <div class="modal-body">
                    <div id="slide1" class="slide form-horizontal">
                        <div class="form-group">
                            <label class="control-label col-md-4">Start Date</label>
                            <div class="col-md-6">
                                <input type="text" class="form-control" id="startDate" name="startDate" value="" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-4">End Date</label>
                            <div class="col-md-6">
                                <input type="text" class="form-control" id="endDate" name="endDate" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Close</button>
                    <button class="btn btn-success btn-sm" id="btnExportData" title="Add Admin">Export to Excel</button>
                </div>
            </div>
        </div>
    </div>--%>
    <script src="../common/_js/jquery.dataTables.min.js"></script>
</asp:Content>

