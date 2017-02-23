using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class horizontalmenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSSOId.Text = Session["SSOID"].ToString();
    }

    public void EnableName()
    {

    }
}