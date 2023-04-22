namespace FocusXamarinForms20082020V1.Views;

public partial class MeasureApprovalPage : ContentPage, IFormsPage
{
    public MeasureApprovalPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = App.ViewModelLocator.MeasureApprovalPageViewModel;
    }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}