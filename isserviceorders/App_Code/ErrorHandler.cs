using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for ErrorHandler
/// </summary>
public class ErrorHandler
{
    private string _code;
    private string _ipaddress;
    private string _browser;
    private string _referrer;
    private string _userSso;
    private string _error;
    private string _url;
    private string _procedurename;


    // Constructor 

    public ErrorHandler(string error = null,
                        string userSso = null,
                        string url = null,
                        string procedurename = null
                      )
    {
        _code = "ISSERVICEORDERS";
        _ipaddress = GetLanIpAddress();
        _browser = GetWebBrowserName();
        _referrer = "ASP.Net";
        _error = error;
        _userSso = userSso;
        _url = url;
        _procedurename = procedurename;
    }


    public void Logerror()
    {
        string errormessage = "URL: " + _url +
                              "<br/> Procedure Name:" + _procedurename +
                              "<br/> Error Message: " + _error;


        OracleConnection connection = DbHelper.ISServiceOrdersConnection;
        OracleCommand oraCommand = new OracleCommand("ISNTSERVICEORDER.COMMON.ErrorHandler", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        oraCommand.Parameters.Add("Code", _code);
        oraCommand.Parameters.Add("IPAddress", _ipaddress);
        oraCommand.Parameters.Add("Browser", _browser);
        oraCommand.Parameters.Add("Referrer", _referrer);
        oraCommand.Parameters.Add("User", _userSso);
        oraCommand.Parameters.Add("Process", DBNull.Value);
        oraCommand.Parameters.Add("Error", errormessage);


        try
        {
            connection.Open();
            oraCommand.ExecuteNonQuery();
            connection.Close();


        }
        catch (Exception ex1)
        {
            string serverAddress = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            SmtpClient smtpClient = new SmtpClient();
            MailMessage objMail = new MailMessage();
            MailAddress objMailFromaddress = new MailAddress("do-not-reply-umkc-web-email@umkc.edu", "UMKC ");
            objMail.From = objMailFromaddress;

            objMail.Subject = "Error Handler didnt work";

            objMail.To.Add("vallambhatlak@umkc.edu");

            objMail.IsBodyHtml = true;
            objMail.Body = "<html><head><title>";
            objMail.Body = objMail.Body + " <h3>Application Error</h3>";
            objMail.Body = objMail.Body + "</title>";
            objMail.Body = objMail.Body + "</head><body>";

            objMail.Body = objMail.Body + "<b>" + "Error" + "</b><br/>";
            objMail.Body = objMail.Body + ex1.Message + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "Code" + "</b><br/>";
            objMail.Body = objMail.Body + _code + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "Browser" + "</b><br/>";
            objMail.Body = objMail.Body + _browser + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "Referrer" + "</b><br/>";
            objMail.Body = objMail.Body + _referrer + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "User" + "</b><br/>";
            objMail.Body = objMail.Body + _userSso + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "Process" + "</b><br/>";
            objMail.Body = objMail.Body + "" + "<br/>";

            objMail.Body = objMail.Body + "<b>" + "Error" + "</b><br/>";
            objMail.Body = objMail.Body + errormessage + "<br/>";



            objMail.Body = objMail.Body + "<b>" + System.DateTime.Now.ToString(CultureInfo.InvariantCulture) + "</b><br/>";
            objMail.Body = objMail.Body + "</body>";
            smtpClient.Host = "massmail.umkc.edu";
            smtpClient.Send(objMail);

        }

    }

    public static String GetLanIpAddress()
    {
        //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a 
        //client connecting to a web server through an HTTP proxy or load balancer
        String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ip))
        {
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        return ip;
    }

    public static string GetWebBrowserName()
    {
        string webBrowserName;
        try
        {
            webBrowserName = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return webBrowserName;
    }
}