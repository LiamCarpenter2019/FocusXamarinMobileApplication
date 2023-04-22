#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class DamageToInvestigate : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;

    //[PrimaryKey, AutoIncrement, JsonIgnore]
    public int DamageId { get; set; } = 0;

    public Guid DamageGuid { get; set; } = Guid.NewGuid();

    //[Ignore]
    public string UserType { get; set; } = "";

    //[Ignore]
    public string ClientName { get; set; } = "";

    //[Ignore]
    public string ProjectName { get; set; } = "";

    //[Ignore]
    public string Location { get; set; } = "";

    public DateTime IncidentDateTime { get; set; } = DateTime.Now;

    public string InvestigationId { get; set; } = "0";

    public long SupervisorId { get; set; } = 0;

    //[Ignore]
    public string ContractReference { get; set; } = "";

    //[Ignore]
    public string QNumber { get; set; } = "";

    public long GangLeaderId { get; set; } = 0;

    public string GangMembersIds { get; set; } = "";

    public bool SavedToServer { get; set; } = true;

    public int? FkInvestigateDamages { get; set; } = 0;

    public string InvestigatorName { get; set; } = "";

    public string StrikeClassification { get; set; }

    public DateTime? RiddorDate { get; set; } = DateTime.Now.Date;

    public TimeSpan RiddorTime { get; set; } = DateTime.Now.TimeOfDay;

    //[Ignore]
    public string CurrentInvestigationStatus { get; set; } = "";

    //[Ignore]
    public string InvestigatorId { get; set; } = "";

    public string ImmediateCause { get; set; }

    public string EventsLeading { get; set; }

    public string ImmediateAction { get; set; }

    public string Category { get; set; }

    public string PublicUtilityDamageGuid { get; set; }

    public DateTime InvestigationDate { get; set; } = DateTime.Now;

    public DateTime? DateOfClientNotification { get; set; } = DateTime.Now;

    [Ignore] public Person Supervisor { get; set; }

    [Ignore] public Person Gangleader { get; set; }

    [Ignore] public Investigation DamageInvestigated { get; set; } = new();

    [Ignore] public DamageTypeAnswers DamageTypeAnswer { get; set; } = new();

    [Ignore] public PublicUtilityDamageQuestion DamageType { get; set; } = new();

    [Ignore] public List<Person> GangMembers { get; set; } = new();

    [Ignore] public List<GangResponsible> GangResponisble { get; set; } = new();

    [Ignore] public List<ExternalPersonnel> ThirdPartyPersonnel { get; set; } = new();

    [Ignore] public List<InjuredPerson> InjuredPersonnel { get; set; } = new();

    [Ignore] public List<Witness> Witnesses { get; set; } = new();

    [Ignore] public PrintTypesProvided PrintTypesProvided { get; set; } = new();

    [Ignore] public List<Pictures4Tablet> PreviousImages { get; set; } = new();

    [Ignore] public List<Pictures4Tablet> SignatureImages { get; set; } = new();

    [Ignore] public DamageSignature SignatureInfo { get; set; }
}