using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for DbHelper
/// </summary>
public class DbHelper
{
    public DbHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string WebExRequestsConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["WebExRequests"].ConnectionString;
        }
    }

    public static OracleConnection WebExRequestsConnection
    {
        get
        {
            return new OracleConnection(WebExRequestsConnectionString);
        }
    }

    public static DataTable GetOracleTables(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {

        DataSet dsTable = new DataSet();
        OracleDataAdapter dataAdapter = new OracleDataAdapter
        {
            SelectCommand = new OracleCommand { CommandText = commandText, CommandType = commandType }
        };

        dataAdapter.SelectCommand.Parameters.Clear();

        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                dataAdapter.SelectCommand.Parameters.Add(parameters[i]);
            }
        }

        OracleConnection connection = WebExRequestsConnection;

        try
        {
            connection.Open();
            dataAdapter.SelectCommand.Connection = connection;
            dataAdapter.Fill(dsTable);
            connection.Close();

            if (dsTable.Tables[0].Rows.Count > 0)
            {
                return dsTable.Tables[0];
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url, commandText);
            errorobj.Logerror();

            return null;
        }

    }
    public static DataTable Checkinstanceavailablility(string varInstance, string ssoid, string url)
    {
        OracleParameter op = null;
        OracleParameter[] oraparameter = new OracleParameter[2];

        op = new OracleParameter("var_instance", OracleDbType.Varchar2);
        op.Direction = ParameterDirection.Input;
        op.Value = varInstance;
        oraparameter[0] = op;



        op = new OracleParameter("var_Results", OracleDbType.RefCursor);
        op.Direction = ParameterDirection.Output;
        oraparameter[1] = op;
        DataTable viewTable = DbHelper.GetOracleTables("ISNTSERVICEORDER.COMMON.checkinstanceavailablility", CommandType.StoredProcedure, oraparameter, ssoid, url);

        return viewTable;
    }

    public static int InsertRequestType(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {

        int rtnVal1 = 0;
        OracleConnection connection = WebExRequestsConnection;
        OracleCommand OraCommand = new OracleCommand(commandText, connection);
        OraCommand.CommandType = commandType;
        
        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                OraCommand.Parameters.Add(parameters[i]);
            }
            try
            {
                connection.Open();
                rtnVal1 = OraCommand.ExecuteNonQuery();
                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
                errorobj.Logerror();
            }
        }

        return 0;
    }

    public static int insertFirstRequest(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {
        int rtnVal1 = 0;
        OracleConnection connection = WebExRequestsConnection;
        OracleCommand OraCommand = new OracleCommand(commandText, connection);
        OraCommand.CommandType = commandType;

        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                OraCommand.Parameters.Add(parameters[i]);
            }
            try
            {
                connection.Open();
                rtnVal1 = OraCommand.ExecuteNonQuery();
                connection.Close();

                return int.Parse(OraCommand.Parameters["req_number"].Value.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
                errorobj.Logerror();
            }
        }

        return 0;
    }



    public static int deleteAdmin(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {
        int rtnVal1 = 0;
        OracleConnection connection = WebExRequestsConnection;
        OracleCommand OraCommand = new OracleCommand(commandText, connection);
        OraCommand.CommandType = commandType;

        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                OraCommand.Parameters.Add(parameters[i]);
            }
            try
            {
                connection.Open();
                rtnVal1 = OraCommand.ExecuteNonQuery();
                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
                errorobj.Logerror();
            }
        }
        return 0;
    }

    public static int ValidateAdmin(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {
        int rtnVal1 = 0;
        OracleConnection connection = WebExRequestsConnection;
        OracleCommand OraCommand = new OracleCommand(commandText, connection);
        OraCommand.CommandType = commandType;

        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                OraCommand.Parameters.Add(parameters[i]);
            }
            try
            {
                connection.Open();
                rtnVal1 = OraCommand.ExecuteNonQuery();
                connection.Close();

                return int.Parse(OraCommand.Parameters["var_result"].Value.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
                errorobj.Logerror();
            }
        }

        return 0;
    }

    public static int DeleteRequest(string commandText, CommandType commandType, OracleParameter[] parameters, string ssoid, string url)
    {
        int rtnVal1 = 0;
        OracleConnection connection = WebExRequestsConnection;
        OracleCommand OraCommand = new OracleCommand(commandText, connection);
        OraCommand.CommandType = commandType;

        if (parameters != null)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                OraCommand.Parameters.Add(parameters[i]);
            }
            try
            {
                connection.Open();
                rtnVal1 = OraCommand.ExecuteNonQuery();
                connection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                ErrorHandler errorobj = new ErrorHandler(ex.ToString(), ssoid, url);
                errorobj.Logerror();
            }
        }

        return 0;
    }
}