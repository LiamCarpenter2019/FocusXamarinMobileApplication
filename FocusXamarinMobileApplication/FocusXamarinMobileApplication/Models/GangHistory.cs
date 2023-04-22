namespace FocusXamarinMobileApplication.Models;

public class GangHistory
{
    public DateTime DateGathered { get; set; }
    public string Name { get; set; }
    public Person Supervisor { get; set; }
    public Person GangLeader { get; set; }
    public List<Person> GangMembers { get; set; } = new();
    public string GangMemberNames { get; set; }
    public long InvestigationNumber { get; set; }
}