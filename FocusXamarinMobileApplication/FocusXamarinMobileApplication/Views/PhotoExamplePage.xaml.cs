namespace FocusXamarinForms20082020V1.Views;

public partial class PhotoExamplePage : ContentPage, IFormsPage
{
    private readonly PhotoExamplePageViewModel _vm;

    public PhotoExamplePage()
    {
        InitializeComponent();

        _vm = App.ViewModelLocator.PhotoExamplePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        //throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _vm.ScreenLoaded.Execute(null);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
        finally
        {
            if (_vm.Photo?.Image != null && _vm.SampleImage != null)
            {
                _vm.ShowExampleFull = false;
                _vm.ShowSplitPage = true;
                _vm.ShowSubmitButtons = true;
            }
            else if (_vm.Photo?.Image == null && _vm.SampleImage != null)
            {
                _vm.ShowExampleFull = true;
                _vm.ShowSplitPage = false;
                _vm.ShowSubmitButtons = false;
            }
        }
    }

    public void SavePhoto(object sender, EventArgs e)
    {
        _vm.SavePhoto.Execute(null);
    }
}