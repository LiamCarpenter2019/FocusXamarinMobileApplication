#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class OfflineDataStore : IDataStore<>
{
    #region IDataStore implementation

    [Time]
    public async Task<IEnumerable<Person>> GetPeopleAsync()
    {
        //do stuff!!!!
        var people = new List<Person>
        {
            new()
            {
                FirstName = "James",
                Surname = "Montemagno"
            },
            new()
            {
                FirstName = "Craig",
                Surname = "Dunn"
            }
        };

        return people;
    }

    #endregion
}