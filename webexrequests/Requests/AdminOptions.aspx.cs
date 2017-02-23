using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Requests_AdminOptions : System.Web.UI.Page
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
            DataTable dtAdmin = DataManagement.getAdmins(Session["SSOID"].ToString(), varPageaccessed);
            StringBuilder group = new StringBuilder();

            if (dtAdmin != null)
            {
                if (dtAdmin.Select("SSO_ID='" + Session["SSOID"].ToString() + "'").Count() > 0)
                {
                    group.Append("<div>" +
                                 "<table id='myTable' class='table table-striped table-hover'  width='100%'>" +
                                 "<thead style='border-collapse:collapse;text-align:center;'>" +

                                 "<th>SSO</th>" +
                                 "<th>Employee ID</th>" +
                                 "<th>Added Admin SSO</th>" +
                                 "<th>Added Admin Employee ID</th>" +
                                 "<th>Added Date</th>" +
                                 "<th style='text-align:center;'>Actions</th>" +
                                 "</thead>" +
                                 "<tbody id='pendinglist'>");

                    foreach (DataRow dr in dtAdmin.Rows)
                    {
                        group.Append("<tr>");
                        group.Append("<td>").Append(dr["SSO_ID"].ToString()).Append("</td>");
                        group.Append("<td>").Append(dr["EMPLID"].ToString()).Append("</td>");
                        group.Append("<td>").Append(dr["ADDED_BY_SSO"].ToString()).Append("</td>");
                        group.Append("<td>").Append(dr["ADDED_BY_EMPLID"].ToString()).Append("</td>");
                        group.Append("<td>").Append((Convert.ToDateTime(dr["DATE_ADDED"])).ToString("dd-MMM-yy")).Append("</td>");
                        group.Append("<td style='text-align: left'> " +
                           "<a href='#' class='btn-sm btn-danger' onClick='DeleteAdmin(\"" + dr["EMPLID"].ToString() + "\",\"" + dr["SSO_ID"].ToString() + "\")'>Delete Admin</a></td>");
                    }

                    group.Append("</tr>");
                    group.Append("</tbody>");
                    group.Append("</table>");
                    group.Append("</div> <br/> <br/>");

                    divWebExRequests.InnerHtml = group.ToString();
                }
                else
                {
                    Response.Redirect("~/Requests/Default.aspx");
                }
            }
            else
            {
                group.Append("<tr><td><center><h3><b>Currently there are no Active Admins </b><h3><center></td></tr>");
            }
        }
    }

    [WebMethod]
    public static string DeleteAdmin()
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        int emplId = Convert.ToInt32(HttpContext.Current.Request.QueryString["emplId"]);
        int result = DataManagement.DeleteAdmin(emplId, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);

        if (result == 1)
        {
            return "Admin Deleted Succesfully";
        }
        return "Problem Deleting Admin";
    }


    [WebMethod]
    public static string AddAdmin()
    {

        Admin adminDetails = new Admin();
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        adminDetails.AdminSSO = HttpContext.Current.Request.QueryString["SSO"];
        adminDetails.AdminEmplId = Convert.ToInt32(HttpContext.Current.Request.QueryString["EmplId"]);
        adminDetails.AddedBySSO = HttpContext.Current.Session["SSOID"].ToString();
        adminDetails.AddedByEmplId = HttpContext.Current.Session["EmplID"].ToString();
        adminDetails.DateAdded = DateTime.Now;

        int result = DataManagement.AddAdmin(adminDetails, HttpContext.Current.Session["SSOID"].ToString(), varPageaccessed);
        if (result == 1)
        {
            return "Admin Added Succesfully";
        }
        return "Problem Adding Admin";
    }

    //protected void btn_proceed_Click(object sender, EventArgs e)
    //{
    //    string sso = HttpContext.Current.Request.QueryString["sso"];
    //    DataManagement.DeleteAdmin("rhchf", "rkcq5", "");
    //    btn_proceed.visible = false;

    //}
}