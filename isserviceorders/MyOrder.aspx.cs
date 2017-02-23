using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SSOID"] == null || Session["ServiceOrder"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            DisplayCartItems();
        }
    }

    private void DisplayCartItems()
    {
        string cartInformation = "";
        StringBuilder departmentInformation = new StringBuilder();
        ServiceOrder service = (ServiceOrder)Session["ServiceOrder"];
        departmentInformation.Append("<div class='table-responsive'><div class='col-md-10'>");
        departmentInformation.Append("<div class='row'><h3><center>Department Information</center></h3></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>Department Name:</p></div><div class='col-md-5'>").Append(service.DepartmentName).Append("</div></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>Contact Name:</p></div><div class='col-md-5'>").Append(service.ContactName).Append("</div></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>Contact Email :</p></div><div class='col-md-5'>").Append(service.ContactEmail).Append("</div></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>Contact phone :</p></div><div class='col-md-5'>").Append(service.ContactPhone).Append("</div></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>Authorized Signer Name:</p></div><div class='col-md-5'>").Append(service.AuthorizedSignerName).Append("</div></div>");
        departmentInformation.Append("<div class='row'><div class='col-md-5'><p>MoCode:</p></div><div class='col-md-5'>").Append(service.MoCode).Append("</div></div>");

        departmentInformation.Append("</div></div>");
        
        pDepartmentInformation.InnerHtml = departmentInformation.ToString();
        itemsCount.InnerHtml = "<h3>You have ordered  - " + service.Count + " Service(s)</h3>";

        if (service.AnalogServiceList != null)
        {
            foreach (var item in service.AnalogServiceList)
            {
                int number = item.Key;
                var aService = item.Value;
                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name :</p></div><div class='col-md-5'>" + aService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room :</p></div><div class='col-md-5'>" + aService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + aService.Jack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Direct In Dial :</p></div><div class='col-md-5'>" + checkValue(aService.DirectInDial) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Long Distance Direct Dial Dropdown :</p></div><div class='col-md-5'>" + checkValue(aService.LongDistanceDirectDial) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Auth Code :</p></div><div class='col-md-5'>" + checkValue(aService.AuthCode) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + aService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }
        if (service.DataServicesList != null)
        {
            foreach (var item in service.DataServicesList)
            {
                int number = item.Key;
                var dService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + dService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + dService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + dService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Existing Jack ?</p></div><div class='col-md-5'>" + checkValue(dService.ExistingDataJack) + "</div></div>";
                if (dService.ExistingDataJack)
                {
                    cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + dService.Jack + "</div></div>";
                }
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + dService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }
        if (service.VoiceServicesList != null)
        {
            foreach (var item in service.VoiceServicesList)
            {

                int number = item.Key;
                var vService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + vService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + vService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + vService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + vService.Jack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Type :</p></div><div class='col-md-5'>" + vService.PhoneType + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Direct In Dial :</p></div><div class='col-md-5'>" + checkValue(vService.DirectInDial) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Voicemail Box :</p></div><div class='col-md-5'>" + checkValue(vService.VoiceMailBox) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Long Distance Direct Dial Dropdown:</p></div><div class='col-md-5'>" + checkValue(vService.LongDistanceDirectDial) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Auth Code :</p></div><div class='col-md-5'>" + checkValue(vService.AuthCode) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Call Pickup group:</p></div><div class='col-md-5'>" + checkValue(vService.CallPickUpgroup) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>CallerID :</p></div><div class='col-md-5'>" + vService.CallerID + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + vService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }

        if (service.ChangeServicesList != null)
        {
            foreach (var item in service.ChangeServicesList)
            {

                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Number:</p></div><div class='col-md-5'>" + aService.PhoneNumber + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Type:</p></div><div class='col-md-5'>" + aService.PhoneType + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + aService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + aService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + aService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }

        if (service.MoveServicesList != null)
        {
            foreach (var item in service.MoveServicesList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Moving:</p></div><div class='col-md-5'>" + aService.MovingType + "</div></div>";
                if (aService.MovingType == "Phone Line")
                {
                    cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Type:</p></div><div class='col-md-5'>" + aService.PhoneType + "</div></div>";

                }
                cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Number:</p></div><div class='col-md-5'>" + aService.PhoneNumber + "</div></div>";
                cartInformation += "<div class='row'><p>From :</p></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + aService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + aService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + aService.FromJack + "</div></div>";
                cartInformation += "<div class='row'><p>To :</p></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name</p></div><div class='col-md-5'>" + aService.ToBuildingName + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + aService.ToRoom + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + aService.ToJack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + aService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }

        if (service.DisconnectServiceList != null)
        {
            foreach (var item in service.DisconnectServiceList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Phone Number:</p></div><div class='col-md-5'>" + aService.PhoneNumber + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + aService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + aService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + aService.Jack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + aService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }

        if (service.ActivationServicesList != null)
        {
            foreach (var item in service.ActivationServicesList)
            {

                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + aService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + aService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Existing Jack ?</p></div><div class='col-md-5'>" + checkValue(aService.ExistingDataJack) + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + aService.Jack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + aService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }

        if (service.LaborServicesList != null)
        {
            foreach (var item in service.LaborServicesList)
            {
                int number = item.Key;
                var lService = item.Value;

                cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                cartInformation += "<div class='row'><h3>" + lService.ServiceName + "</h3></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>LaborType:</p></div><div class='col-md-5'>" + lService.LaborType + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Building Name:</p></div><div class='col-md-5'>" + lService.Building + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Room:</p></div><div class='col-md-5'>" + lService.Room + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Jack:</p></div><div class='col-md-5'>" + lService.Jack + "</div></div>";
                cartInformation += "<div class='row'><div class='col-md-5'><p>Comments :</p></div><div class='col-md-5'>" + lService.Comments + "</div></div>";
                cartInformation += "</div></div></div>";
            }
        }
        divCart.InnerHtml = cartInformation;
    }

    private string checkValue(bool value)
    {
        if (value)
        {
            return "yes";
        }
        else
        {
            return "No";
        }
    }

    protected void btnReturnToForm_Click(object sender, EventArgs e)
    {
        Session.Remove("ServiceOrder");
        Response.Redirect("~/Default.aspx");
    }
}

