<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckOut.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-10">
        <div class="page-header">
            <h1 class="">Department Information</h1>
        </div>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="lblDepartmentName" runat="server" CssClass="control-label col-md-4" Text="Department Name" AssociatedControlID="txtDepartmentName"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblContactName" runat="server" CssClass="control-label col-md-4" Text="Contact Name" AssociatedControlID="txtContactName"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblPhoneNumber" runat="server" CssClass="control-label col-md-4" Text="Phone Number" AssociatedControlID="txtPhoneNumber"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Please enter extension only"></asp:TextBox>

                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblContactEmail" runat="server" CssClass="control-label col-md-4" Text="Contact Email" AssociatedControlID="txtContactEmail"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblMoCode" runat="server" CssClass="control-label col-md-4" Text="MoCode" AssociatedControlID="txtMoCode"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMoCode" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAuthorizedSignerName" runat="server" CssClass="control-label col-md-4" Text="Name of Authorized signer" AssociatedControlID="txtNameOfAuthorizedSigner"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtNameOfAuthorizedSigner" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-4 col-md-7">
                    <asp:Button ID="btnCheckOut" runat="server" Text="Check out" OnClientClick="return confirmCheckOut(this);" CssClass="col-md-offset-1 col-md-3 btn btn-primary" OnClick="btnCheckOut_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>

        function confirmCheckOut(sender) {
            if ($(sender).attr("confirmed") == "true") { return true; }

            bootbox.confirm({
                size: "small",
                message: "Are you sure you want to Check out?",
                animate: true,
                buttons: {
                    cancel: {
                        label: "Cancel",
                        className: "btn-primary btn-xs",
                    },
                    confirm: {
                        label: "Check Out",
                        className: "btn-warning btn-xs",
                    }
                },
                callback: function (confirmed) {
                    if (confirmed) {
                        $(sender).attr("confirmed", confirmed).trigger("click");
                    }
                }
            });
            return false;
        }

        $('#service').validate({
            ignore: ":hidden",
            rules: {

                '<%=txtDepartmentName.UniqueID %>': {
                    required: true
                },

                '<%=txtContactName.UniqueID %>': {
                    required: true
                },
                '<%=txtPhoneNumber.UniqueID %>': {
                    required: true,
                    number: true,
                    maxlength: 4,
                    minlength: 4
                },
                '<%=txtContactEmail.UniqueID %>': {
                    required: true,
                    email: true
                },
                '<%=txtMoCode.UniqueID %>': {
                    required: true
                },
                '<%=txtNameOfAuthorizedSigner.UniqueID %>': {
                    required: true
                }
            },
            messages: {
                '<%=txtDepartmentName.UniqueID %>': { required: "Please enter a Department Name" },
                '<%=txtContactName.UniqueID %>': { required: "Please enter a contact name" },
                '<%=txtPhoneNumber.UniqueID %>': { required: "Please enter extension only" },
                '<%=txtContactEmail.UniqueID %>': { required: "Please enter a valid contact email" },
                '<%=txtMoCode.UniqueID %>': { required: "Please enter MoCode" },
                '<%=txtNameOfAuthorizedSigner.UniqueID %>': { required: "Please enter an Authorized signer" }
            },
            // errorElement: 'span',
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

