using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class horizontalmenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string varPageaccessed = this.Request.Url.ToString();
        if (Session["ssoid"] != null)
        {
            lblSSOId.Text = Session["Name"].ToString();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Profile Picture", "loadProfileImage();", true);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}