#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class Blockage : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long EndPointId { get; set; } = 0;
    public string Reason { get; set; } = "";
    public string QNumber { get; set; } = "";
    public Guid ImageId { get; set; } = Guid.NewGuid();
    public string LengthFromCab { get; set; } = "";
    public string LengthFromToby { get; set; } = "";
    public string Comments { get; set; } = "";
    public bool? Cleared { get; set; } = false;
    public DateTime? StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; } = DateTime.Now;
    public string ClearenceComments { get; set; } = "";

    public long? RegisteredBy { get; set; } = 0;
    public long? ClearedBy { get; set; } = 0;
    public string LocationGps { get; set; } = "";
    public string CableRunId { get; set; } = "";
    public string PointARef { get; set; }
    public string PointAGps { get; set; }
    public string PointBRef { get; set; }
    public string PointBGps { get; set; }
}