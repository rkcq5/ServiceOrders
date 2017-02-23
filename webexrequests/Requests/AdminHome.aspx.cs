using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

public partial class Requests_AdminHome : System.Web.UI.Page
{
    string varPageaccessed = HttpContext.Current.Request.Url.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SSOID"] == null)
        {
              Response.Redirect("~/Requests/Default.aspx");
        }
        else
        {
            DataTable dtRequests = DataManagement.getRequests(HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
            StringBuilder group = new StringBuilder();

            if (dtRequests != null)
            {
                group.Append("<div>" +
                             "<table id='myTable' class='table table-striped table-hover'  width='100%'>" +
                             "<thead style='border-collapse:collapse;text-align:center;'>" +
                              "<th>Request#<br></th>" +
                             "<th>Requestor<br>Name</th>" +
                             "<th>User Name</th>" +
                             "<th>Telephony<br>Privileges</th>" +
                             "<th>Department</th>" +
                             "<th>MoCode</th>" +
                             "<th>Status</th>" +
                             "<th style='text-align:center;'>Actions</th>" +
                              "</thead>" +
                             "<tbody id='pendinglist'>");

                foreach (DataRow dr in dtRequests.Rows)
                {
                    string ModifiedDate = "--";
                    if (dr["MODIFIED_DATE"].ToString() != "")
                    {
                        ModifiedDate = Convert.ToDateTime(dr["MODIFIED_DATE"].ToString()).ToShortDateString();
                    }

                    group.Append("<tr>");
                    group.Append("<td>").Append(dr["request_number"].ToString()).Append("</td>");
                    group.Append("<td>").Append(dr["LASTNAME"].ToString()).Append(" ").Append(dr["FIRSTNAME"].ToString()).Append("</td>");
                    group.Append("<td>").Append(dr["sso_id"].ToString().Split(';')[0]).Append("</td>");
                    if (dr["STATUS"].ToString() == "Aprroved")
                    {
                        group.Append("<td>").Append("Yes").Append("</td>");
                    }
                    else
                    {
                        group.Append("<td>").Append("No").Append("</td>");
                    }
                    group.Append("<td>").Append(dr["Department"].ToString()).Append("</td>");
                    group.Append("<td>").Append(dr["MOCODE"].ToString()).Append("</td>");
                    group.Append("<td>").Append(dr["STATUS"].ToString()).Append("</td>");

                    if(dr["STATUS"].ToString() == "Approved" || dr["STATUS"].ToString() == "Denied")
                    {
                        group.Append(
                       "<td style='text-align: left'> " +
                       "<a href='#' onClick='viewHistory(" + (dr["request_number"].ToString()) + ")'>View</a>" + " | " +
                       "<a href='#' onClick='EditRequest(\"" + dr["SSO_ID"].ToString() + "\",\"" + dr["STATUS"].ToString().ToLower() + "\",\"" + ModifiedDate + "\")'>Edit</a>" + " | " +
                       "<a href='#' onClick='DeleteRequest(" + (dr["request_number"].ToString()) + ")'>Delete</a></td>");
                    }
                    else
                    {
                    group.Append(
                       "<td style='text-align: left'> " +
                       "<a href='#' onClick='viewHistory(" + (dr["request_number"].ToString()) + ")'>View</a>" + " | " +
                       "<a href='/intapps/WebExRequests/Requests/ModifyRequest.aspx?Request_Number=" + HttpUtility.UrlEncode(Encrypt(dr["request_number"].ToString())) + "'>Edit</a>" + " | " +
                       "<a href='#' onClick='DeleteRequest(" + (dr["request_number"].ToString()) + ")'>Delete</a></td>");
                    }

                }
            }
            else
            {
                group.Append("<tr><td><center><h3><b>No Requests from the users</b><h3><center></td></tr>");
            }

            group.Append("</tr>");
            group.Append("</tbody>");
            group.Append("</table>");
            group.Append("</div> <br/> <br/>");

            divWebExRequests.InnerHtml = group.ToString();
        }
    }

