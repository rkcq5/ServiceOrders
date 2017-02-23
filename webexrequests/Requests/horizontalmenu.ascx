<%@ Control Language="C#" AutoEventWireup="true" CodeFile="horizontalmenu.ascx.cs" Inherits="horizontalmenu" %>



<nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0; background-color: #0065CC">
    <div class="navbar-header">
        <a href="Default.aspx" class="navbar-brand" style="background: #0065CC; color: white"">WebEx Telephony Requests</a>
    </div>
    <ul class="nav navbar-top-links navbar-right">
        <li>
            <table>
                <tr>
                    <td><div id="divImg1" style="margin-top: 10px;">
                    </div></td>
                    <td>&nbsp;<asp:Label class=" hidden-xs hidden-sm" ID="lblSSOId" runat="server" Text="" Font-Italic="True" ForeColor="White" Font-Bold="True"></asp:Label></td>
                    <td><a href="logout.aspx" style="background: #0065CC; color: white"><i class="fa fa-sign-out"></i>Log out</a></td>
                </tr>
            </table>
             <%--<div id="divImg1" style="margin-top: 20px;">
                    </div>
            <a href="logout.aspx" style="background: #0065CC; color: white">
               <asp:Label ID="lblSSOId" runat="server" Text="" Font-Italic="True" Font-Bold="True"></asp:Label>               
                <i class="fa fa-sign-out"></i>Log out
            </a>--%>
        </li>
    </ul>
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
        /*border-radius: 50% !important;*/
        width: 30px !important;
        height: 30px !important;
        background-size: cover;
        /*display: inline-block !important;*/
        background-repeat: no-repeat !important;
        background-position: top;
    }
</style>


<%--<style>
    .user {
        float: left;
        width: 32% !important;
        height: 32% !important;
        border-radius: 50% !important;
        background-position: center center !important;
        background-size: cover !important;
        margin-left: -27% !important;
        border: 18px solid;
        border-color: white;
    }

    .margin {
        /*margin-left: 14% !important;*/
    }

    @media only screen and (max-width: 768px) {
        /* For mobile phones: */
        .visibility {
            display: inline-block !important;
        }
    }
</style>
--%>
