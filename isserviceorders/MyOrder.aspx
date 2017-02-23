<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyOrder.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="col-md-12">
            <div class="row">
                <div class="ibox col-md-12">
                    <div class="ibox-title">
                       
                        <p id="pDepartmentInformation" runat="server"></p>
                         <p id="itemsCount" runat="server"></p>
                    </div>
                    <div class="ibox-content">

                        <div id="divCart" runat="server">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="ibox col-md-10">
                    <asp:Button ID="btnReturnToShop" runat="server" Text="Return to Form" CssClass="col-md-offset-5 col-md-3 btn btn-primary" OnClick="btnReturnToForm_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

