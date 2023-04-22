namespace FocusXamarinForms20082020V1.Views;

public partial class PermitPage : ContentPage, IFormsPage
{
    private readonly PermitPageViewModel _vm;

    public PermitPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        BindingContext = App.ViewModelLocator.PermitPageViewModel;
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }
}