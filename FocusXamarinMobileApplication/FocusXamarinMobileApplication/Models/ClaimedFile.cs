#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class ClaimedFile : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public Guid ClaimId { get; set; } = Guid.NewGuid();
    public string ContractReference { get; set; } = "";
    public string QuoteId { get; set; } = "";
    public string ContractId { get; set; } = "";
    public string SynCode { get; set; } = "0";
    public string ConPrefix { get; set; } = "";
    public string ClaimSupervisor { get; set; } = "";
    public string ClaimGang { get; set; } = "";
    public string ClaimType { get; set; } = "";
    public string IsVariation { get; set; } = "";
    public string ClaimHeader { get; set; } = "";
    public string ClaimDesc { get; set; } = "";
    public decimal ClaimQty { get; set; } = 0;
    public string BaseUnit { get; set; } = "";
    public string ClaimRate01 { get; set; } = "";
    public string ClaimRate02 { get; set; } = "";
    public string ClaimPrefix { get; set; } = "";
    public DateTime ClaimDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string ToDoBeforeClaim { get; set; } = "";
    public DateTime PostedByGanger { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime ApprovedBySupervisor { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string SupervisorChanges { get; set; } = "";
    public string PostedByGangerName { get; set; } = "";
    public string ApprovedBySupervisorName { get; set; } = "";
    public DateTime PostedByAdmin { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public decimal Labour { get; set; } = 0;
    public decimal Plant { get; set; } = 0;
    public decimal Materials { get; set; } = 0;
    public decimal SubContract { get; set; } = 0;
    public string OriginalClaimQtyByGang { get; set; } = "";
    public decimal GangLabour { get; set; } = 0;
    public decimal GangPlant { get; set; } = 0;
    public decimal GangMaterials { get; set; } = 0;
    public decimal GrabLabour { get; set; } = 0;
    public decimal GrabPlant { get; set; } = 0;
    public decimal GrabMaterials { get; set; } = 0;
    public decimal BoxBuilderLabour { get; set; } = 0;
    public decimal BoxBuilderPlant { get; set; } = 0;
    public decimal BoxBuilderMaterials { get; set; } = 0;
    public decimal ReinstatingLabour { get; set; } = 0;
    public decimal ReinstatingPlant { get; set; } = 0;
    public decimal ReinstatingMaterials { get; set; } = 0;
    public decimal CableGangLabour { get; set; } = 0;
    public decimal CableGangPlant { get; set; } = 0;
    public decimal CableGangMaterials { get; set; } = 0;
    public decimal FibreGangLabour { get; set; } = 0;
    public decimal FibreGangPlant { get; set; } = 0;
    public decimal FibreGangMaterials { get; set; } = 0;
    public decimal Mchapter8 { get; set; } = 0;
    public decimal RiskAndBalancingItem { get; set; } = 0;
    public decimal SubbyLabourOnly { get; set; } = 0;
    public decimal SubbyLabourAndPlant { get; set; } = 0;
    public decimal ClaimWidth { get; set; } = 0;
    public string TobyPicture { get; set; } = "";
    public string TrackPicture { get; set; } = "";
    public string TobyMeasure { get; set; } = "";
    public decimal ClaimDepth { get; set; } = 0;
    public string AssetName { get; set; } = "";
    public string HeadDesc => $"{ClaimHeader} - {ClaimDesc}";
    [Ignore] public List<Guid> MeasureEndPoints { get; set; }
}