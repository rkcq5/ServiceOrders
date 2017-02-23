<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveUrl("~/js/default.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-10">

        <%--<form id="service" runat="server">--%>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="lblservice" runat="server" CssClass="control-label col-md-4" Text="Select a service" AssociatedControlID="ddlService" ClientIDMode="Static"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Select service" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Add" Value="Add" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Change" Value="Change"></asp:ListItem>
                        <asp:ListItem Text="Move" Value="Move"></asp:ListItem>
                        <asp:ListItem Text="Disconnect" Value="Disconnect"></asp:ListItem>
                        <asp:ListItem Text="Activation" Value="Activation"></asp:ListItem>
                        <asp:ListItem Text="Labor" Value="Labor"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group AddService">
                <asp:Label ID="lbladdSubService" runat="server" CssClass="control-label col-md-4" Text="Select add service" AssociatedControlID="ddladdSubService"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddladdSubService" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <%--<asp:ListItem Enabled="true" Text="Select add service" Value="-1"></asp:ListItem>--%>
                        <asp:ListItem Text="Data" Value="Data"></asp:ListItem>
                        <asp:ListItem Text="Voice" Value="Voice"></asp:ListItem>
                        <asp:ListItem Text="Analog" Value="Analog"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group MoveService">
                <asp:Label ID="lblMovingType" runat="server" CssClass="control-label col-md-4" Text="What are you Moving?" AssociatedControlID="ddlMovingType"></asp:Label>
                <div class="col-md-6">

                    <asp:DropDownList ID="ddlMovingType" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Phone Line" Value="Phone Line"></asp:ListItem>
                        <asp:ListItem Text="Fax Line" Value="Fax Line"></asp:ListItem>
                        <asp:ListItem Text="Alarm Line" Value="Alarm Line"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group VoiceService ChangeService MoveService" id="div_PhoneType">
                <asp:Label ID="lblphoneType" runat="server" CssClass="control-label col-md-4" Text="Phone Type" AssociatedControlID="ddlphoneType"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlphoneType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="VOIP 6921" Value="VOIP 6921"></asp:ListItem>
                        <asp:ListItem Text="VOIP 7965" Value="VOIP 7965"></asp:ListItem>
                        <asp:ListItem Text="VOIP 7911" Value="VOIP 7911"></asp:ListItem>
                        <asp:ListItem Text="Jabber Softphone" Value="Jabber Softphone"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>


            <div class="form-group DisconnectService MoveService">
                <asp:Label ID="lblPhoneNumber" runat="server" CssClass="control-label col-md-4" Text="Phone Number" AssociatedControlID="txtPhoneNumber"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Please enter extension only"></asp:TextBox>
                </div>
            </div>

            <div class="form-group MoveService">
                <asp:Label ID="lblMoving" runat="server" CssClass="control-label col-md-offset-4 col-md-4 col-md-offset-4" Text="Moving From"></asp:Label>
            </div>
            <div class="form-group LaborService">
                <asp:Label ID="lblLaborType" runat="server" CssClass="control-label col-md-4" Text="Labor Type" AssociatedControlID="ddlLaborType"></asp:Label>
                <div class="col-md-6">

                    <asp:DropDownList ID="ddlLaborType" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Time And Material" Value="Time And Material"></asp:ListItem>
                        <asp:ListItem Text="Demolition" Value="Demolition"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblbuildingName" runat="server" CssClass="control-label col-md-4" Text="Building" AssociatedControlID="ddlbuildingName"></asp:Label>
                <div class="col-md-6">
                    <%-- <asp:TextBox ID="txtbuildingName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlbuildingName" runat="server" CssClass="form-control" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblroomNumber" runat="server" CssClass="control-label col-md-4" Text="Room" AssociatedControlID="txtroomNumber"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtroomNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group AddService DataService ActivationService">
                <asp:Label ID="lblExistingJack" runat="server" CssClass="control-label col-md-4" Text="Existing Data Jack?" AssociatedControlID="ddlExistingJack"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlExistingJack" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group DisconnectService MoveService ActivationService VoiceService AnalogService ChangeService LaborService">
                <asp:Label ID="lbljack" runat="server" CssClass="control-label col-md-4" Text="Jack" AssociatedControlID="txtjack"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtjack" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
            </div>
            <div class="form-group AddService DataService" id="div_ExistingJack">
                <asp:Label ID="lblDataJackOnly" runat="server" CssClass="control-label col-md-4" Text="Jack" AssociatedControlID="txtDataJackOnly"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtDataJackOnly" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group MoveService">
                <asp:Label ID="lblMovingTo" runat="server" CssClass="control-label col-md-offset-4 col-md-4 col-md-offset-4" Text="Moving To"></asp:Label>
            </div>

            <div class="form-group MoveService">
                <asp:Label ID="lblMovingToBuilding" runat="server" CssClass="control-label col-md-4" Text="Building" AssociatedControlID="ddlToBuildingName"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlToBuildingName" runat="server" CssClass="form-control" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group MoveService">
                <asp:Label ID="Label3" runat="server" CssClass="control-label col-md-4" Text="Room" AssociatedControlID="txtMovingToRoom"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMovingToRoom" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group MoveService">
                <asp:Label ID="lblMovingToJack" runat="server" CssClass="control-label col-md-4" Text="Jack" AssociatedControlID="txtMovingToJack"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtMovingToJack" runat="server" CssClass="form-control"></asp:TextBox>

                </div>
            </div>
            <div class="form-group  AddService AnalogService">
                <asp:Label ID="lblDirectinDial" runat="server" CssClass="control-label col-md-4" Text="Direct in Dial" AssociatedControlID="ddldirectInDial"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddldirectInDial" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group VoiceService AddService">
                <asp:Label ID="lblvoiceMailbox" runat="server" CssClass="control-label col-md-4" Text="Voicemail Box" AssociatedControlID="ddlVoiceMailBox"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlVoiceMailBox" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group VoiceService AddService" id="div_UserEmailAlias">
                <asp:Label ID="lblUserEmailAlias" runat="server" CssClass="control-label col-md-4" Text="User email alias" AssociatedControlID="txtUserEmailAlias"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtUserEmailAlias" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>


            <div class="form-group AnalogService AddService">
                <asp:Label ID="lbllongDistanceDial" runat="server" CssClass="control-label col-md-4" Text="Long distance direct dial" AssociatedControlID="ddllongDistanceDial"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddllongDistanceDial" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group AnalogService AddService" id="div_AuthCode">
                <asp:Label ID="lblAuthCode" runat="server" CssClass="control-label col-md-4" Text="Auth code" AssociatedControlID="ddlAuthCode"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlAuthCode" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group VoiceService AddService">
                <asp:Label ID="lblCallPickUpGroup" runat="server" CssClass="control-label col-md-4" Text="Call Pickup group" AssociatedControlID="ddlCallPickUpGroup"></asp:Label>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlCallPickUpGroup" runat="server" CssClass="form-control" ClientIDMode="Static">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="False" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group VoiceService AddService" id="div_CallGroupPhoneNumber">
                <asp:Label ID="lblCallGroupPhoneNumber" runat="server" CssClass="control-label col-md-4" Text="Existing phone number" AssociatedControlID="txtCallGroupPhoneNumber"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCallGroupPhoneNumber" runat="server" CssClass="form-control" placeholder="Please enter extension only"></asp:TextBox>
                </div>
            </div>

            <div class="form-group VoiceService AddService">
                <asp:Label ID="Label1" runat="server" CssClass="control-label col-md-4" Text="Caller Id" AssociatedControlID="txtCallerId"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCallerId" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblcomments" runat="server" CssClass="control-label col-md-4" Text="Comments/ Special Instructions" AssociatedControlID="txtcomments"></asp:Label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtcomments" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-4 col-md-7">
                    <asp:Button ID="btnaddToCart" runat="server" Text="Add to Cart" CssClass="col-md-3 btn btn-primary" OnClick="btnaddToCart_Click" />
                    <a class="col-md-offset-1 col-md-3 btn btn-warning" href="ViewCart.aspx">View Cart</a>
                </div>
            </div>
        </div>
        <%--</form>--%>
    </div>
    <script>
        $('#service').validate({
            ignore: ":hidden",
            rules: {

                '<%=ddlbuildingName.UniqueID %>': {
                    required: true
                },

                '<%=txtjack.UniqueID %>': {
                    required: true
                },
                '<%=txtroomNumber.UniqueID %>': {
                    required: true
                },
                '<%=txtUserEmailAlias.UniqueID %>': {
                    required: true
                },
                '<%=ddlAuthCode.UniqueID %>': {
                    required: true
                },
                '<%=txtCallGroupPhoneNumber.UniqueID %>': {
                    required: true,
                    number: true,
                    maxlength: 4,
                    minlength: 4
                },
                '<%=txtPhoneNumber.UniqueID %>': {
                    required: true,
                    number: true,
                    maxlength: 4,
                    minlength: 4
                },
                '<%=ddlToBuildingName.UniqueID %>': {
                    required: true
                },
                '<%=txtMovingToRoom.UniqueID %>': {
                    required: true
                },
                '<%=txtcomments.UniqueID %>': {
                    maxlength: 300
                },
                '<%=txtCallerId.UniqueID %>': {
                    required: true
                },
                '<%=txtDataJackOnly.UniqueID%>': {
                    required: true
                }
            },
            messages: {
                '<%=ddlbuildingName.UniqueID %>': { required: "Please select a building" },
                '<%=txtjack.UniqueID %>': { required: "Please enter jack information" },
                '<%=txtroomNumber.UniqueID %>': { required: "Please enter a room number" },
                '<%=txtUserEmailAlias.UniqueID %>': { required: "Please enter a User Email Alias" },
                '<%=ddlAuthCode.UniqueID %>': { required: "Please select the AuthCode" },
                '<%=txtCallGroupPhoneNumber.UniqueID %>': { required: "Please enter extension only" },
                '<%=txtPhoneNumber.UniqueID %>': { required: "Please enter extension only" },
                '<%=ddlToBuildingName.UniqueID %>': { required: "Please select a building" },
                '<%=txtMovingToRoom.UniqueID %>': { required: "Please enter a room number" },
                '<%=txtCallerId.UniqueID %>': { required: "Please enter caller ID" },
                '<%=txtDataJackOnly.UniqueID%>': { required: "Please enter a existing Data Jack Value" }
            },
            //errorElement: 'span',
            // errorClass: "error.block",

            errorPlacement: function (error, element) {
                if (element.parent('.input-group').length) {
                    error.insertAfter(element.parent());
                } else {
                    error.insertAfter(element);
                }
            }
        });

        function showAlert(description) {

            $("#alertModal").modal('show');
            // $("#description").innerHTML = "<div>" + description + "</div>";
            $(".modal-body #bookId").html(description);
            $(".modal-body #bookId").css("width", "auto");
            $(".modal-body #bookId").css("word-wrap", "break-word");
        }
    </script>

    <div class="modal fadein" id="alertModal" role="dialog">
        <div class="modal-dialog" style="width: 350px;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Your Item was successfully added to the cart</h4>
                </div>
                <div class="modal-body">
                    <p id="bookId"></p>
                </div>
                <div class="modal-footer">
                    <div>
                        <a class="col-md-offset-4 col-md-4 btn btn-primary btn-xs" href="Default.aspx">Continue Adding</a>
                        <a class="col-md-offset-4 col-md-3 btn btn-warning btn-xs" href="ViewCart.aspx">View Cart</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

