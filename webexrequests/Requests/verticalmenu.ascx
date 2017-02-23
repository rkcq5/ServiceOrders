<%@ Control Language="C#" AutoEventWireup="true" CodeFile="verticalmenu.ascx.cs" Inherits="verticalmenu" %>

<nav class="navbar-default navbar-static-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav metismenu" id="side-menu">
            <li class="nav-header">
                <div class="profile-element">
                    <%--<a href="Default.aspx">
                        <img alt="logo" style="height: 50px; margin: -26px 10px -10px 0; border: 0; padding: 0; vertical-align: top;" src="<%=ResolveUrl("~/images/UMKC_1C_white.png")%>"></a>--%>


                    <div id="divImg1" style="margin-top: 20px;">
                    </div>
                    <span class="block  "><strong class="font-bold">
                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="white"></asp:Label></strong>
                    </span>
                </div>
                <div class="logo-element" style="height: 72px; display: none !important">
                    <a href="Default.aspx">
                        <img alt="logo" style="height: 28px; margin: -7px 10px -10px 0; border: 0; padding: 0; vertical-align: top;" src="<%=ResolveUrl("~/images/UMKC_1C_white.png")%>"></a>
                    <strong class="font-bold" style="font-weight: 600; font-size: 12px;">WebEx Telephony Request</strong>
                </div>
            </li>

            <li>
                <a href="Default.aspx" id="liAddRequest" runat="server">
                    <i class="fa fa-book fa-lg" title="Dashboard"></i><span class="nav-label">Add Request</span>
                </a>
            </li>

            <li>
                <a href="AdminHome.aspx" id="liRequests" runat="server">
                    <i class="fa fa-book fa-lg" title="ViewRequests"></i><span class="nav-label">View Requests</span>
                </a>
            </li>
            
             <li>
                <a href="AdminOptions.aspx" id="liManageAdmins" runat="server">
                    <i class="fa fa-book fa-lg" title="Add/Delete Administrators"></i><span class="nav-label">Manage Admins</span>
                </a>
            </li>
            <li>
                <a href="Exportdata.aspx" id="liExportData" runat="server">
                    <i class="fa fa-book fa-lg" title="Dashboard"></i><span class="nav-label">Export Data</span>
                </a>
            </li>
            <li class=" visible-xs hidden-sm hidden-md hidden-lg">
                <a href="~/Logout.aspx" runat="server" id="logout">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </a>


            </li>

        </ul>

    </div>
</nav>

<script type="text/javascript">
    function loadProfileImage() {
        var emplid = '<%= Session["Emplid"] %>';

        if (emplid != "") {
            $.getJSON("https://cf3.umkc.edu/intapps/services/photo-service/photo.cfc?emplid=" + emplid + "&method=getProfileImgJsonP&callback=?",
                      function (data) {
                          $("#divImg1").css('background-image', 'url(data:png;base64,' + data.image + ')');
                      });
        }
    }

</script>
<style>
    #divImg1 {
        border-radius: 50% !important;
        width: 58px !important;
        height: 60px !important;
        background-size: cover;
        display: inline-block !important;
        background-repeat: no-repeat !important;
        background-position: top;
    }
</style>
