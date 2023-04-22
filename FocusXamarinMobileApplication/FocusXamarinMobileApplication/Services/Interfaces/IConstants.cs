namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IConstants
{
    bool AreThereAnyConstants();
    Task<string> GetNewApiString(string aPIcode);
}