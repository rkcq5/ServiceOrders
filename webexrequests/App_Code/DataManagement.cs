using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;


public class DataManagement
{
    public DataManagement()
    {
        
    }

    public static int addRequest(WebExRequest requestDetails, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[13];
        op = new OracleParameter("FirstName", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.FirstName;
        oraparameter[0] = op;
        op = new OracleParameter("lastname", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.LastName;
        oraparameter[1] = op;
        op = new OracleParameter("sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.UserName;
        oraparameter[2] = op;
        op = new OracleParameter("fis_officer", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.FiscalOfficer;
        oraparameter[3] = op;
        op = new OracleParameter("mocode", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.MoCode;
        oraparameter[4] = op; op = new OracleParameter("comments", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Comments;
        oraparameter[5] = op; op = new OracleParameter("submitted_empid", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.SubmittedByEmplId;
        oraparameter[6] = op; op = new OracleParameter("submission_date", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.SubmissionDate.ToString("dd-MMM-yy");
        oraparameter[7] = op;
        op = new OracleParameter("status", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Status;
        oraparameter[8] = op;
        op = new OracleParameter("version_number", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.VersionNumber;
        oraparameter[9] = op;
        op = new OracleParameter("req_number", OracleDbType.Int32);
        op.Direction = ParameterDirection.Output;
        oraparameter[10] = op;
        op = new OracleParameter("var_user_email", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.User_Email;
        oraparameter[11] = op;
        op = new OracleParameter("var_department", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Department;
        oraparameter[12] = op;

        int viewResult = DbHelper.insertFirstRequest("WebExRequests.Manage_Requests.insert_request", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static int DeleteAdmin(int EMPLID, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[1];
        op = new OracleParameter("EMPLID", OracleDbType.Int32);
        op.Direction = ParameterDirection.Input;
        op.Value = EMPLID;
        oraparameter[0] = op;
        int viewResult = DbHelper.deleteAdmin("WebExRequests.Manage_Requests.delete_admins", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static DataTable dataMailRequest(int request_Number, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];
        op = new OracleParameter("requestnumber", OracleDbType.Int32);
        op.Direction = ParameterDirection.Input;
        op.Value = request_Number;
        oraparameter[0] = op;
        op = new OracleParameter("c_maildata", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.data_mail_request", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static int ModifyRequest(WebExRequest requestDetails, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[16];
        op = new OracleParameter("FirstName", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.FirstName;
        oraparameter[0] = op;
        op = new OracleParameter("lastname", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.LastName;
        oraparameter[1] = op;
        op = new OracleParameter("sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.UserName;
        oraparameter[2] = op;
        op = new OracleParameter("fis_officer", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.FiscalOfficer;
        oraparameter[3] = op;
        op = new OracleParameter("mocode", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.MoCode;
        oraparameter[4] = op;
        op = new OracleParameter("comments", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Comments;
        oraparameter[5] = op;
        op = new OracleParameter("submitted_empid", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.SubmittedByEmplId;
        oraparameter[6] = op;
        op = new OracleParameter("submission_date", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.SubmissionDate.ToString("dd-MMM-yy");
        oraparameter[7] = op;
        op = new OracleParameter("MODIFIEDBYEMPLID", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.ModifiedByEmplId;
        oraparameter[8] = op;
        op = new OracleParameter("MODIFIEDDATE", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.SubmissionDate.ToString("dd-MMM-yy");
        oraparameter[9] = op;
        op = new OracleParameter("MODIFIEDBYSSO", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.ModifiedBysso;
        oraparameter[10] = op;
        op = new OracleParameter("status", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Status;
        oraparameter[11] = op;
        op = new OracleParameter("version_number", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.VersionNumber;
        oraparameter[12] = op;
        op = new OracleParameter("Request_number", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.RequestID;
        oraparameter[13] = op;
        op = new OracleParameter("var_user_email", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.User_Email;
        oraparameter[14] = op;
        op = new OracleParameter("var_department", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = requestDetails.Department;
        oraparameter[15] = op;


        int viewResult = DbHelper.InsertRequestType("WebExRequests.Manage_Requests.Update_Request", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }


    public static DataTable getRequests(string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[1];

        op = new OracleParameter("c_requesthistory", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[0] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.version_history", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static DataTable getAdmins(string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[1];
        op = new OracleParameter("c_admins", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[0] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.get_admins", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static DataTable getData(DateTime startDate, DateTime endDate, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[3];
        op = new OracleParameter("startdate", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = startDate.ToString("dd-MMM-yy");
        oraparameter[0] = op;
        op = new OracleParameter("enddate", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = endDate.ToString("dd-MMM-yy");
        oraparameter[1] = op;
        op = new OracleParameter("c_exportdata", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[2] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.data_export", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }






    public static int SendEmail(string mailRecipient, string mailRecipientCC, string mailBody, string mailSubject)
    {
        string serverAddress = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
        SmtpClient smtpClient = new SmtpClient();
        MailMessage objMail = new MailMessage();
        MailAddress objMailfromaddress = new MailAddress("do-not-reply-umkc-web-email@umkc.edu", "WebEx Requests");
        objMail.From = objMailfromaddress;
        objMail.IsBodyHtml = true;

        string filepath = HttpContext.Current.Server.MapPath("~/EmailNotification.html");
        StreamReader reader = new StreamReader(filepath);
        string readFile = reader.ReadToEnd();
        string StrContent = readFile;
        StrContent = StrContent.Replace("{s_date}", DateTime.Now.ToShortDateString());
        StrContent = StrContent.Replace("{email_summary}", "");
        StrContent = StrContent.Replace("{email_bodycontent}", mailBody);


        switch (serverAddress)
        {
            case "net3.umkc.edu":

                objMail.To.Add(mailRecipient);

                if (mailRecipientCC != "")
                {
                    objMail.CC.Add(mailRecipientCC);
                }
                StrContent = StrContent.Replace("{email_subject}", "New WebEx Request");
                objMail.Subject = "New WebEx Request";
                break;
            case "nettest.umkc.edu":

                objMail.To.Add(mailRecipient);

                if (mailRecipientCC != "")
                {
                    objMail.CC.Add(mailRecipientCC);
                }
                StrContent = StrContent.Replace("{email_subject}", "New WebEx Request");
                objMail.Subject = "New WebEx Request";
                break;
            default:
                objMail.To.Add(mailRecipient);
                //  objMail.To.Add("rkcq5@mail.umkc.edu");
                //objMail.CC.Add("sgdd7@mail.umkc.edu");
                if (mailRecipientCC != "")
                {
                    objMail.CC.Add(mailRecipientCC);
                }
                StrContent = StrContent.Replace("{email_subject}", "New WebEx Request");
                objMail.Subject = mailSubject;
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

    public static DataTable getVersions(int request_number, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];

        op = new OracleParameter("requestnumber", OracleDbType.Int32);
        op.Direction = ParameterDirection.Input;
        op.Value = request_number;
        oraparameter[0] = op;

        op = new OracleParameter("c_users", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.requests_versions", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static int AddAdmin(Admin adminDetails, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[5];
        op = new OracleParameter("sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = adminDetails.AdminSSO;
        oraparameter[0] = op;
        op = new OracleParameter("empid", OracleDbType.Int32);
        op.Direction = ParameterDirection.Input;
        op.Value = adminDetails.AdminEmplId;
        oraparameter[1] = op;
        op = new OracleParameter("addedbysso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = adminDetails.AddedBySSO;
        oraparameter[2] = op;
        op = new OracleParameter("addedbyempid", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = adminDetails.AddedByEmplId;
        oraparameter[3] = op;
        op = new OracleParameter("dateadded", OracleDbType.Date);
        op.Direction = ParameterDirection.Input;
        op.Value = adminDetails.DateAdded.ToString("dd-MMM-yy");
        oraparameter[4] = op;

        int viewResult = DbHelper.InsertRequestType("WebExRequests.Manage_Requests.add_admins", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static int IsValidAdmin(string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];
        op = new OracleParameter("var_sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = ssoid;
        oraparameter[0] = op;
        op = new OracleParameter("var_result", OracleDbType.Int32);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        int viewResult = DbHelper.ValidateAdmin("WebExRequests.Manage_Requests.admin_check", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static int DeleteRequest(int request_number, string deleted_by_sso, DateTime deleted_date, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[3];

        op = new OracleParameter("requestnumber", OracleDbType.Int32);
        op.Direction = ParameterDirection.Input;
        op.Value = request_number;
        oraparameter[0] = op;

        op = new OracleParameter("deleted_by_sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = deleted_by_sso;
        oraparameter[1] = op;

        op = new OracleParameter("deleted_date", OracleDbType.Date);
        op.Direction = ParameterDirection.Input;
        op.Value = deleted_date;
        oraparameter[2] = op;

        int viewResult = DbHelper.DeleteRequest("WebExRequests.Manage_Requests.delete_request", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }

    public static DataTable getFiscalOfficer(string lastName, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];
        op = new OracleParameter("last_name", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = lastName;
        oraparameter[0] = op;
        op = new OracleParameter("c_fiscal_officer", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.get_fiscal_officer", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }
    public static DataTable getSSO(string sso, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];
        op = new OracleParameter("var_sso", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = sso;
        oraparameter[0] = op;
        op = new OracleParameter("c_getsso", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        DataTable viewResult = DbHelper.GetOracleTables("WebExRequests.Manage_Requests.get_sso", CommandType.StoredProcedure, oraparameter, ssoid, url);
        return viewResult;
    }
}