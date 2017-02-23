using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class ActionOnRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Request_Number"] != null)
        {
            Session["Req_Num"] = Decrypt(HttpUtility.UrlDecode(Request.QueryString["Request_Number"]));
        }
        
        if (Session["Req_Num"].ToString() != null && Request.QueryString["Action"] != null)
        {
            string Action = Request.QueryString["Action"];
            int RequestNumber = int.Parse(Session["Req_Num"].ToString());
            string varPageaccessed = this.Request.Url.ToString();

            DataTable dtUnapprovedRequest = DataManagement.dataMailRequest(RequestNumber, "User Not Captured", varPageaccessed);
            string user_email;
            if (dtUnapprovedRequest != null)
            {
                DataRow dr = dtUnapprovedRequest.Rows[0];
                txtFirstName.Text = dr["FirstName"].ToString();
                txtLastName.Text = dr["LastName"].ToString();
                txtUserName.Text = dr["SSO_ID"].ToString();
                txtFiscalOfficer.Text = dr["FISCAL_OFFICER"].ToString();
                txtMoCode.Text = dr["MOCODE"].ToString();
                txtComments.Text = dr["Comments"].ToString();
                Session["SubmittedEmplID"] = dr["submitted_by_empid"];
                Session["SubmittedDate"] = dr["submission_date"];
                Session["VersionNumber"] = dr["version_number"];
                Session["Status"] = dr["status"].ToString();
                user_email = dr["USER_EMAIL"].ToString();
                Session["Department"] = dr["department"].ToString();
                if (dr["status"].ToString() == "Approved" || dr["status"].ToString() == "Denied")
                {
                    lblSuccess.Text = dr["FISCAL_OFFICER"].ToString() + " has previously " + dr["status"].ToString().ToLower() + " this request on " + Convert.ToDateTime(dr["MODIFIED_DATE"].ToString()).ToShortDateString() + ". You cannot " + Action +". ";
                    lblSuccess.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    switch (Action.ToLower())
                    {
                        case "approve":
                            Approve(user_email, RequestNumber);
                            break;
                        case "deny":
                            Deny(user_email, RequestNumber);
                            break;
                        default: break;
                    }
                }
            }
        }
        else
        {
            lblSuccess.Text = "Please check the link provided in the mail";
            lblSuccess.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Approve(string mailrecepient, int RequestNumber)
    {
        string varPageaccessed = this.Request.Url.ToString();
        WebExRequest requestDetails = new WebExRequest();
        requestDetails.FirstName = txtFirstName.Text;
        requestDetails.LastName = txtLastName.Text;
        requestDetails.UserName = txtUserName.Text;
        requestDetails.FiscalOfficer = txtFiscalOfficer.Text;
        requestDetails.MoCode = txtMoCode.Text;
        requestDetails.Comments = txtComments.Text;
        requestDetails.SubmittedByEmplId = Session["SubmittedEmplID"].ToString();
        requestDetails.SubmissionDate = DateTime.Parse(Session["SubmittedDate"].ToString());
        requestDetails.ModifiedByEmplId = "";
        requestDetails.ModifiedDate = DateTime.Now;
        requestDetails.ModifiedBysso = "";
        requestDetails.Status = "Approved";
        requestDetails.VersionNumber = int.Parse(Session["VersionNumber"].ToString());
        requestDetails.RequestID = RequestNumber;
        requestDetails.User_Email = mailrecepient;
        requestDetails.Department = Session["Department"].ToString();
        DataManagement.ModifyRequest(requestDetails, "User Not Captured", varPageaccessed);

        lblSuccess.Text = "Thank you for approving this request";
        lblSuccess.ForeColor = System.Drawing.Color.Green;
        StringBuilder emailbodyUser = new StringBuilder();
        emailbodyUser = emailbodyUser.Append("<font size=2 face=Arial>");
        emailbodyUser = emailbodyUser.Append("<p>Your request has been approved by your fiscal officer.");
        emailbodyUser = emailbodyUser.Append(" An Information Services staff member will now begin processing your request.");
        emailbodyUser = emailbodyUser.Append(" Please contact webex@umkc.edu if you should have any questions.</p>");
        emailbodyUser = emailbodyUser.Append("<p>" + DisplayRequestDetails(requestDetails) + "<p/>");
        emailbodyUser = emailbodyUser.Append("UMKC Information Services</font>"); 
        
        DataManagement.SendEmail(mailrecepient, "", emailbodyUser.ToString(), "WebExRequest " + requestDetails.RequestID + " - Approved");
    }

    protected void Deny(string mailrecepient, int RequestNumber)
    {
        string varPageaccessed = this.Request.Url.ToString();
        WebExRequest requestDetails = new WebExRequest();
        requestDetails.FirstName = txtFirstName.Text;
        requestDetails.LastName = txtLastName.Text;
        requestDetails.UserName = txtUserName.Text;
        requestDetails.FiscalOfficer = txtFiscalOfficer.Text;
        requestDetails.MoCode = txtMoCode.Text;
        requestDetails.Comments = txtComments.Text;
        requestDetails.SubmittedByEmplId = Session["SubmittedEmplID"].ToString();
        requestDetails.SubmissionDate = DateTime.Parse(Session["SubmittedDate"].ToString());
        requestDetails.ModifiedByEmplId = "";
        requestDetails.ModifiedDate = DateTime.Now;
        requestDetails.ModifiedBysso = "";
        requestDetails.Status = "Denied";
        requestDetails.VersionNumber = int.Parse(Session["VersionNumber"].ToString());
        requestDetails.RequestID = RequestNumber;
        requestDetails.User_Email = mailrecepient;
        requestDetails.Department = Session["Department"].ToString();
        DataManagement.ModifyRequest(requestDetails, "User Not Captured", varPageaccessed);
        requestDetails.User_Email = mailrecepient;
        requestDetails.Department = Session["Department"].ToString();

        lblSuccess.Text = "You have denied this request.";
        lblSuccess.ForeColor = System.Drawing.Color.Green;
        StringBuilder emailbodyUser = new StringBuilder();
        emailbodyUser = emailbodyUser.Append("<font size=2 face=Arial>");
        emailbodyUser = emailbodyUser.Append("<p>Your request has been denied by your fiscal officer. Please resubmit your request for the approval.</p>");
        emailbodyUser = emailbodyUser.Append("<p>" + DisplayRequestDetails(requestDetails) + "<p/>");
        emailbodyUser = emailbodyUser.Append("UMKC Information Services" + "</font>");
        
        DataManagement.SendEmail(mailrecepient, "", emailbodyUser.ToString(), "WebExRequest " + requestDetails.RequestID + " - Denied");
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