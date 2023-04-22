#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Investigation : BusinessEntityBase
{
    //[PrimaryKey, AutoIncrement, JsonIgnore]
    // public int id { get; set; }

    //public event PropertyChangedEventHandler PropertyChanged;
    private string _surfaceType;

    public long RemoteTabledId { get; set; } = 0;

    //  public Guid PublicUtilityDamageGuid { get; set; }

    //[JsonIgnore]
    //public string InvestigationJsonString { get; set; } = "";

    public string DamageId { get; set; } = "";

    //[Ignore]
    //public string StrikeClassification { get; set; } = "";

    //[Ignore]
    //public bool? PrintsProvided { get; set; } = false;

    //[Ignore]
    //public bool? AdequateInformation { get; set; } = false;

    //[Ignore]
    //public bool? PrintsInColour { get; set; } = false;

    //[Ignore]
    public string SurfaceMaterial { get; set; } = "";

    //[Ignore]
    //public string DamageTypeString { get; set; } = "";

    //[Ignore]
    public string ContractReference { get; set; } = "";

    //[Ignore]
    //  public string EventsLeading { get; set; } = "";

    //[Ignore]
    //  public string ImmediateAction { get; set; } = "";

    public string InvestigationSubmitType { get; set; } = "";

    //[Ignore]
    public string SurfaceType { get; set; } = "";

    //[JsonIgnore]
    public bool IsUpdatedToServer { get; set; } = false;

    //[JsonIgnore]
    public bool IsCompleted { get; set; } = false;

    //[Ignore]
    public string SignatureId { get; set; } = "";

    //[Ignore]
    public string OperativeId { get; set; } = "";

    //[Ignore]
    //  public string ImmediateCause { get; set; } = "";

    //[Ignore]
    // public DateTime? RiddorDate { get; set; } = DateTime.Now;

    //[Ignore]
    // public TimeSpan RiddorTime { get; set; } = DateTime.Now.TimeOfDay;

    //[Ignore]
    public DateTime InvestigationDate { get; set; } = DateTime.Now;


    [Ignore] public List<InvestigationAnswer> InvestigationAnswers { get; set; } = new();


    //[Ignore] public List<InvestigationQuestion> InvestigationQuestion { get; set; } = new List<InvestigationQuestion>();


    [Ignore] public List<Pictures4Tablet> InvestigationImage { get; set; } = new();
}