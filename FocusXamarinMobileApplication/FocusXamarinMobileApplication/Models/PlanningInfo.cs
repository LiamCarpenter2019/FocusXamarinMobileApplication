#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PlanningInfo : BusinessEntityBase
{
    public PlanningInfo()
    {
        Id = Guid.NewGuid();
        //EnquiryCreated = DateTime.Now;
    }

    public Guid Id { get; set; }

    public int Qnumber { get; set; }
    public string VmnbUnumber { get; set; }

    public string RoadName { get; set; }
    public string StreetUsrn { get; set; }
    public string Coordinates { get; set; }
    public string HighwayAgency { get; set; }
    public string OtherParty { get; set; }
    public string TrafficSens { get; set; }
    public string EngDifficulties { get; set; }
    public string NoticeOrPermit { get; set; }
    public string Activity { get; set; }
    public string WorksToDo { get; set; }
    public string TrafficManagement { get; set; }

    public DateTime WhenCreated { get; set; }
}