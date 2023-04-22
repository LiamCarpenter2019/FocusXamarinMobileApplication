namespace FocusXamarinMobileApplication.Views
{
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage => Application.Current.MainPage as MainPage;
        List<NavMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            // Add items to the menu
            menuItems = new List<NavMenuItem>
            {
                new NavMenuItem {Id = MenuItemType.LogIn, Title="Jobs" },
                new NavMenuItem {Id = MenuItemType.Settings, Title="Settings" }
            };

            ListViewMenu.ItemsSource = menuItems;

            // Initialize the selected item
            ListViewMenu.SelectedItem = menuItems[0];

            // Handle the ItemSelected event to navigate to the
            // selected page
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((NavMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }

        private async void OnSignOut(object sender, EventArgs e)
        {
            var signout = await DisplayAlert("Sign out?", "Do you want to sign out?", "Yes", "No");
            if (signout)
            {
                await (Application.Current as Microsoft.SharePoint.Client.App).SignOut();
            }
        }

        private async void OnSignIn(object sender, EventArgs e)
        {
            try
            {
                if (!NavigationalParameters.Constants.AreThereAnyConstants())
                {
                    await Navigation.PushAsync(new SettingsPage());
                }
                else
                {
                    await (Application.Current as Microsoft.SharePoint.Client.App).SignIn();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Authentication Error", ex.Message, "OK");
            }
        }
    }
}