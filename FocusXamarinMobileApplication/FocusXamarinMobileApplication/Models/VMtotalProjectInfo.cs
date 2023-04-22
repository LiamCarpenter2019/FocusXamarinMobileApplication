#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class VMtotalProjectInfo : BusinessEntityBase
{
    public VMtotalProjectInfo()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public int Qnumber { get; set; }
    public string VmnbUnumber { get; set; }
    public string WorksAddress { get; set; }
    public string DescriptionOfWorks { get; set; }
    public string AddnlComments { get; set; }
    public string AccntProjectCode { get; set; }
    public string POno { get; set; }
    public string LoaclAuthority { get; set; }
    public string OriginatorPlanner { get; set; }
    public string LocationOffice { get; set; }
    public string ContactNos { get; set; }
    public string EnquiryCreatedBy { get; set; }
    public DateTime EnquiryCreated { get; set; }
    public List<PlanningInfo> VMplanningData { get; set; }
    public List<VMexpansionReleaseData> VMexpansionReleaseData { get; set; }
    public List<VMl4CabDetail> VMl4CabDetails { get; set; }
    public List<VMl3CabDetail> VMl3CabDetails { get; set; }
    public bool IsTheJobCivils { get; set; }
    public string AnyErrorMessage { get; set; }
}