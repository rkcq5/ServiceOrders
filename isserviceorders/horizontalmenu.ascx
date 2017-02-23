<%@ Control Language="C#" AutoEventWireup="true" CodeFile="horizontalmenu.ascx.cs" Inherits="horizontalmenu" %>


<nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0;background-color: #0065CC">


   <div class="navbar-header">
              
                <a href="Default.aspx" class="navbar-brand" style="background: #0065CC;">IS Service Orders</a>
       

  
       </div>
      <ul class="nav navbar-top-links navbar-right" >
        <li>
       <%--     <div id="divImg" runat="server">
                
            </div>--%>
            <a href="logout.aspx" style="background: #0065CC; color: white">
                <i class="fa fa-sign-out"></i>Log out
                (<asp:Label ID="lblSSOId" runat="server" Text="" Font-Italic="True" Font-Bold="True"></asp:Label>)
            </a>
        </li>
    </ul>
</nav>
