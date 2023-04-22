#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class GangResponsible : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public int InvestigationID { get; set; }
    public DateTime InputOn { get; set; }

    public Guid PublicUtilityDamageGuid { get; set; }
    public long PublicUtilityDamagesId { get; set; }

    public string OperativeName { get; set; }
    public string GangLeaderName { get; set; }
    public string SupervisorName { get; set; }

    public long SupervisorID { get; set; }
    public long GangLeaderID { get; set; }
    public long OperativeID { get; set; }
    public string GangResponsibleNames { get; set; }

    //public string GangResponsibleNames { get; set; }
}