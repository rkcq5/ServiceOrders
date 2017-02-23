using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class verticalmenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ssoid"] != null)
        {
            string varPageaccessed = this.Request.Url.ToString();


            Label1.Text = Session["Name"].ToString();
            Literal1.Text = "  <i class='fa fa-sign-out fa-lg' " +
                           "title='Logout " + Session["Name"].ToString() + "'></i>";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "loadProfileImage()", true);

            int dtAdmin = DataManagement.IsValidAdmin(Session["ssoid"].ToString(), varPageaccessed);


            if (dtAdmin >= 1)
            {
                liManageAdmins.Visible = true;
                liRequests.Visible = true;
            }
            else
            {
                liManageAdmins.Visible = false;
                liRequests.Visible = false;
                liAddRequest.Visible = false;
                liExportData.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Requests/Default.aspx");
        }
    }
}