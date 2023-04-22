namespace FocusXamarinForms20082020V1.Views;

public partial class OrderListPage : ContentPage, IFormsPage
{
    private readonly OrderListPageViewModel _vm;

    public OrderListPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.OrderListPageViewModel;

        BindingContext = _vm;

        orderList.ItemTapped += (sender, e) =>
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

    public void InvoicePhotoCommand(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button != null)
        {
            NavigationalParameters.Order = button.CommandParameter as Order;

            if (NavigationalParameters.Order != null) _vm.InvoicePhotoCommand.Execute(null);
        }
    }


    public void VoidCommand(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button != null)
        {
            NavigationalParameters.Order = button.CommandParameter as Order;

            if (NavigationalParameters.Order != null) _vm.VoidCommand.Execute(null);
        }
    }
}