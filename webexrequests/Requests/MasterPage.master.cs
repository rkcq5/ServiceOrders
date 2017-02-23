using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string varPageaccessed = this.Request.Url.ToString();
        
        int dtAdmin = DataManagement.IsValidAdmin(Session["ssoid"].ToString(), varPageaccessed);
        if (dtAdmin >= 1)
        {
            AdminButtons.Visible = true;
        }
        else
        {
            AdminButtons.Visible = false;
        }
    }
}
