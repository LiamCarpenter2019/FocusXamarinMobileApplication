#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class InvestigationAnswer : BusinessEntityBase
{
    //private bool _disposed = false;
    private bool _isSelected;
    public long RemoteTableId { get; set; } = 0;
    public bool Ticked { get; set; } = false;
    public string Comment { get; set; } = "";
    public string QuestionId { get; set; } = "";
    public DateTime InputOn { get; internal set; } = DateTime.Now;
    public Guid PublicUtilityDamageGuid { get; set; }
    public long InvestigationId { get; set; } = 0;
    public string Answer { get; set; } = "";

    public long DamageId { get; set; } = 0;

    //[JsonIgnore, Ignore]
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            if (_isSelected) MessagingCenter.Send(this, "AnswerChanged");
        }
    }
}