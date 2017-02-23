<%@ Page Title="" Language="C#" MasterPageFile="~/Requests/MasterPage.master" AutoEventWireup="true" CodeFile="testing.aspx.cs" Inherits="Requests_testing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>

<script>
    $(function () {
        $("#calendar1").datepicker();
    });
</script>
    <script>
        $(function () {
            $("#calendar2").datepicker();
        });
</script>

<p style="text-align: center">Start Date: <input type="text" id="calendar1" /></p>


<p style="text-align: center">End Date: <input type="text" id="calendar2" /></p>

    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</asp:Content>

