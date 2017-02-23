using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ViewCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SSOID"] == null)
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

        if (Session["ServiceOrder"] == null)
        {
            cartInformation = DisplayZeroItems(cartInformation);

        }
        else
        {
            ServiceOrder service = (ServiceOrder)Session["ServiceOrder"];

            if (service.Count == 0)
            {
                cartInformation = DisplayZeroItems(cartInformation);
            }

            itemsCount.InnerHtml = "<h3>Items in your cart  - " + service.Count + "</h3>";

            if (service.AnalogServiceList != null)
            {
                foreach (var item in service.AnalogServiceList)
                {
                    int number = item.Key;
                    var aService = item.Value;
                    cartInformation += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-10'>";
                    cartInformation += "<div class='row'><h3>" + aService.ServiceName + "</h3></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name :</div><div class='col-md-5'>" + aService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p><div class='col-md-5'>Room :</div><div class='col-md-5'>" + aService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + aService.Jack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Direct In Dial :</div><div class='col-md-5'>" + checkValue(aService.DirectInDial) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Long Distance Direct Dial Dropdown :</div><div class='col-md-5'>" + checkValue(aService.LongDistanceDirectDial) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Auth Code :</div><div class='col-md-5'>" + checkValue(aService.AuthCode) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + aService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p><div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + dService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><div class='col-md-5'>Room:</div><div class='col-md-5'>" + dService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><div class='col-md-5'>Existing Jack?</div><div class='col-md-5'>" + checkValue(dService.ExistingDataJack) + "</div></p></div>";
                    if (dService.ExistingDataJack)
                    {
                        cartInformation += "<div class='row'><div class='col-md-5'>Jack:</div><div class='col-md-5'>" + dService.Jack + "</div></p></div>";
                    }
                    cartInformation += "<div class='row'><div class='col-md-5'>Comments :</div><div class='col-md-5'>" + dService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + vService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + vService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + vService.Jack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Type :</div><div class='col-md-5'>" + vService.PhoneType + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Direct In Dial :</div><div class='col-md-5'>" + checkValue(vService.DirectInDial) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Voicemail Box :</div><div class='col-md-5'>" + checkValue(vService.VoiceMailBox) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Long Distance Direct Dial Dropdown:</div><div class='col-md-5'>" + checkValue(vService.LongDistanceDirectDial) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Auth Code :</div><div class='col-md-5'>" + checkValue(vService.AuthCode) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Call Pickup group:</div><div class='col-md-5'>" + checkValue(vService.CallPickUpgroup) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Existing Phone Number:</div><div class='col-md-5'>" + vService.ExistingPhoneNumber + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>CallerID :</div><div class='col-md-5'>" + vService.CallerID + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + vService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Number:</div><div class='col-md-5'>" + aService.PhoneNumber + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Type:</div><div class='col-md-5'>" + aService.PhoneType + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + aService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + aService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + aService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Moving:</div><div class='col-md-5'>" + aService.MovingType + "</div></p></div>";
                    if (aService.MovingType == "Phone Line")
                    {
                        cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Type:</div><div class='col-md-5'>" + aService.PhoneType + "</div></p></div>";

                    }
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Number:</div><div class='col-md-5'>" + aService.PhoneNumber + "</div></p></div>";
                    cartInformation += "<div class='row'><p>From :</p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + aService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + aService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + aService.FromJack + "</div></p></div>";
                    cartInformation += "<div class='row'><p>To :</p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name</div><div class='col-md-5'>" + aService.ToBuildingName + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + aService.ToRoom + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + aService.ToJack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + aService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Phone Number:</div><div class='col-md-5'>" + aService.PhoneNumber + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + aService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + aService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + aService.Jack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + aService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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

                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + aService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + aService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Existing Jack ?</div><div class='col-md-5'>" + checkValue(aService.ExistingDataJack) + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + aService.Jack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + aService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
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
                    cartInformation += "<div class='row'><p<div class='col-md-5'>LaborType:</div><div class='col-md-5'>" + lService.LaborType + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Building Name:</div><div class='col-md-5'>" + lService.Building + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Room:</div><div class='col-md-5'>" + lService.Room + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Jack:</div><div class='col-md-5'>" + lService.Jack + "</div></p></div>";
                    cartInformation += "<div class='row'><p<div class='col-md-5'>Comments :</div><div class='col-md-5'>" + lService.Comments + "</div></p></div>";
                    cartInformation += "<div class='row'><a href='#' onClick='deleteClick(" + number + ")'><span class='glyphicon glyphicon-trash'></span>Delete this item</a></div>";
                    cartInformation += "</div></div></div>";
                }
            }
        }
        divCart.InnerHtml = cartInformation;
    }

    private string DisplayZeroItems(string cartInformation)
    {
        itemsCount.InnerHtml = "<h3>Items in your cart  - 0</h3>";
        btnCheckoutDetails.Enabled = false;

        cartInformation = cartInformation + "<div class='ibox-content'><div class='table-responsive'><table class='table shoping-cart-table'><tbody>";
        cartInformation = cartInformation + "<tr><td class='desc'>";
        cartInformation = cartInformation + "<h3> No Items in your Cart</h3>";
        cartInformation = cartInformation + "</td></tr></tbody></table></div></div>";
        return cartInformation;
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
    protected void btnCheckoutDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CheckOut.aspx");
    }
    protected void btnGoToShop_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    [WebMethod]
    public static void DeleteRequest()
    {
        int i = int.Parse(HttpContext.Current.Request.QueryString["id"]);

        ServiceOrder service = (ServiceOrder)HttpContext.Current.Session["ServiceOrder"];

        if (service.AnalogServiceList != null &&
            service.AnalogServiceList.ContainsKey(i))
        {
            service.AnalogServiceList.Remove(i);
            service.Count--;
        }
        else if (service.DataServicesList != null && service.DataServicesList.ContainsKey(i))
        {
            service.DataServicesList.Remove(i);
            service.Count--;
        }
        else if (service.VoiceServicesList != null && service.VoiceServicesList.ContainsKey(i))
        {
            service.VoiceServicesList.Remove(i);
            service.Count--;
        }
        else if (service.ChangeServicesList != null && service.ChangeServicesList.ContainsKey(i))
        {
            service.ChangeServicesList.Remove(i);
            service.Count--;
        }
        else if (service.DisconnectServiceList != null && service.DisconnectServiceList.ContainsKey(i))
        {
            service.DisconnectServiceList.Remove(i);
            service.Count--;
        }
        else if (service.MoveServicesList != null && service.MoveServicesList.ContainsKey(i))
        {
            service.MoveServicesList.Remove(i);
            service.Count--;
        }
        else if (service.ActivationServicesList != null && service.ActivationServicesList.ContainsKey(i))
        {
            service.ActivationServicesList.Remove(i);
            service.Count--;
        }
        else if (service.LaborServicesList != null && service.LaborServicesList.ContainsKey(i))
        {
            service.LaborServicesList.Remove(i);
            service.Count--;
        }

        HttpContext.Current.Session["ServiceOrder"] = service;

    }
}

