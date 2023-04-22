using System.Collections.Generic;

namespace FocusXamarinMobileApplication.Models;

public class EventManagementInfo
{
    public List<EventManagementType> EventManagementTypeList { get; set; } = new();
    // public List<EventSeverity> EventSeverityList { get; set; } = new List<EventSeverity>();
}