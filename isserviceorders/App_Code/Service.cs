using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for ServiceItem
/// </summary>
public class Service
{
    public string Building { get; set; }
    public string Room { get; set; }
    public string Comments { get; set; }


    public static DataTable GETBUILDINGS(string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] Oraparameter = new OracleParameter[1];

        op = new OracleParameter("buildings_result", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        Oraparameter[0] = op;


        DataTable resultTable = DbHelper.GetISSOOracleTable("ISNTSERVICEORDER.GETBUILDINGS",
            CommandType.StoredProcedure, Oraparameter, ssoid, url);

        return resultTable;

    }


    public static int InsertRequest(ServiceOrder service, string ssoid, string url)
    {
        int rtnVal1 = 0;
        OracleConnection connection = DbHelper.ISServiceOrdersConnection;
        OracleCommand OraCommand = new OracleCommand("ISNTSERVICEORDER.INSERTREQUEST", connection);
        OraCommand.CommandType = CommandType.StoredProcedure;

        OracleParameter[] parameter = new OracleParameter[7];
        OraCommand.Parameters.Add("p_requestid", OracleDbType.Int32, ParameterDirection.Output);
        OraCommand.Parameters.Add("p_departmentname", service.DepartmentName);
        OraCommand.Parameters.Add("p_contactname", service.ContactName);
        OraCommand.Parameters.Add("p_contactphone", service.ContactPhone);
        OraCommand.Parameters.Add("p_contactemail", service.ContactEmail);
        OraCommand.Parameters.Add("p_authorizedsigner", service.AuthorizedSignerName);
        OraCommand.Parameters.Add("p_mocode", service.MoCode);

        try
        {
            connection.Open();
            rtnVal1 = OraCommand.ExecuteNonQuery();
            connection.Close();
            return Int32.Parse(OraCommand.Parameters["p_requestid"].Value.ToString());
        }
        catch (Exception ex)
        {
            ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
            errorobj.Logerror();
        }

        return 0;
    }
}

public class DataService : Service
{
    public string ServiceName
    {
        get
        {
            return "Data Service";
        }
    }
    public bool ExistingDataJack { get; set; }
    public string Jack { get; set; }
}

public class VoiceService : Service
{
    public string ServiceName
    {
        get
        {
            return "Voice Service";
        }
    }
    public string PhoneType { get; set; }
    public string Jack { get; set; }
    public bool DirectInDial { get; set; }
    public bool LongDistanceDirectDial { get; set; }
    public bool VoiceMailBox { get; set; }
    public string UserEmailAlias { get; set; }
    public bool AuthCode { get; set; }
    public bool CallPickUpgroup { get; set; }
    public string ExistingPhoneNumber { get; set; }
    public string CallerID { get; set; }
}

public class AnalogService : Service
{
    public string ServiceName
    {
        get
        {
            return "Analog Service";
        }
    }
    public string Jack { get; set; }
    public bool DirectInDial { get; set; }
    public bool LongDistanceDirectDial { get; set; }
    public bool AuthCode { get; set; }
}

public class ChangeService : Service
{
    public string ServiceName
    {
        get
        {
            return "Change Service";
        }
    }

    public string PhoneNumber { get; set; }
    public string PhoneType { get; set; }

}

public class MoveService : Service
{
    public string ServiceName
    {
        get
        {
            return "Move Service";
        }
    }
    public string PhoneType { get; set; }
    public string PhoneNumber { get; set; }
    public string MovingType { get; set; }
    public string ToBuildingName { get; set; }
    public string ToRoom { get; set; }
    public string ToJack { get; set; }
    public string FromJack { get; set; }
}

public class DisconnectService : Service
{
    public string ServiceName
    {
        get
        {
            return "Disconnect Service";
        }
    }
    public string PhoneNumber { get; set; }
    public string Jack { get; set; }
}

public class ActivationService:Service
{
    public string ServiceName
    {
        get
        {
            return "Activation Service";
        }
    }
    public bool ExistingDataJack { get; set; }
    public string Jack { get; set; }
}

public class LaborService:Service
{
    public string ServiceName
    {
        get
        {
            return "Labor Service";
        }
    }
    public string LaborType { get; set; }
    public string Jack { get; set; }
}
