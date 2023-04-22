#region

using FocusXamarinForms20082020V1.Helpers;
using Syncfusion.ListView.XForms;
using Device = Xamarin.Forms.Device;
using ScrollToPosition = Xamarin.Forms.ScrollToPosition;

#endregion

namespace FocusXamarinForms20082020V1.Views;

public partial class SurveyQuestionsPage : ContentPage, IFormsPage
{
    private readonly SurveyQuestionsPageViewModel _vm;

    private VisualContainer Container;

    public SurveyQuestionsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SurveyQuestionsPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

            _vm.IsLoading = true;

            _vm.ShowPage = false;

        _vm.EnableSubmit = true;

        try
        {
            _vm.PageLoaded.Execute(null);

            _vm.RefreshQuestionList();

            _vm.IsLoading = false;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
        finally
        {
            _vm.IsLoading = false;

            _vm.ShowPage = true;

            if (_vm.SelectedQuestion != null || NavigationalParameters.SelectedQuestion != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? NavigationalParameters.SelectedQuestion?.QuestionId,
                            (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
                    });
                }
        }
    }

    public void FilterQuestions(object sender, EventArgs e)
    {
        var button = sender as Button;

        _vm.ButtonAColour = Color.LightGray;
        btnCivils.BackgroundColor = Color.LightGray;
        _vm.ButtonBColour = Color.LightGray;
        btnTpa.BackgroundColor = Color.LightGray;
        _vm.ButtonCColour = Color.LightGray;
        btnHse.BackgroundColor = Color.LightGray;
        _vm.ButtonDColour = Color.LightGray;
        btnWay.BackgroundColor = Color.LightGray;
        _vm.ButtonEColour = Color.LightGray;
        btnReinstatement.BackgroundColor = Color.LightGray;
        _vm.ButtonFColour = Color.LightGray;
        btnRTM.BackgroundColor = Color.LightGray;
        _vm.ButtonGColour = Color.LightGray;
        btnNoticing.BackgroundColor = Color.LightGray;
        _vm.ButtonHColour = Color.LightGray;
        btnAll.BackgroundColor = Color.LightGray;

        if (button.BackgroundColor == Color.LawnGreen)
            button.BackgroundColor = Color.LightGray;
        else
            button.BackgroundColor = Color.LawnGreen;

        //button.BackgroundColor = Color.Green;

        _vm.FilteredQuestionBy = button.Text;

        //_vm.FilteredQuestions(button);
        _vm.RefreshQuestionList();
    }

    private async void GoMulti(object sender, EventArgs args)
    {
        //OnAppearing();
        var button = sender as Button;

        await _vm.GetCurrentMultiAnswerAsync(button);


        listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? 0,
            (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
    }

    private async void GoYesNoNa(object sender, EventArgs args)
    {
        //OnAppearing();
        var button = sender as Button;

        await _vm.GetCurrentAnswerAsync(button);



        listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? 0,
            (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);

    }

    private void GoGps(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;

        NavigationalParameters.SelectedQuestion = _vm.SelectedQuestion = imageButton.CommandParameter as SurveyQuestion;

        _vm.CheckSelectedAnswer();

        if (NavigationalParameters.SelectedQuestion.QuestionId != 0.10M &&
            string.IsNullOrEmpty(_vm.SelectedAnswer?.AnswerGiven) &&
            NavigationalParameters.SelectedQuestion.ResponseType !=
            "mark location on map, select house, comments, photos")
        {
            DisplayAlert("Please select a answer", "Please make a selection and continue!", "OK");
        }
        else
        {
            _vm.SelectedQuestion = NavigationalParameters.SelectedQuestion;

            _vm.GoQuestionGps.Execute(null);
        }

        listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? 0,
            (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);

    }

    private void GoComments(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;

        _vm.CheckSelectedAnswer();

        NavigationalParameters.SelectedQuestion = _vm.SelectedQuestion = imageButton.CommandParameter as SurveyQuestion;

        if (NavigationalParameters.SelectedQuestion.QuestionId != 0.10M &&
            string.IsNullOrEmpty(_vm.SelectedAnswer?.AnswerGiven) &&
            NavigationalParameters.SelectedQuestion.ResponseType !=
            "mark location on map, select house, comments, photos")
        {
            DisplayAlert("Please select a answer", "Please make a selection and continue!", "OK");
        }
        else
        {
            _vm.SelectedQuestion = NavigationalParameters.SelectedQuestion;

            _vm.GoComments.Execute(null);
        }

        listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? 0,
            (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
    }

    private void GoPhoto(object sender, EventArgs args)
    {
        try
        {
            var imageButton = sender as ImageButton;

            NavigationalParameters.SelectedQuestion =
                _vm.SelectedQuestion = imageButton.CommandParameter as SurveyQuestion;

            _vm.CheckSelectedAnswer();


            if (NavigationalParameters.SelectedQuestion.QuestionId != 0.10M &&
                string.IsNullOrEmpty(_vm.SelectedAnswer?.AnswerGiven) &&
                NavigationalParameters.SelectedQuestion.ResponseType !=
                "mark location on map, select house, comments, photos")
            {
                DisplayAlert("Please select a answer", "Please make a selection and continue!", "OK");
            }
            else
            {
                try
                {
                    _vm.SelectedQuestion = NavigationalParameters.SelectedQuestion;

                    _vm.GoPicture.Execute(null);


                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
               $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

                    var error = ex.ToString();
                }
                finally
                {

                    _vm.RefreshQuestionList();
                }

            }

            listView.ScrollTo(_vm.SelectedQuestion?.QuestionId ?? 0,
                (ScrollToPosition)Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    public void GoRating10(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "10";

        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void GoRating9(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "9";

        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void GoRating8(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "8";


        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void GoRating7(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "7";

        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void GoRating6(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "6";

        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.DEFAULT;

        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void GoRatingNA(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;

        imageButton.ClassId = "N/A";

        _ = _vm.QuestionRatingCommand(imageButton);
    }

    public void Distance_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (NavigationalParameters.CurrentPermit != null)
        {
            if (!string.IsNullOrEmpty(_vm.Distance))
            {
                NavigationalParameters.CurrentPermit.Distance = Convert.ToDecimal(_vm.Distance);
            }
        }
    }

    public void SaveEntry(object sender, EventArgs e)
    {
        var text = sender as Entry;

        NavigationalParameters.SelectedQuestion = _vm.SelectedQuestion = text.ReturnCommandParameter as SurveyQuestion;

        var answerGiven = text.Text;

        _vm.SaveEntryAnswer(answerGiven, text);
    }
}