<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionOnRequest.aspx.cs" Inherits="ActionOnRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>WebEx Requests
    </title>

    <link href="<%=ResolveUrl("~/css/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/font-awesome/css/font-awesome.css")%>" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/plug-ins/f2c75b7247b/integration/bootstrap/3/dataTables.bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/1.0.4/css/dataTables.responsive.css" type="text/css" />
    <link href="<%=ResolveUrl("~/css/animate.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/css/style.css")%>" rel="stylesheet" />
    <link href="umkc-standards/umkc-styles.css" rel="stylesheet" />
    <!--#include file="~/umkc-standards/umkc-header.html"-->

    <style>
        #umkc_footerwrap {
            background-color: white;
        }
    </style>

</head>
<body class="top-navigation skin-1" style="background-color: white;">
    <div id="wrapper" style="background-color: white; max-height:700px; overflow-y: hidden; ">
        <div class="container">
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10">
                    <div id="page-wrapper" class="gray-bg" style="overflow-y: hidden; overflow-x: hidden; padding-bottom: 50px">
    <div class="wrapper wrapper-content animated fadeInRight">

        <form id="form1" runat="server">
            <br />
            <div style="text-align: center; font-size: large;">
                <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            </div>
            <br />
            <br /> <br />
            <div class="col-md-10">

                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblFirstName" runat="server" CssClass="control-label col-md-4" Text="First Name" AssociatedControlID="txtFirstName"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblLastName" runat="server" CssClass="control-label col-md-4" Text="Last Name" AssociatedControlID="txtLastName"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblUserName" runat="server" CssClass="control-label col-md-4" Text="User Name (SSO)" AssociatedControlID="txtUserName"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblFiscalOfficer" runat="server" CssClass="control-label col-md-4" Text="Fiscal Officer" AssociatedControlID="txtFiscalOfficer"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtFiscalOfficer" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblMoCode" runat="server" CssClass="control-label col-md-4" Text="MoCode" AssociatedControlID="txtMoCode"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtMoCode" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblComments" runat="server" CssClass="control-label col-md-4" Text="Comments" AssociatedControlID="txtComments"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

        </form>
        
    </div>

    <div class="footer">
        <div class="pull-right">
            <span class="pull-right hidden-md hidden-lg">&copy; UMKC | v1.0</span>

            <span class="pull-right hidden-sm hidden-xs">&copy; University of Missouri - Kansas City | v1.0</span>
        </div>
        <div>
            <a href="mailto:UMKCISInternalApplications-Support@umkc.edu"><u>Need Technical Help?</u></a>
            <span class="visible-xs-inline"><a href="#" class="plainlink" data-toggle="modal" data-target="#supportModal"><u>Need Help?</u></a></span>


        </div>
    </div>
                        </div>
                </div>
                <div class="col-lg-1"></div>
            </div>
        </div>
        <!-- Bootstrap core JavaScript
    ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->


        <!-- Data Tables-->
        <script src="https://cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
        <script src="//cdn.datatables.net/responsive/1.0.4/js/dataTables.responsive.js"></script>
        <script src="https://cdn.datatables.net/plug-ins/f2c75b7247b/integration/bootstrap/3/dataTables.bootstrap.js"></script>

        <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    </div>
    <!--#include file= "~/umkc-standards/umkc-footer.html"-->
</body>
</html>
