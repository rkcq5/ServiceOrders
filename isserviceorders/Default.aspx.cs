using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;

public partial class Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SSOID"] != null)
        {
            LoadBuildings();
        }
        else
        {
            Authenticate();
            LoadBuildings();
        }
    }

    private void LoadBuildings()
    {
        if (!IsPostBack)
        {
            string varPageaccessed = this.Request.Url.ToString();
            DataTable dtbuildings = Service.GETBUILDINGS(Session["SSOID"].ToString(), varPageaccessed);

            if (dtbuildings != null)
            {
                ddlbuildingName.DataSource = dtbuildings;
                ddlbuildingName.DataTextField = "BuildingName";
                ddlbuildingName.DataValueField = "BuildingName";
                ddlbuildingName.DataBind();
                ddlToBuildingName.DataSource = dtbuildings;
                ddlToBuildingName.DataTextField = "BuildingName";
                ddlToBuildingName.DataValueField = "BuildingName";
                ddlToBuildingName.DataBind();
            }
        }
    }

    protected void btnaddToCart_Click(object sender, EventArgs e)
    {
        ServiceOrder serviceOrder;

        int requestCount;
        int HighestKey;
        if (Session["ServiceOrder"] == null)
        {
            serviceOrder = new ServiceOrder();
            requestCount = 0;
            HighestKey = 0;
        }
        else
        {
            serviceOrder = (ServiceOrder)Session["ServiceOrder"];
            requestCount = serviceOrder.Count;
            HighestKey = serviceOrder.HighestKey;
        }

        switch (ddlService.SelectedValue)
        {
            case "Add":

                switch (ddladdSubService.SelectedValue)
                {
                    case "Data":
                        requestCount++;
                        HighestKey++;
                        DataService dataService = new DataService();
                        if (serviceOrder.DataServicesList == null)
                        {
                            serviceOrder.DataServicesList = new Dictionary<int, DataService>();
                        }
                        dataService.Building = ddlbuildingName.SelectedValue;
                        dataService.Room = txtroomNumber.Text;
                        if (ddlExistingJack.SelectedValue == "True")
                        {
                            dataService.ExistingDataJack = true;
                            dataService.Jack = txtDataJackOnly.Text;
                        }
                        else
                        {
                            dataService.ExistingDataJack = false;
                            dataService.Jack = txtjack.Text;
                        }
                        dataService.Comments = txtcomments.Text;
                        //serviceOrder.ServiceDictionary.Add(count, dataService);
                        serviceOrder.DataServicesList.Add(HighestKey, dataService);
                        break;

                    case "Voice":
                        requestCount++;
                        HighestKey++;
                        VoiceService voiceService = new VoiceService();
                        if (serviceOrder.VoiceServicesList == null)
                        {
                            serviceOrder.VoiceServicesList = new Dictionary<int, VoiceService>();
                        }
                        voiceService.Building = ddlbuildingName.SelectedValue;
                        voiceService.Room = txtroomNumber.Text;
                        if (txtjack.Text.Length != 0)
                        {
                            voiceService.Jack = txtjack.Text;
                        }
                        voiceService.PhoneType = ddlphoneType.SelectedValue;

                        if (ddldirectInDial.SelectedValue == "True")
                        {
                            voiceService.DirectInDial = true;
                        }
                        else
                        {
                            voiceService.DirectInDial = false;
                        }
                        if (ddlVoiceMailBox.SelectedValue == "True")
                        {
                            voiceService.VoiceMailBox = true;
                            voiceService.UserEmailAlias = txtUserEmailAlias.Text;
                        }
                        else
                        {
                            voiceService.VoiceMailBox = false;
                        }
                        if (ddllongDistanceDial.SelectedValue == "True")
                        {
                            voiceService.LongDistanceDirectDial = true;
                            if (ddlAuthCode.SelectedValue == "True")
                            {
                                voiceService.AuthCode = true;
                            }
                            else
                            {
                                voiceService.AuthCode = false;
                            }
                        }
                        else
                        {
                            voiceService.LongDistanceDirectDial = false;
                            voiceService.AuthCode = false;
                        }

                        if (ddlCallPickUpGroup.SelectedValue == "True")
                        {
                            voiceService.CallPickUpgroup = true;
                            voiceService.ExistingPhoneNumber = txtCallGroupPhoneNumber.Text;
                        }
                        else
                        {
                            voiceService.CallPickUpgroup = false;
                        }
                        voiceService.CallerID = txtCallerId.Text;
                        voiceService.Comments = txtcomments.Text;
                        //serviceOrder.ServiceDictionary.Add(count, voiceService);
                        serviceOrder.VoiceServicesList.Add(HighestKey, voiceService);
                        break;

                    case "Analog":
                        requestCount++;
                        HighestKey++;
                        AnalogService analogService = new AnalogService();
                        if (serviceOrder.AnalogServiceList == null)
                        {
                            serviceOrder.AnalogServiceList = new Dictionary<int, AnalogService>();
                        }
                        analogService.Building = ddlbuildingName.SelectedValue;
                        analogService.Room = txtroomNumber.Text;
                        if (txtjack.Text.Length != 0)
                        {
                            analogService.Jack = txtjack.Text;
                        }
                        if (ddldirectInDial.SelectedValue == "True")
                        {
                            analogService.DirectInDial = true;
                        }
                        else
                        {
                            analogService.DirectInDial = false;
                        }
                        if (ddllongDistanceDial.SelectedValue == "True")
                        {
                            analogService.LongDistanceDirectDial = true;
                            if (ddllongDistanceDial.SelectedValue == "True")
                                analogService.AuthCode = true;
                        }
                        else
                        {
                            analogService.LongDistanceDirectDial = false;
                            analogService.AuthCode = false;
                        }
                        analogService.Comments = txtcomments.Text;
                        //serviceOrder.ServiceDictionary.Add(count, analogService);
                        serviceOrder.AnalogServiceList.Add(HighestKey, analogService);
                        break;
                }
                break;

            case "Change":
                requestCount++;
                HighestKey++;
                ChangeService changeService = new ChangeService();
                if (serviceOrder.ChangeServicesList == null)
                {
                    serviceOrder.ChangeServicesList = new Dictionary<int, ChangeService>();
                }
                changeService.PhoneNumber = txtPhoneNumber.Text;
                changeService.Building = ddlbuildingName.SelectedValue;
                changeService.Room = txtroomNumber.Text;
                changeService.Comments = txtcomments.Text;
                changeService.PhoneType = ddlphoneType.SelectedItem.Value.ToString();

                //serviceOrder.ServiceDictionary.Add(count, changeService);
                serviceOrder.ChangeServicesList.Add(HighestKey, changeService);
                break;

            case "Move":
                requestCount++;
                HighestKey++;
                MoveService moveService = new MoveService();
                if (serviceOrder.MoveServicesList == null)
                {
                    serviceOrder.MoveServicesList = new Dictionary<int, MoveService>();
                }
                moveService.MovingType = ddlMovingType.SelectedValue;
                if (moveService.MovingType == "Phone Line")
                {
                    moveService.PhoneType = ddlphoneType.SelectedValue;
                }
                moveService.PhoneNumber = txtPhoneNumber.Text;
                moveService.Building = ddlbuildingName.SelectedValue;
                moveService.Room = txtroomNumber.Text;
                if (txtjack.Text.Length != 0)
                {
                    moveService.FromJack = txtjack.Text;
                }
                moveService.ToBuildingName = ddlToBuildingName.SelectedValue;
                moveService.ToRoom = txtMovingToRoom.Text;
                if (txtjack.Text.Length != 0)
                {
                    moveService.ToJack = txtMovingToJack.Text;
                }
                moveService.Comments = txtcomments.Text;
                //serviceOrder.ServiceDictionary.Add(count, moveService);
                serviceOrder.MoveServicesList.Add(HighestKey, moveService);
                break;

            case "Disconnect":
                requestCount++;
                HighestKey++;
                DisconnectService disconnectService = new DisconnectService();
                if (serviceOrder.DisconnectServiceList == null)
                {
                    serviceOrder.DisconnectServiceList = new Dictionary<int, DisconnectService>();
                }
                disconnectService.PhoneNumber = txtPhoneNumber.Text;
                disconnectService.Building = ddlbuildingName.SelectedValue;
                disconnectService.Room = txtroomNumber.Text;
                if (txtjack.Text.Length != 0)
                {
                    disconnectService.Jack = txtjack.Text;
                }
                disconnectService.Comments = txtcomments.Text;
                // serviceOrder.ServiceDictionary.Add(count, disconnectService);
                serviceOrder.DisconnectServiceList.Add(HighestKey, disconnectService);
                break;

            case "Activation":
                requestCount++;
                HighestKey++;
                ActivationService activationService = new ActivationService();
                if (serviceOrder.ActivationServicesList == null)
                {
                    serviceOrder.ActivationServicesList = new Dictionary<int, ActivationService>();
                }
                activationService.Building = ddlbuildingName.SelectedValue;
                activationService.Room = txtroomNumber.Text;
                activationService.ExistingDataJack = true;
                if (txtjack.Text.Length != 0)
                {
                    activationService.Jack = txtjack.Text;
                }
                activationService.Comments = txtcomments.Text;

                serviceOrder.ActivationServicesList.Add(HighestKey, activationService);
                break;

            case "Labor":
                requestCount++;
                HighestKey++;
                LaborService laborService = new LaborService();
                if (serviceOrder.LaborServicesList == null)
                {
                    serviceOrder.LaborServicesList = new Dictionary<int, LaborService>();
                }
                laborService.Building = ddlbuildingName.SelectedValue;
                laborService.LaborType = ddlLaborType.SelectedValue;
                laborService.Room = txtroomNumber.Text;

                if (txtjack.Text.Length != 0)
                {
                    laborService.Jack = txtjack.Text;
                }
                laborService.Comments = txtcomments.Text;

                serviceOrder.LaborServicesList.Add(HighestKey, laborService);
                break;
        }
        serviceOrder.Count = requestCount;
        serviceOrder.HighestKey = HighestKey;
        Session["ServiceOrder"] = serviceOrder;
        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "showAlert(\"Items in your Cart : <b>" + serviceOrder.Count + "</b>\")", true);
    }

    protected void Authenticate()
    {
        string name = Request.ServerVariables["HTTP_DISPLAYNAME"];
        switch (name)
        {
            case null:

                break;
            default:

                Session["Name"] = name;
                break;
        }
        string id = Request.ServerVariables["HTTP_EMPLOYEEID"];
        switch (id)
        {
            case null:

                break;
            default:

                Session["Emplid"] = id;
                break;
        }

        string sso = Request.ServerVariables["HTTP_samAccountName"];
        sso = "rkcq5";
        switch (sso)
        {
            case null:

                break;
            default:

                Session["SSOID"] = sso.ToLower();
                break;
        }
        string email = Request.ServerVariables["HTTP_emailAddress"];
        switch (email)
        {
            case null:

                break;
            default:

                Session["EMAIL"] = email.ToLower();
                break;
        }

    }

    protected void btnModalViewCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ViewCart.aspx");
    }
}