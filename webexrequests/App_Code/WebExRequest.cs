using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Request
/// </summary>
public class WebExRequest
{
    public int RequestID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string FiscalOfficer { get; set; }
    public string MoCode { get; set; }
    public string Comments { get; set; }
    public int VersionNumber { get; set; }
    public string SubmittedByEmplId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }
    public string ModifiedByEmplId { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string DeletedByEmplId { get; set; }
    public DateTime DeletedDate { get; set; }
    public string ModifiedBysso { get; set; }
    public string User_Email { get; set; }
    public string Department { get; set; }
}

public class Admin
{
    public string AdminSSO { get; set; }
    public int AdminEmplId { get; set; }
    public string AddedBySSO { get; set; }
    public string AddedByEmplId { get; set; }
    public DateTime DateAdded { get; set; }
}
