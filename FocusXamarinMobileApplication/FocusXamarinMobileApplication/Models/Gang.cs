namespace FocusXamarinMobileApplication.Models;

public class Gang
{
    private bool _disposed;

    [PrimaryKey]
    [AutoIncrement]
    [JsonIgnore]
    public int GangId { get; set; }


    public long Id { get; set; }

    public long ProjectManagerId { get; set; }

    public string ProjectManagerFirstName { get; set; }

    public string ProjectManagerSurname { get; set; }

    public string ProjectManagerPin { get; set; }

    public long SupervisorId { get; set; }

    public string SupervisorFirstName { get; set; }

    public string SupervisorSurname { get; set; }

    public string SupervisorPin { get; set; }

    public long GangLeaderId { get; set; }

    public string GangLeaderFirstName { get; set; }

    public string GangLeaderSurname { get; set; }

    public string GangLeaderPin { get; set; }

    public string ContractReference { get; set; }

    public string CNumber { get; set; }

    public string QNumber { get; set; }

    public string ProjectName { get; set; }

    public long ResourceGangId { get; set; }

    public string ConPrefix { get; set; }

    public bool EveningWork { get; set; }

    public DateTime WorkDate { get; set; }

    [JsonIgnore] public bool IsSelected { get; set; }

    //[OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<GangMember> GangMembers { get; set; }
    public string GangMemberNames { get; set; }
    public string SupervisorFullName { get; set; }

    public List<Person> GangLabourFiles { get; set; } = new();

    public List<ClaimedNotesFile> ClaimedNotes { get; set; } = new();
}