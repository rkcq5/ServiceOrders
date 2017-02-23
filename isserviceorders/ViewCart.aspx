<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="ViewCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function deleteClick(id, requestname) {

            //$("#alertModal").modal('show');
            //// $("#description").innerHTML = "<div>" + description + "</div>";
            //$(".modal-body #bookId").html(description);
            //$(".modal-body #bookId").css("width", "auto");
            //$(".modal-body #bookId").css("word-wrap", "break-word");


            bootbox.confirm({
                size: 'small',
                message: "Are you sure ?",
                animate: true,
                style:"Width:500px;",
                buttons: {
                    cancel: {
                        label: "Cancel",
                        className: "btn-primary btn-xs",
                    },
                    confirm: {
                        label:"Delete",
                        className: "btn-warning btn-xs",
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            type: "POST",
                            url: "ViewCart.aspx/DeleteRequest?id=" + id,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="ibox col-md-10">
                <div class="ibox-title">
                    <p id="itemsCount" runat="server"></p>
                </div>
                <div class="ibox-content panel-body">
                    <div id="divCart" runat="server">
                    </div>


                </div>
            </div>
        </div>
        <div class="row">
            <div class="ibox col-md-10">
                <asp:Button ID="btnGoToShop" runat="server" Text="Continue Adding" CssClass="col-md-offset-2 col-md-3 btn btn-primary" OnClick="btnGoToShop_Click" />
                <asp:Button ID="btnCheckoutDetails" runat="server" Text="Check Out" CssClass="col-md-offset-2 col-md-3 btn btn-warning" OnClick="btnCheckoutDetails_Click" ClientIDMode="Static" ToolTip="Continue to Check Out" />
            </div>
        </div>
    </div>


    <div class="modal fadein" id="alertModal" role="dialog">
        <div class="modal-dialog" style="width: 400px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Alert !!!</h4>
                </div>
                <div class="modal-body">
                    <p id="bookId"></p>
                </div>
                <div class="modal-footer">
                    <div>
                        <a class="col-md-offset-2 col-md-4 btn btn-primary btn-xs" href="Default.aspx">Continue Adding</a>
                        <a class="col-md-offset-8 col-md-4 btn btn-warning btn-xs" href="ViewCart.aspx">View Cart</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

