using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServiceOrder
/// </summary>
public class ServiceOrder
{
    public string DepartmentName { get; set; }
    public string ContactName { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string MoCode { get; set; }
    public string AuthorizedSignerName { get; set; }
    public int Count { get; set; }
    public int HighestKey { get; set; }
    //public Dictionary<Int32, object> ServiceDictionary = new Dictionary<Int32, object>();
    public Dictionary<int, DataService> DataServicesList { get; set; }
    public Dictionary<int, AnalogService> AnalogServiceList { get; set; }
    public Dictionary<int, ChangeService> ChangeServicesList { get; set; }
    public Dictionary<int, MoveService> MoveServicesList { get; set; }
    public Dictionary<int, DisconnectService> DisconnectServiceList { get; set; }
    public Dictionary<int, VoiceService> VoiceServicesList { get; set; }
    public Dictionary<int, ActivationService> ActivationServicesList { get; set; }
    public Dictionary<int, LaborService> LaborServicesList { get; set; }
}