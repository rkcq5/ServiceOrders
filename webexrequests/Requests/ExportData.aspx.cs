using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Requests_ExportData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SSOID"] == null)
        {
            Response.Redirect("~/Requests/Default.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string varPageaccessed = HttpContext.Current.Request.Url.ToString();
        string start = txtStartDate.Text;
        DateTime startdate = Convert.ToDateTime(start);
        string end = txtEndDate.Text;
        DateTime enddate = Convert.ToDateTime(end);
        DataTable dtData = DataManagement.getData(startdate, enddate, Session["SSOID"].ToString(), varPageaccessed);
        if (dtData != null)
        {
            ExportToExcel(dtData);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "NoData", "NoDataAlert();", true);
        }

    }
    public void ExportToExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string attachment = "attachment; filename=WebExRequests.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.Flush();
            Response.End();
        }
    }
}