    [WebMethod]
    public static string getVersions()
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        int request_number = Convert.ToInt32(HttpContext.Current.Request.QueryString["id"]);

        DataTable dtVersions = DataManagement.getVersions(request_number, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
        WebExRequest webExRequest;
        string versionHistory = "";

        if (dtVersions != null)
        {
            int count1 = 0, count2 =0;
            foreach (DataRow dr in dtVersions.Rows)
            {
                string ModifiedDate = "--";
                if (dr["MODIFIED_DATE"].ToString() != "")
                {
                    ModifiedDate =  Convert.ToDateTime(dr["MODIFIED_DATE"].ToString()).ToShortDateString();
                }
                if (count1 == 0)
                {
                    versionHistory += "<div class='ibox-content'  style='border:none;'><div class='table-responsive'><div class='col-md-12'>";
                versionHistory += "<div class='row' style='background-color:lightgray'><h3><div class='col-md-5'>Details:</h3></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>First Name:</div><div class='col-md-5'>" + dr["firstname"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Last Name:</div><div class='col-md-5'>" + dr["lastname"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>User Name:</div><div class='col-md-5'>" + dr["sso_id"].ToString().Split(';')[0] + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Fiscal Officer:</div><div class='col-md-5'>" + dr["fiscal_officer"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>MoCode:</div><div class='col-md-5'>" + dr["mocode"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Comments:</div><div class='col-md-5'>" + dr["comments"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Status:</div><div class='col-md-5'>" + dr["status"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modified on:</div><div class='col-md-5'>" + ModifiedDate + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modifier Employee Id:</div><div class='col-md-7'>" + dr["MODIFIED_BY_EMPLID"].ToString() + "</div></b></p></div>";
                versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modifier SSO:</div><div class='col-md-5'>" + dr["MODIFIED_BY_SSO"].ToString() + "</div></b></p></div>";
                
                versionHistory += "</div></div></div><br>";
               
                count1++;
                }
                else
                {


                    if (count2 == 0)
                    {
                        versionHistory += "<div class='row' style='border: 0.1px dashed gray'></div>";
                        //versionHistory += "<div class='ibox-content'><div class='table-responsive'><div class='col-md-12'>";                       
                        versionHistory += "<div><h3><b>Change History</b></h3></div>";
                        //versionHistory += "</div></div></div>";
                        count2++;
                    }
                    versionHistory += "<div class='ibox-content' style='border:none;'><div class='table-responsive'><div class='col-md-12'>";
                    versionHistory += "<div class='row' style='background-color:lightgray'><h3><div class='col-md-12'>Version :  " + dr["version_number"].ToString() + "</h3></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>First Name:</div><div class='col-md-5'>" + dr["firstname"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Last Name:</div><div class='col-md-5'>" + dr["lastname"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>User Name:</div><div class='col-md-5'>" + dr["sso_id"].ToString().Split(';')[0] + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Fiscal Officer:</div><div class='col-md-5'>" + dr["fiscal_officer"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>MoCode:</div><div class='col-md-5'>" + dr["mocode"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Comments:</div><div class='col-md-5'>" + dr["comments"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Status:</div><div class='col-md-5'>" + dr["status"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modified on:</div><div class='col-md-5'>" + ModifiedDate + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modifier Employee Id:</div><div class='col-md-7'>" + dr["MODIFIED_BY_EMPLID"].ToString() + "</div></b></p></div>";
                    versionHistory += "<div class='row'><p><b><div class='col-md-5'>Modifier SSO:</div><div class='col-md-5'>" + dr["MODIFIED_BY_SSO"].ToString() + "</div></b></p></div>";
                    versionHistory += "</div></div></div>";
                }
                
            }
        }

        return versionHistory.ToString();
    }

    [WebMethod]
    public static int DeleteRequest()
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        int request_number = Convert.ToInt32(HttpContext.Current.Request.QueryString["id"]);
        string ssoid = HttpContext.Current.Session["SSOID"].ToString();
        int result = DataManagement.DeleteRequest(request_number, ssoid, DateTime.Now, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);

        return result;
    }

    [WebMethod]
    public static void AddRequest()
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        WebExRequest requestDetails = new WebExRequest();
        requestDetails.FirstName = HttpContext.Current.Session["FirstName"].ToString();
        requestDetails.LastName = HttpContext.Current.Session["LastName"].ToString();
        requestDetails.UserName = HttpContext.Current.Session["Name"].ToString();

        //requestDetails.FiscalOfficer = txtFiscalOfficer.Text;
        //requestDetails.MoCode = txtMoCode.Text;
        //requestDetails.Comments = txtComments.Text;


        requestDetails.SubmittedByEmplId = HttpContext.Current.Session["EmplID"].ToString();
        requestDetails.SubmissionDate = DateTime.Now;
        requestDetails.Status = "Pending";
        requestDetails.VersionNumber = 1;
        requestDetails.User_Email = HttpContext.Current.Session["email"].ToString();

        requestDetails.RequestID = DataManagement.addRequest(requestDetails, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
        //string emailbody;
        //emailbody = "<html><head><h3>Hello,</h3></head>";
        //emailbody = emailbody + "<body><p /><font size=2 face=Arial>";
        //emailbody = emailbody + "<b>'" + requestDetails.FirstName + " " + requestDetails.LastName + "' has requested that telephony services be enabled on the following WebEx account: '" + HttpContext.Current.Session["ssoid"];
        //emailbody = emailbody + "'. The user provided the following '" + requestDetails.MoCode + "' as part of this request.Thank you! submitted successfully.</b><p/>";
        //emailbody = emailbody + "<p>Please pick from one of the following options to process this request:</p><ul>";
        //emailbody = emailbody + "<li><a href='https://netdev.umkc.edu/intapps/webexrequests/ActionOnRequest.aspx?Request_Number=" + HttpUtility.UrlEncode(Encrypt(requestDetails.RequestID.ToString())) + "&Action=Approve' >Approve the request</a></li>";
        //emailbody = emailbody + "<li><a href='https://netdev.umkc.edu/intapps/webexrequests/ActionOnRequest.aspx?Request_Number=" + HttpUtility.UrlEncode(Encrypt(requestDetails.RequestID.ToString())) + "&Action=Deny' >Deny the request</a></li>";
        //emailbody = emailbody + "<li><a href='https://netdev.umkc.edu/intapps/webexrequests/Requests/ModifyRequest.aspx?Request_Number=" + HttpUtility.UrlEncode(Encrypt(requestDetails.RequestID.ToString())) + "&Action=Modify' >Modify the request</a></li></ul><br/>";
        //emailbody = emailbody + "UMKC Information Services" + "</font></body>";
        //int a = DataManagement.SendEmail(requestDetails.FiscalOfficer, requestDetails.User_Email, emailbody);

        if (requestDetails.RequestID != 0)
        {
            //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "showAlert(\"Your request has been submitted. A copy of your request has been submitted to the fiscal officer you specified, and your request will be processed by Information Services once we have approval.\")", true);
        }
        else
        {
            // ClientScript.RegisterStartupScript(Page.GetType(), "Message", "showAlert(\"Unable to add the request. Please try again.\")", true);
        }
    }

    [WebMethod]
    public static int ExportData()
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();

        DateTime startDate = Convert.ToDateTime( HttpContext.Current.Request.QueryString["startDate"]);
        DateTime endDate = Convert.ToDateTime(HttpContext.Current.Request.QueryString["endDate"]);
       


        DataTable dtExportData = DataManagement.getData(startDate, endDate, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);

        if (dtExportData != null)
        {
            ExportToExcel(dtExportData);
        }
        return 1;
    }

    public static void ExportToExcel(DataTable dt)
    {
        Page page = HttpContext.Current.Handler as Page;

        string filename = "DownloadMobileNoExcel.xls";
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        DataGrid dgGrid = new DataGrid();
        dgGrid.DataSource = dt;
        dgGrid.DataBind();


        dgGrid.RenderControl(hw);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;

        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        if (page != null)
        {
            page.EnableViewState = false;
        }

        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();

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
}