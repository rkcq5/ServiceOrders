using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class ModifyRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string typeOfUser = "";
        string varPageaccessed = this.Request.Url.ToString();
        Session["Req_Num"] = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Request_Number"]));
        if (Session["SSOID"] == null)
        {
            string sso = Request.ServerVariables["HTTP_samAccountName"];
            switch (sso)
            {
                case null:
                    break;
                default:
                    Session["SSOID"] = sso.ToLower();
                    break;
            }
        }

        if (Session["Name"] == null)
        {
            string username = Request.ServerVariables["HTTP_DISPLAYNAME"];
            switch (username)
            {
                case null:
                    break;
                default:
                    Session["Name"] = username;
                    break;
            }
        }

        if (Session["EmplID"] == null)
        {
            string id = Request.ServerVariables["HTTP_EMPLOYEEID"];
            switch (id)
            {
                case null:
                    break;
                default:
                    Session["EmplID"] = id;
                    break;
            }
        }

        DataTable dtstatus = DataManagement.dataMailRequest(Convert.ToInt32(Session["Req_num"]), Session["SSOID"].ToString(), varPageaccessed);
        if (dtstatus != null)
        {
            DataRow dtr = dtstatus.Rows[0];
            string result = dtr["status"].ToString();

            if (result == "Approved" || result == "Denied")
            {
                lblStatus.Text = dtr["FISCAL_OFFICER"].ToString() + " has previously " + dtr["status"].ToString() + " this request on " + dtr["MODIFIED_DATE"].ToString() + ". You cannot modify this request.";
                lblStatus.Visible = true;
                RequestForm.Visible = false;
            }
            else
            {
                if (Session["Req_Num"] != null || Request.QueryString["Action"] != null)
                {
                    if (Request.QueryString["Action"] != null)
                    {
                        string action = Request.QueryString["Action"];
                        typeOfUser = "FiscalOfficer";
                    }
                    else
                    {
                        typeOfUser = "Admin";
                    }

                    int adminResult = DataManagement.IsValidAdmin(Session["ssoid"].ToString(), varPageaccessed);
                    if (adminResult > 0)
                    {
                        typeOfUser = "Admin";
                    }

                    if (typeOfUser == "FiscalOfficer")
                    {
                        txtFirstName.ReadOnly = true;
                        txtLastName.ReadOnly = true;
                        txtUserName.ReadOnly = true;
                        txtFiscalOfficer.ReadOnly = true;
                    }

                    if (!IsPostBack)
                    {
                        if (Session["Req_Num"] != null)
                        {
                            int requestNumber = int.Parse(Session["Req_Num"].ToString());

                            DataTable dtUnapprovedRequest = DataManagement.dataMailRequest(requestNumber, Session["SSOID"].ToString(), varPageaccessed);

                            if (dtUnapprovedRequest != null)
                            {
                                WebExRequest requestDetails = new WebExRequest();

                                DataRow dr = dtUnapprovedRequest.Rows[0];

                                if ((dr["FISCAL_OFFICER"].ToString() == Session["SSOID"].ToString() && typeOfUser == "FiscalOfficer") || typeOfUser == "Admin")
                                {
                                    requestDetails.FirstName = dr["FirstName"].ToString();
                                    txtFirstName.Text = requestDetails.FirstName;

                                    requestDetails.LastName = dr["LastName"].ToString();
                                    txtLastName.Text = requestDetails.LastName;

                                    requestDetails.UserName = dr["SSO_ID"].ToString();
                                    txtUserName.Text = requestDetails.UserName;

                                    requestDetails.FiscalOfficer = dr["FISCAL_OFFICER"].ToString().Trim();
                                    DataTable Result = DataManagement.getSSO(requestDetails.FiscalOfficer.ToUpper(), HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
                                    if (Result != null)
                                    {
                                        txtFiscalOfficer.Text = Result.Rows[0]["name_last"].ToString() + " , " + Result.Rows[0]["name_first"].ToString() + " (" + Result.Rows[0]["SSO_ID"].ToString().ToLower() + ") ";
                                    }


                                    requestDetails.MoCode = dr["MOCODE"].ToString();
                                    txtMoCode.Text = dr["MOCODE"].ToString();

                                    requestDetails.Comments = dr["Comments"].ToString();
                                    txtComments.Text = dr["Comments"].ToString();

                                    requestDetails.SubmittedByEmplId = dr["submitted_by_empid"].ToString();
                                    requestDetails.SubmissionDate = Convert.ToDateTime(dr["submission_date"]);
                                    requestDetails.RequestID = Convert.ToInt32(dr["Request_Number"]);
                                    requestDetails.VersionNumber = Convert.ToInt32(dr["version_number"]);
                                    requestDetails.Status = dr["status"].ToString();
                                    requestDetails.Department = dr["department"].ToString();
                                    requestDetails.User_Email = dr["user_email"].ToString();
                                    Session["ToModifyRequest"] = requestDetails;
                                }
                                else
                                {
                                    lblStatus.Text = "Not Authorized Officer";
                                    lblStatus.Visible = true;
                                    RequestForm.Visible = false;
                                }
                            }
                        }

                    }
                }

                else
                {
                    lblStatus.Text = "Please check the link provided in the mail";
                    lblStatus.Visible = true;
                    RequestForm.Visible = false;
                }
            }
        }
        else
        {
            lblStatus.Text = "Please check the link provided in the mail";
            lblStatus.Visible = true;
            RequestForm.Visible = false;
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        WebExRequest requestDetails = (WebExRequest)Session["ToModifyRequest"];        

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
            FiscalOfficerLastName = FiscalOfficerLastName.First().ToString().ToUpper() + string.Join("", FiscalOfficerLastName.Substring(1));
            requestDetails.FirstName = txtFirstName.Text;
            requestDetails.LastName = txtLastName.Text;
            requestDetails.UserName = txtUserName.Text;
            requestDetails.FiscalOfficer = FiscalOfficerSSO;
            requestDetails.MoCode = txtMoCode.Text;
            requestDetails.Comments = txtComments.Text;

            requestDetails.ModifiedByEmplId = Request.ServerVariables["HTTP_EMPLOYEEID"];
            requestDetails.ModifiedDate = DateTime.Now;
            requestDetails.ModifiedBysso = Request.ServerVariables["HTTP_samAccountName"];
            requestDetails.Status = "Pending";
            
            string OfficerEmail = "";
            DataTable Result = DataManagement.getFiscalOfficer(FiscalOfficerLastName.Trim(), HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
            if (Result != null)
            {
                for (int i = 0; i < Result.Rows.Count; i++)
                {
                    if (Result.Rows[i]["SSO_ID"].ToString().ToLower() == FiscalOfficerSSO.ToLower())
                    {
                        OfficerEmail = Result.Rows[i]["EMAILID"].ToString();
                    }
                }
            }
            int modifyResult = DataManagement.ModifyRequest(requestDetails, Session["SSOID"].ToString(), varPageaccessed);
            
            string Text = "Request has been modified";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "bootbox.alert('" + Text + "', function(result){ window.location.href = 'AdminHome.aspx'})", true);
        }
        else
        {
            string Text = "Please select Fiscal Officer from drop down menu by entering the last name.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "bootbox.alert('" + Text + "', function(result){ window.location.href = 'AdminHome.aspx'})", true);                     
        }
    }

    [WebMethod]
    public static string[] GetCompletionList(string name)
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        name = name.First().ToString().ToUpper() + string.Join("", name.Substring(1));
        DataTable viewResult = DataManagement.getFiscalOfficer(name, "sgdd7", "");

        string[] ssoid = new string[viewResult.Rows.Count];

        for (int i = 0; i < viewResult.Rows.Count; i++)
        {
            ssoid[i] = viewResult.Rows[i]["NAME_LAST"].ToString() + ", " +
                viewResult.Rows[i]["NAME_FIRST"].ToString() + " (" + viewResult.Rows[i]["SSO_ID"].ToString().ToLower() + ")";
        }

       
        return ssoid.ToArray();
    }
    protected void btnModifyandApprove_Click(object sender, EventArgs e)
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        WebExRequest requestDetails = (WebExRequest)Session["ToModifyRequest"];

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
            FiscalOfficerLastName = FiscalOfficerLastName.First().ToString().ToUpper() + string.Join("", FiscalOfficerLastName.Substring(1));
            requestDetails.FirstName = txtFirstName.Text;
            requestDetails.LastName = txtLastName.Text;
            requestDetails.UserName = txtUserName.Text;
            requestDetails.FiscalOfficer = FiscalOfficerSSO;
            requestDetails.MoCode = txtMoCode.Text;
            requestDetails.Comments = txtComments.Text;

            
            requestDetails.ModifiedByEmplId = Request.ServerVariables["HTTP_EMPLOYEEID"];
            requestDetails.ModifiedDate = DateTime.Now;
            requestDetails.ModifiedBysso = Request.ServerVariables["HTTP_samAccountName"];
            requestDetails.Status = "Approved";
           
            string OfficerEmail = "";
            DataTable Result = DataManagement.getFiscalOfficer(FiscalOfficerLastName.Trim(), HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
            if (Result != null)
            {
                for (int i = 0; i < Result.Rows.Count; i++)
                {
                    if (Result.Rows[i]["SSO_ID"].ToString().ToLower() == FiscalOfficerSSO.ToLower())
                    {
                        OfficerEmail = Result.Rows[i]["EMAILID"].ToString();
                    }
                }
            }
            int modifyResult = DataManagement.ModifyRequest(requestDetails, Session["SSOID"].ToString(), varPageaccessed);

            StringBuilder emailbodyUser = new StringBuilder();
            emailbodyUser = emailbodyUser.Append("<font size=2 face=Arial>");
            emailbodyUser = emailbodyUser.Append("<p>Your request has been modified and approved.");
            emailbodyUser = emailbodyUser.Append(" An Information Services staff member will now begin processing your request.");
            emailbodyUser = emailbodyUser.Append(" Please contact webex@umkc.edu if you should have any questions.</p>");
            emailbodyUser = emailbodyUser.Append("<p>" + DisplayRequestDetails(requestDetails) + "<p/>");
            emailbodyUser = emailbodyUser.Append("UMKC Information Services</font>");

            DataManagement.SendEmail(requestDetails.User_Email, "", emailbodyUser.ToString(), "WebExRequest " + requestDetails.RequestID + " - Approved");
            string Text = "Request has been modified and approved. An email has been sent to the submitter.";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "bootbox.alert('" + Text + "', function(result){ window.location.href = 'AdminHome.aspx'})", true);
        }
        else
        {
            string Text = "Please select Fiscal Officer from drop down menu by entering the last name.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "bootbox.alert('" + Text + "', function(result){ window.location.href = 'AdminHome.aspx'})", true);
        }
    }
	
	private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
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