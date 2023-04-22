namespace FocusXamarinForms20082020V1.Views;

public partial class ReInstatementPage : ContentPage, IFormsPage
{
    private readonly ReInstatementPageViewModel _vm;

    public ReInstatementPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ReInstatementPageViewModel;

        BindingContext = _vm;

        //  _vm.ScreenLoaded.Execute(null);

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    public void btnDelete_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        NavigationalParameters.ReinstatmentMeasureToDelete = button.CommandParameter as ReinstatementMeasure;

        _vm.DeleteMeasure.Execute(null);
    }

    private void materialsPicker_PrpertyChanged(object sender, PropertyChangedEventArgs e)
    {
        _vm.SetSizePickerList.Execute(null);
    }
}