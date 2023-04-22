namespace FocusXamarinForms20082020V1.Views;

public partial class MethodologyPage : ContentPage, IFormsPage
{
    private readonly MethodologyPageViewModel _vm;

    public MethodologyPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.MethodologyPageViewModel;
        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        _vm.RefreshData.Execute(null);
        // PhotoView.Refresh();
        //Vm.DamagePhotos = PhotoView.PhotoList;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.RefreshData.Execute(null);

        methodology.SelectedItem = null;
    }

    private async void GoYesNoNa(object sender, EventArgs args)
    {
        var button = sender as Button;
        await _vm.GetCurrentAnswer(button);

        RefreshPage();
    }

    private void GoComments(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;
        _vm.GoComments(imageButton);
        // _vm.RefreshQuestions();
        RefreshPage();
    }

    private void GoPhoto(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;
        _vm.GoPhoto(imageButton);
        // _vm.RefreshQuestions();
        RefreshPage();
    }
}