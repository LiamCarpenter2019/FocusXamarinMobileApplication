#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

//[Table("InvestigationDamages")]
public class InvestigateDamages : BusinessEntityBase
{
    //[PrimaryKey, AutoIncrement]
    public int InvestigationDamagesId { get; set; }
    public string DamageId { get; set; }
    public string ProjectName { get; set; }
    public string DamageType { get; set; }
    public string DamageColour { get; set; }
    public string Location { get; set; }
    public DateTime IncidentDateTime { get; set; }
    public string FullName { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string InvestigationLevel { get; set; }
    public string DamageTypeId { get; set; }
    public string PublicUtilityDamageGuid { get; set; }
    public string Category { get; set; }
    [Ignore] public List<DamageToInvestigate> DamageDetails { get; set; } = new();
    [Ignore] public List<GangResponsible> GangResponisble { get; set; } = new();

    //[Ignore] public List<InjuredPerson> InjuredPersonnel { get; set; } = new List<InjuredPerson>();
    //[Ignore] public List<ExternalPersonnel> ThirdPartyPersonnel { get; set; } = new List<ExternalPersonnel>();
    //[Ignore] public List<Witness> Witnesses { get; set; } = new List<Witness>();

    //[Ignore] public List<Pictures4Tablet> PreviousImages { get; set; } = new List<Pictures4Tablet>();
    //[Ignore] public List<Pictures4Tablet> SignatureImages { get; set; } = new List<Pictures4Tablet>();

    //[Ignore] public PublicUtilityDamageQuestion DamageTypeQuestion { get; set; }
    //[Ignore] public List<InvestigationAnswer> InvestigationAnswers { get; set; } = new List<InvestigationAnswer>();


    //[Ignore] public List<Person> GangMembers { get; set; } = new List<Person>();
}