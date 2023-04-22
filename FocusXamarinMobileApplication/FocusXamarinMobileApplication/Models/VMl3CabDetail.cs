#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class VMl3CabDetail : BusinessEntityBase
{
    public VMl3CabDetail()
    {
        Id = Guid.NewGuid();
        //EnquiryCreated = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string VmnbUnumber { get; set; }

    public string CabinetDetails { get; set; }
    public string Location { get; set; }
    public string ExistingCab { get; set; }
    public string UcNc { get; set; }

    public int L4Total { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public int OnOffsiteL4CabTotal { get; set; }
}