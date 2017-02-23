using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Data;
using System.Web.Services;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string varPageaccessed = this.Request.Url.ToString();

        if (Session["SSOID"] == null)
        {
            Authenticate();
        }

        if (!IsPostBack)
        {
            if (IsValidAdmin() > 0)
            {
                Session["isValidAdmin"] = "True";
            }

            txtFirstName.Text = Session["FirstName"].ToString();
            txtLastName.Text = Session["LastName"].ToString();
            txtUserName.Text = Session["SSOID"].ToString();
        }
    }

    protected int IsValidAdmin()
    {
        string varPageaccessed = this.Request.Url.ToString();
        int adminResult = DataManagement.IsValidAdmin(Session["ssoid"].ToString(), varPageaccessed);

        return adminResult;
    }

    protected void Authenticate()
    {
        string username = Request.ServerVariables["HTTP_DISPLAYNAME"];
        username = "raviteja reddy";
        // username = "Dinesh reddy";
        switch (username)
        {
            case null:
                break;
            default:
                Session["Name"] = username;
                break;
        }

        string firstname = Request.ServerVariables["HTTP_givenName"];
        firstname = "ravi";
        //firstname = "Dinesh";
        switch (firstname)
        {
            case null:
                break;
            default:
                Session["FirstName"] = firstname;
                break;
        }

        string lastname = Request.ServerVariables["HTTP_sn"];

        lastname = "teja";
        // lastname = "Reddy";
        switch (lastname)
        {
            case null:
                break;
            default:
                Session["LastName"] = lastname;
                break;
        }

        string id = Request.ServerVariables["HTTP_EMPLOYEEID"];
        id = "16208639";
        //  id = "16208573";
        switch (id)
        {
            case null:
                break;
            default:
                Session["EmplID"] = id;
                break;
        }

        string sso = Request.ServerVariables["HTTP_samAccountName"];
        sso = "rkcq5";
        //sso = "dpq53";
        switch (sso)
        {
            case null:
                break;
            default:
                Session["SSOID"] = sso.ToLower();
                break;
        }

        string email = Request.ServerVariables["HTTP_emailAddress"];
        email = "rkcq5@mail.umkc.edu";
        //email = "dpq53@mail.umkc.edu";
        switch (email)
        {
            case null:

                break;
            default:

                Session["EMAIL"] = email.ToLower();
                break;
        }

        string department = Request.ServerVariables["HTTP_department"];
        department = "CS";
        switch (department)
        {
            case null:

                break;
            default:

                Session["UserDepartment"] = department;
                break;
        }
    }

    [WebMethod]
    public static string[] GetCompletionList(string name)
    {
        string fiscalOfficer = name.ToLower();
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        fiscalOfficer = fiscalOfficer.First().ToString().ToUpper() + string.Join("", fiscalOfficer.Substring(1));
        DataTable viewResult = DataManagement.getFiscalOfficer(fiscalOfficer, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
        string[] ssoid = new string[viewResult.Rows.Count];
        for (int i = 0; i < viewResult.Rows.Count; i++)
        {
            ssoid[i] = viewResult.Rows[i]["NAME_LAST"].ToString() + ", " +
                viewResult.Rows[i]["NAME_FIRST"].ToString() + " (" + viewResult.Rows[i]["SSO_ID"].ToString().ToLower() + ")";
        }
        return ssoid.ToArray();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string varPageaccessed = this.Request.Url.ToString();
        WebExRequest requestDetails = new WebExRequest();

        string FiscalOfficerSSO = "";
        string FiscalOfficerLastName = "";

        if (txtFiscalOfficer.Text.Contains("(") && txtFiscalOfficer.Text.Contains(")"))
        {
            FiscalOfficerSSO = txtFiscalOfficer.Text.Split('(', ')')[1];
        }

        if (txtFiscalOfficer.Text.Contains(","))
        {
            FiscalOfficerLastName = txtFiscalOfficer.Text.Split(',')[0];
        }

        if (FiscalOfficerSSO != "" && FiscalOfficerLastName != "")
        {
            requestDetails.FirstName = txtFirstName.Text;
            requestDetails.LastName = txtLastName.Text;
            requestDetails.UserName = Session["SSOID"].ToString(); // To be changed
            requestDetails.FiscalOfficer = FiscalOfficerSSO;
            requestDetails.MoCode = txtMoCode.Text;
            requestDetails.Comments = txtComments.Text;

            requestDetails.SubmittedByEmplId = Session["EmplID"].ToString();

            requestDetails.SubmissionDate = DateTime.Today;
            requestDetails.Status = "Pending";
            requestDetails.VersionNumber = 1;
            requestDetails.User_Email = Session["EMAIL"].ToString();
            requestDetails.Department = Session["UserDepartment"].ToString();

            requestDetails.RequestID = DataManagement.addRequest(requestDetails, Session["SSOID"].ToString(), varPageaccessed);

            DataTable Result = DataManagement.getFiscalOfficer(FiscalOfficerLastName, Session["SSOID"].ToString(), varPageaccessed);
            string OfficerEmail = "";
            if (Result != null)
            {
                for (int i = 0; i < Result.Rows.Count; i++)
                {
                    if (Result.Rows[i]["SSO_ID"].ToString().ToLower() == FiscalOfficerSSO)
                    {
                        OfficerEmail = Result.Rows[i]["EMAILID"].ToString();
                    }
                }
            }
            string RequestID = HttpUtility.UrlEncode(Encrypt(requestDetails.RequestID.ToString()));
            StringBuilder emailbody = new StringBuilder();
            emailbody = emailbody.Append("<font size=2 face=Arial>");
            emailbody = emailbody.Append("<p><b>Hello, <br/>" + requestDetails.FirstName + " " + requestDetails.LastName + " has requested that telephony services be enabled on the following WebEx account: '" + Session["ssoid"]);
            emailbody = emailbody.Append("'. The user provided the following MoCode as part of this request: " + requestDetails.MoCode + ".</b><p/>");
            emailbody = emailbody.Append("<p>Please pick from one of the following options to process this request:</p><ul>");
            emailbody = emailbody.Append("<li><a href='https://netdev.umkc.edu/intapps/webexrequests/ActionOnRequest.aspx?Request_Number=" + RequestID + "&Action=Approve' >Approve the request</a></li>");
            emailbody = emailbody.Append("<li><a href='https://netdev.umkc.edu/intapps/webexrequests/ActionOnRequest.aspx?Request_Number=" + RequestID + "&Action=Deny' >Deny the request</a></li>");
            emailbody = emailbody.Append("<li><a href='https://netdev.umkc.edu/intapps/webexrequests/Requests/ModifyRequest.aspx?Request_Number=" + RequestID + "&Action=Modify' >Modify the request</a></li></ul><br/>");
            emailbody = emailbody.Append("UMKC Information Services" + "</font></body>");
            int mailResult = DataManagement.SendEmail(OfficerEmail, "", emailbody.ToString(), "New WebEx Request");

            StringBuilder emailbodyUser = new StringBuilder();
            emailbodyUser = emailbodyUser.Append("<font size=2 face=Arial>");
            emailbodyUser = emailbodyUser.Append("<p><b>Hello, <br/>Your information has been submitted. <br/>");
            emailbodyUser = emailbodyUser.Append("An email has been sent to the fiscal officer you specified. Your request will be processed by Information Services once we have approval from your fiscal officer.</b></p>");
            emailbodyUser = emailbodyUser.Append("<p>" + DisplayRequestDetails(requestDetails) + "<p/>");
            emailbodyUser = emailbodyUser.Append("UMKC Information Services" + "</font>");
            int mailResultUser = DataManagement.SendEmail(requestDetails.User_Email, "", emailbodyUser.ToString(), "New WebEx Request");
            if (requestDetails.RequestID != 0 && mailResult == 1)
            {

                string text = "Your information has been submitted and an email has been sent to the fiscal officer you specified." +
                              "Your request will be processed by Information Services once we have approval from your fiscal officer.";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Submission Alert", "showAlert('" + text + "');", true);
            }
            else
            {
                string text = "Unable to add the request. Please try again.";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Submission Alert", "showAlert('" + text + "');", true);
            }
        }

        else
        {
            string text = "Please select Fiscal Officer from drop down menu by entering the last name.";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Submission Alert", "FiscalOfficerAlert('" + text + "');", true);
        }

    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    protected static string DisplayRequestDetails(WebExRequest Requestdetails)
    {

        StringBuilder MailBody = new System.Text.StringBuilder();

        MailBody.Append("</br><b>REQUEST DETAILS:</b>");

        MailBody.Append("<table class='bodyContent'>");

        MailBody.Append("<tr><td>Request Number</td>");
        MailBody.Append("<td>" + Requestdetails.RequestID + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>First Name</td>");
        MailBody.Append("<td>" + Requestdetails.FirstName + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>Last Name</td>");
        MailBody.Append("<td>" + Requestdetails.LastName + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>Fiscal Officer</td>");
        MailBody.Append("<td>" + Requestdetails.FiscalOfficer + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>MoCode</td>");
        MailBody.Append("<td>" + Requestdetails.MoCode + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>Comments</td>");
        MailBody.Append("<td>" + Requestdetails.Comments + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("<tr><td>Submitted On</td>");
        MailBody.Append("<td>" + Requestdetails.SubmissionDate.ToShortDateString() + "</td>");
        MailBody.Append("</tr>");

        MailBody.Append("</table>");

        return MailBody.ToString();
    }
}