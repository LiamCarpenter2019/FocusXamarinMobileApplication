namespace FocusXamarinMobileApplication.database;

public class BusinessEntityBase : IBusinessEntity
{
    [PrimaryKey] [AutoIncrement] public int Id { get; set; }
}