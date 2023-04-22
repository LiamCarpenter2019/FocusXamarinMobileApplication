#region

using Person = FocusXamarinForms20082020V1.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class DiarySelectionPage : ContentPage, IFormsPage
{
    private Binding _claimedNotesBinding;

    // private Binding<bool, bool> _itemCompletedBinding;
    public Tuple<Person, List<Assignment>, JobData4Tablet> NavPassedSelectInfo;

    public DiarySelectionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.DiarySelectPageViewModel;
        BindingContext = _vm;
    }

    public DiarySelectPageViewModel _vm { get; set; }


    public void RefreshPage()
    {
        //add refresh method where appropriate
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            _vm.SupervisorScreenLoaded.Execute(NavigationalParameters.ReturnPage);
        else
            try
            {
                _vm.ScreenLoaded4InputDiariesSelector.Execute("DailyDiaryViewModelKey");
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
    }
}