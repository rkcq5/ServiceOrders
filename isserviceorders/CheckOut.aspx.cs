using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using System.Net.Mail;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ServiceOrder"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (Session["ServiceOrder"] != null)
        {
            ServiceOrder serviceOrder = (ServiceOrder)Session["ServiceOrder"];
            if (serviceOrder.Count == 0)
            {
                Response.Redirect("~/ViewCart.aspx");
            }
        }
    }

    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        if (Session["ServiceOrder"] != null)
        {
            ServiceOrder serviceOrder = (ServiceOrder)Session["ServiceOrder"];
            serviceOrder.DepartmentName = txtDepartmentName.Text;
            serviceOrder.ContactName = txtContactName.Text;
            serviceOrder.ContactEmail = txtContactEmail.Text;
            serviceOrder.ContactPhone = txtPhoneNumber.Text;
            serviceOrder.MoCode = txtMoCode.Text;
            serviceOrder.AuthorizedSignerName = txtNameOfAuthorizedSigner.Text;
            if (SendEmail(serviceOrder) == 1)
            {
                InsertToDataBase(serviceOrder);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "Requested Submitted Succesfully");
               // Session.Remove("ServiceOrder");
                Response.Redirect("~/MyOrder.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "Problem in Submitting Request");
            }
        }
    }

    private int SendEmail(ServiceOrder serviceOrder)
    {
        string serverAddress = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        SmtpClient smtpClient = new SmtpClient();
        MailMessage objMail = new MailMessage();
        MailAddress objMailfromaddress = new MailAddress("do-not-reply-umkc-web-email@umkc.edu", "IS Service Orders");
        objMail.From = objMailfromaddress;
        objMail.IsBodyHtml = true;

        string filepath = HttpContext.Current.Server.MapPath("~/EmailNotification.html");
        StreamReader reader = new StreamReader(filepath);
        string readFile = reader.ReadToEnd();
        string StrContent = readFile;
        StrContent = StrContent.Replace("{s_date}", DateTime.Now.ToShortDateString());
        StrContent = StrContent.Replace("{email_summary}", serviceOrder.Count + " Request(s)");
        StrContent = StrContent.Replace("{email_bodycontent}", FormatEmail(serviceOrder));//+

        switch (serverAddress)
        {
            case "net3.umkc.edu":

                objMail.To.Add("rkcq5@mail.umkc.edu");
                StrContent = StrContent.Replace("{email_subject}", "New Service Order submitted");
                objMail.Subject = "New Service Order submitted";
                break;
            case "nettest.umkc.edu":

                objMail.To.Add("brownmau@umkc.edu");
                StrContent = StrContent.Replace("{email_subject}", "New Service Order submitted on Test");
                objMail.Subject = "New Service Order submitted on Test";
                break;
            default:

                objMail.To.Add("rkcq5@mail.umkc.edu");
                objMail.To.Add(Session["EMAIL"].ToString());
                StrContent = StrContent.Replace("{email_subject}", "New Service Order submitted on Dev");
                objMail.Subject = "New Service Order submitted on Dev";
                break;
        }
        objMail.Body = StrContent;
        smtpClient.Host = "massmail.umkc.edu";


        try
        {
            smtpClient.Send(objMail);
            objMail.To.Clear();
            return 1;
        }

        catch (Exception exc)
        {
            return 0;
        }

    }

    private string FormatEmail(ServiceOrder service)
    {
        string cartInformation = "<Table><tr><th colspan=4><h2>Department Information</h2></th></tr>";
        cartInformation += "<tr><td>Department Name:</td><td colspan=2></td><td>" + service.DepartmentName;
        cartInformation += "</td></tr><tr><td>Contact Name:</td><td colspan=2></td><td>" + service.ContactName;
        cartInformation += "</td></tr><tr><td>Contact Email :</td><td colspan=2></td><td>" + service.ContactEmail;
        cartInformation += "</td></tr><tr><td>Contact phone :</td><td colspan=2></td><td>" + service.ContactPhone;
        cartInformation += "</td></tr><tr><td>Authorized Signer Name:</td><td colspan=2></td><td>" + service.AuthorizedSignerName;
        cartInformation += "</td></tr><tr><td>MoCode:</td><td colspan=2></td><td>" + service.MoCode;

        cartInformation += "</td></tr></Table><br>";

        if (service.AnalogServiceList != null)
        {
            foreach (var item in service.AnalogServiceList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.Jack;
                cartInformation += "</td></tr><tr><td>Direct In Dial :</td><td colspan=2></td><td>" + checkValue(aService.DirectInDial);
                cartInformation += "</td></tr><tr><td>Long Distance Direct Dial Dropdown :</td><td colspan=2></td><td>" + checkValue(aService.LongDistanceDirectDial);
                cartInformation += "</td></tr><tr><td>Auth Code :</td><td colspan=2></td><td>" + checkValue(aService.AuthCode);
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";

            }
        }
        if (service.DataServicesList != null)
        {
            foreach (var item in service.DataServicesList)
            {
                int number = item.Key;
                var dService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + dService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + dService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + dService.Room;
                cartInformation += "</td></tr><tr><td>Existing Jack? :<td colspan=2></td><td>" + checkValue(dService.ExistingDataJack);
                if (dService.ExistingDataJack)
                {
                    cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + dService.Jack;
                }
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + dService.Comments;
                cartInformation += "</td></tr></Table><br>";

            }
        }
        if (service.VoiceServicesList != null)
        {
            foreach (var item in service.VoiceServicesList)
            {
                int number = item.Key;
                var vService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + vService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + vService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + vService.Room;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + vService.Jack;
                cartInformation += "</td></tr><tr><td>Phone Type :</td><td colspan=2></td><td>" + vService.PhoneType;
                cartInformation += "</td></tr><tr><td>Direct In Dial :</td><td colspan=2></td><td>" + checkValue(vService.DirectInDial);
                cartInformation += "</td></tr><tr><td>Voicemail Box :</td><td colspan=2></td><td>" + checkValue(vService.VoiceMailBox);
                cartInformation += "</td></tr><tr><td>Long Distance Direct Dial Dropdown :</td><td colspan=2></td><td>" + checkValue(vService.LongDistanceDirectDial);
                cartInformation += "</td></tr><tr><td>Auth Code :</td><td colspan=2></td><td>" + checkValue(vService.AuthCode);
                cartInformation += "</td></tr><tr><td>Call Pickup group :</td><td colspan=2></td><td>" + checkValue(vService.CallPickUpgroup);
                cartInformation += "</td></tr><tr><td>Existing Phone Number :</td><td colspan=2></td><td>" + vService.ExistingPhoneNumber;
                cartInformation += "</td></tr><tr><td>Caller Id :</td><td colspan=2></td><td>" + vService.CallerID;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + vService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }

        if (service.ChangeServicesList != null)
        {
            foreach (var item in service.ChangeServicesList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Phone Number :</td><td colspan=2></td><td>" + aService.PhoneNumber;
                cartInformation += "</td></tr><tr><td>Phone Type :</td><td colspan=2></td><td>" + aService.PhoneType;
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }

        if (service.MoveServicesList != null)
        {
            foreach (var item in service.MoveServicesList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "<tr><td>Moving :</td><td colspan=2></td><td>" + aService.MovingType;
                if (aService.MovingType == "Phone Line")
                {
                    cartInformation += "<tr><td>Phone Type: :</td><td colspan=2></td><td>" + aService.PhoneType;

                }
                cartInformation += "<tr><td>Phone Number :</td><td colspan=2></td><td>" + aService.PhoneNumber;
                cartInformation += "</td></tr><th>Moving From </th>";
                cartInformation += "<tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.FromJack;
                cartInformation += "</td></tr><th>Moving To </th>";
                cartInformation += "<tr><td>Building Name :</td><td colspan=2></td><td>" + aService.ToBuildingName;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.ToRoom;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.ToJack;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }

        if (service.DisconnectServiceList != null)
        {
            foreach (var item in service.DisconnectServiceList)
            {
                int number = item.Key;
                var aService = item.Value;

                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Phone Number :</td><td colspan=2></td><td>" + aService.PhoneNumber;
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.Jack;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }

        if (service.ActivationServicesList != null)
        {
            foreach (var item in service.ActivationServicesList)
            {
                int number = item.Key;
                var aService = item.Value;
                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Existing Jack? :<td colspan=2></td><td>" + checkValue(aService.ExistingDataJack);
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.Jack;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }

        if (service.LaborServicesList != null)
        {
            foreach (var item in service.LaborServicesList)
            {
                int number = item.Key;
                var aService = item.Value;
                cartInformation += "<Table><tr><th colspan=4><h2>" + aService.ServiceName + "</h2></th></tr>";
                cartInformation += "</td></tr><tr><td>Labor Type :</td><td colspan=2></td><td>" + aService.LaborType;
                cartInformation += "</td></tr><tr><td>Building Name :</td><td colspan=2></td><td>" + aService.Building;
                cartInformation += "</td></tr><tr><td>Room :</td><td colspan=2></td><td>" + aService.Room;
                cartInformation += "</td></tr><tr><td>Jack :</td><td colspan=2></td><td>" + aService.Jack;
                cartInformation += "</td></tr><tr><td>Comments :</td><td colspan=2></td><td>" + aService.Comments;
                cartInformation += "</td></tr></Table><br>";
            }
        }
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

    private void InsertToDataBase(ServiceOrder serviceOrder)
    {
        int reqquestId = Service.InsertRequest(serviceOrder, "", "");

        if (serviceOrder.DataServicesList != null)
        {
            foreach (var item in serviceOrder.DataServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[7];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_subtype", 1);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_existingJack", serviceType.ExistingDataJack.ToString());
                parameter[5] = new OracleParameter("p_jack", serviceType.Jack);
                parameter[6] = new OracleParameter("p_comments", serviceType.Comments);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.INSERTDATAREQUEST", CommandType.StoredProcedure, parameter, "", "");
            }
        }

        if (serviceOrder.VoiceServicesList != null)
        {
            foreach (var item in serviceOrder.VoiceServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[15];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_subtype", 2);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_jack", serviceType.Jack);
                parameter[5] = new OracleParameter("p_phonetype", serviceType.PhoneType);
                parameter[6] = new OracleParameter("p_directindial", serviceType.DirectInDial.ToString());
                parameter[7] = new OracleParameter("p_voicemailbox", serviceType.VoiceMailBox.ToString());
                parameter[8] = new OracleParameter("p_voicemailemailalias", serviceType.UserEmailAlias);
                parameter[9] = new OracleParameter("p_longdistancedirectdial", serviceType.LongDistanceDirectDial.ToString());
                parameter[10] = new OracleParameter("p_authcode", serviceType.AuthCode.ToString());
                parameter[11] = new OracleParameter("p_callpickupgroup", serviceType.CallPickUpgroup.ToString());
                parameter[12] = new OracleParameter("p_callpickupphonenumber", serviceType.ExistingPhoneNumber);
                parameter[13] = new OracleParameter("p_comments", serviceType.Comments);
                parameter[14] = new OracleParameter("p_callerid", serviceType.CallerID);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.insertvoicerequest", CommandType.StoredProcedure, parameter, "", "");
            }
        }

        if (serviceOrder.AnalogServiceList != null)
        {
            foreach (var item in serviceOrder.AnalogServiceList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[8];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_subtype", 3);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_jack", serviceType.Jack);
                parameter[5] = new OracleParameter("p_directindial", serviceType.DirectInDial.ToString());
                parameter[6] = new OracleParameter("p_longdistancedirectdial", serviceType.LongDistanceDirectDial.ToString());
                parameter[7] = new OracleParameter("p_comments", serviceType.Comments);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.INSERTANALOGREQUEST", CommandType.StoredProcedure, parameter, "", "");
            }
        }

        if (serviceOrder.ChangeServicesList != null)
        {
            foreach (var item in serviceOrder.ChangeServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[6];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_phonenumber", serviceType.PhoneNumber);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_changerequest", serviceType.Comments);
                parameter[5] = new OracleParameter("p_phonetype", serviceType.PhoneType);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.INSERTCHANGEREQUEST", CommandType.StoredProcedure, parameter, "", "");
            }
        }
        if (serviceOrder.MoveServicesList != null)
        {
            foreach (var item in serviceOrder.MoveServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[11];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_linetype", serviceType.MovingType);
                parameter[2] = new OracleParameter("p_frombuilding", serviceType.Building);
                parameter[3] = new OracleParameter("p_fromroom", serviceType.Room);
                parameter[4] = new OracleParameter("p_fromjack", serviceType.FromJack);
                parameter[5] = new OracleParameter("p_tobuilding", serviceType.ToBuildingName);
                parameter[6] = new OracleParameter("p_toroom", serviceType.ToRoom);
                parameter[7] = new OracleParameter("p_tojack", serviceType.ToJack);
                parameter[8] = new OracleParameter("p_comments", serviceType.Comments);
                parameter[9] = new OracleParameter("p_phonenumber", serviceType.PhoneNumber);
                parameter[10] = new OracleParameter("p_phonetype", serviceType.PhoneType);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.insertmoverequest", CommandType.StoredProcedure, parameter, "", "");
            }
        }
        if (serviceOrder.DisconnectServiceList != null)
        {
            foreach (var item in serviceOrder.DisconnectServiceList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[5];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_phonenumber", serviceType.PhoneNumber);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_comments", serviceType.Comments);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.insertdisconnectrequest", CommandType.StoredProcedure, parameter, "", "");
            }
        }
        if (serviceOrder.ActivationServicesList != null)
        {
            foreach (var item in serviceOrder.ActivationServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[6];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[2] = new OracleParameter("p_room", serviceType.Room);
                parameter[3] = new OracleParameter("p_jack", serviceType.Jack);
                parameter[4] = new OracleParameter("p_existingJack", serviceType.ExistingDataJack.ToString());
                parameter[5] = new OracleParameter("p_comments", serviceType.Comments);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.INSERTACTIVATIONREQUEST", CommandType.StoredProcedure, parameter, "", "");
            }
        }

        if (serviceOrder.LaborServicesList != null)
        {
            foreach (var item in serviceOrder.LaborServicesList)
            {
                var serviceType = item.Value;
                int rtnVal1 = 0;

                OracleParameter[] parameter = new OracleParameter[6];
                parameter[0] = new OracleParameter("p_requestid", reqquestId);
                parameter[1] = new OracleParameter("P_LABORTYPE", serviceType.LaborType);
                parameter[2] = new OracleParameter("p_buildingname", serviceType.Building);
                parameter[3] = new OracleParameter("p_room", serviceType.Room);
                parameter[4] = new OracleParameter("p_jack", serviceType.Jack);
                parameter[5] = new OracleParameter("p_comments", serviceType.Comments);
                rtnVal1 = DbHelper.InsertRequestType("ISNTSERVICEORDER.INSERTLABORREQUEST", CommandType.StoredProcedure, parameter, "", "");
            }
        }
    }
}