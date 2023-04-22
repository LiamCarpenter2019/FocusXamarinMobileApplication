using System.Windows.Input;

namespace FocusXamarinMobileApplication.ViewModels
{
    public class RattingBarViewModal : FBaseViewModel, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private string _selectedStar;

        public RattingBarViewModal()
        {
            RattingBarCommand = new Command(OnItemTapped); // this will execute on tap of image (star)
            ClickCommand = new Command(OnClicked); // this will execute on the click of Click me button.
        }

        public ICommand RattingBarCommand { get; set; }
        public ICommand ClickCommand { get; set; }


        public string SelectedStar
        {
            get => _selectedStar;
            set
            {
                _selectedStar = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnClicked(object obj)
        {
            var b = (RattingBar) obj;
            //App.Current.MainPage.DisplayAlert("Selected Value is", b.SelectedStarValue.ToString(), "OK");
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void OnItemTapped(object obj)
        {
            var obje = obj;
            SelectedStar = "Selected Star is " + obj;
        }
    }
}