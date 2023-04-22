#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Dfe : BusinessEntityBase
{
    public string RemoteTableId { get; set; } = "0";
    public string StreetName { get; set; } = "";
    public Guid DfeId { get; set; } = Guid.NewGuid();
    public string Qnumber { get; set; } = "";
    public string Comments { get; set; } = "";
    public string ToAddress { get; set; } = "";
    public string FromAddress { get; set; } = "";
    public DateTime LastUpdateTime { get; set; }
    public Guid AssignmentId { get; set; } = Guid.Empty;

    [Ignore] public List<ProjectWorks> DfeWorks { get; set; }

    [Ignore] public List<Pictures4Tablet> DfePictures { get; set; }
}