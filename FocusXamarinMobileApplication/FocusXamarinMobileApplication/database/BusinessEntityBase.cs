using FocusXamarinMobileApplication.Services.Interfaces;
using SQLite;

namespace FocusXamarinMobileApplication.database;

public class BusinessEntityBase : IBusinessEntity
{
    [PrimaryKey] [AutoIncrement] public int Id { get; set; }
}