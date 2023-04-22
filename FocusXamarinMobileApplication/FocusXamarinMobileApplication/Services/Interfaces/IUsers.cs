#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services.Interfaces;

internal interface IUsers
{
    List<Person> GetAllUsersNow();

    //Task<Options> GetAllOptionsNow(int LoggedInUserId);
    Person GetLoggedInUser();
    bool CreateDBifNotExists();
    Task ClearAllRows();
    Task AddUsers(List<Person> passedUsers);
    Task AddUser(Person passedUser);
    Task AddOptions(Options passedOptions);
    Person Check4ValidLoggedInPerson();
}