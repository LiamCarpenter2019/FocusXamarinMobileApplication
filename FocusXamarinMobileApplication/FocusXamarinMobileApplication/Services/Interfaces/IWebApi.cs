#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IWebApi
{
    //Task<string> LoginAsync(Person Details);

    //Task<IEnumerable<Person>> GetUserData(string UserRole, long UserId);
    //Task<JobData> GetJobData(string UserRole, long UserId);
    //Task<IEnumerable<Document>> GetJobDocsData(List<long> JobIds);

    Task<Tuple<Person, string, List<Assignment>>> LogonRequest(string loggedInUserAlias);

    Task<long> SaveInvoiceToServer(Order order);

    //Task<UtilityDamageData> GetData();
    //Task<string> SubmitRegisterStrikes();
    //Task SaveData(UtilityDamageData Data);
    //Task<bool> DoPartialSubmit(Investigation PartiallyCompleteInvestigation);
    //Task<bool> UpdatePin(GangMember Member);
    //Task<bool> DoFullSubmit(Investigation CompleteInvestigation);
    //Task<bool> SubmitApprovedInvestigation(ApprovedDamage Damage);
    //Task<string> GetSasUri();
}