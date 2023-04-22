#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class RegisterUtilityDamage : BusinessEntityBase
{
    public Guid PublicUtilityDamageGuid { get; set; }

    [Ignore] public List<EventManagementType> EventsManagementTypes { get; set; }

    public string DamageId { get; set; }
    public int StrikeId { get; set; }
    public string TxtReporterName { get; set; }
    public string TxtReporterCompany { get; set; }
    public int TxtContractorResponsibleId { get; set; }
    public string TxtContractorResponsibleContact { get; set; }
    public string TxtContractorResponsibleNumber { get; set; }
    public string TxtLocation { get; set; }
    public string TxtNotes { get; set; }
    public int DamageTypeId { get; set; } = 0;
    public string TxtAnswerOne { get; set; }
    public string TxtAnswerTwo { get; set; }
    public string TxtAnswerThree { get; set; }
    public string TxtAnswerFour { get; set; }
    public bool ChkInjuries { get; set; }
    public int TxtUtilityId { get; set; }
    public string TxtUtilityReference { get; set; }
    public string TxtUtilityContactName { get; set; }
    public string TxtUtilityContactNumber { get; set; }
    public string ContractReference { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public bool IsClientContacted { get; set; }
    public bool IsUtilityContacted { get; set; }
    public string DamageLocation { get; set; }
    public DateTime UtilityContactedDateTime { get; set; } = DateTime.Parse("01/01/1900");
    public DateTime IncidentDateTime { get; set; } = DateTime.Parse("01/01/1900");
    public DateTime? ClientContacted { get; set; } = DateTime.Parse("01/01/1900");

    [Ignore] public List<InjuredPerson> InjuredPersonnel { get; set; }

    [Ignore] public List<Pictures4Tablet> Pictures { get; set; }

    [Ignore] public List<GangResponsible> GangResponsible { get; set; }

    public bool? IsFinal { get; set; } = false;
}