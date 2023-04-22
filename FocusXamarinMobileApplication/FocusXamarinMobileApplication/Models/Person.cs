#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Person : BusinessEntityBase
{
    public long RemoteTableId = 0;
    public long FocusId { get; set; }

    [Ignore] public string Password { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string Surname { get; set; } = "";
    public string LoginAlias { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Ssid { get; set; } = "";
    public DateTime SsiDdate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string CompanyId { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string OperativeType { get; set; } = "";
    public DateTime InputOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string MemberPin { get; set; } = "";
    public string Email { get; set; } = "";
    public string CompanyAddress { get; set; }
    public string Token { get; internal set; } = "";

    public string OperativePicture { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string DataImage { get; set; } = "";
    public string EmploymentType { get; set; } = "";

    public Guid PublicUtilityDamageGuid { get; set; }

    // -------- NEW ----------------------
    public bool IsLoggedIn { get; set; } = false;

    public string Role { get; set; } = "";

    [Ignore] public string Signature { get; set; }

    [Ignore] public Color BackgroundHighlighted { get; set; } = Color.Transparent;

    [Ignore] public bool HasSigned { get; set; } = false;

    [Ignore] public Options MyOptions { get; set; } = new();

    [Ignore] public List<Labour> MyLabour { get; set; } = new();

    [Ignore] public Labour CurrentLabour { get; set; }

    [Ignore] public List<Holiday> Holidays { get; set; } = new();

    [Ignore] public List<UserGroup> MyGroups { get; set; } = new();

    [Ignore] public List<UserDailyTime> DailyProjectTimes { get; set; } = new();
}