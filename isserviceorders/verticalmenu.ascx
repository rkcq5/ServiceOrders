<%@ Control Language="C#" AutoEventWireup="true" CodeFile="verticalmenu.ascx.cs" Inherits="verticalmenu" %>

<nav class="navbar-default navbar-static-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav metismenu" id="side-menu">
            <li class="nav-header">
                <div class="profile-element">
                    <a href="Default.aspx">
                        <img alt="logo" style="height: 50px; margin: -26px 10px -10px 0; border: 0; padding: 0; vertical-align: top;" src="<%=ResolveUrl("~/images/UMKC_1C_white.png")%>"></a>

                    <span class="clear  visible-sm hidden-xs visible-md visible-lg" style="padding-top: 10px;"><span class="block m-t-xs" style="color: white;"><strong class="font-bold" style="font-weight: 600; font-size: large;">IS Service Orders</strong></span></span>

                </div>
                <%-- <div class="logo-element" style="height: 50px">
                    <a href="Default.aspx">
                        <img alt="logo" style="height: 28px; margin: -7px 10px -10px 0; border: 0; padding: 0; vertical-align: top;" src="<%=ResolveUrl("~/images/UMKC_1C_white.png")%>"></a>

                </div>--%>
            </li>
            <li class="active">
                <a href="Default.aspx"><i class="fa fa-th-large" title="Dashboard"></i><span class="nav-label">Dashboard</span></a>
            </li>
            
            
              <li class="visible-sm visible-xs hidden-md hidden-lg">
            <a href="logout.aspx">
                <i class="fa fa-sign-out" title="Log out"></i><span class="nav-label">Log out</span>
               
            </a>
        </li>
          <%--  <li>
                <a href="#"><i class="fa fa-table"></i><span class="nav-label">Tables</span> </a>
            </li>--%>
        </ul>

    </div>
</nav>
