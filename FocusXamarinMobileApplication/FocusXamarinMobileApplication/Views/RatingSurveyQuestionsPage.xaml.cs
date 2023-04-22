using System;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class RatingSurveyQuestionsPage : ContentPage, IFormsPage
{
    private readonly RatingSurveyQuestionsPageViewModel _vm;

    public RatingSurveyQuestionsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.RatingSurveyQuestionsPageViewModel;

        BindingContext = _vm;

        //listView.ItemTapped += (sender, e) =>
        //{
        //    // don't do anything if we just de-selected the row.
        //    if (e.Item == null) return;
        //    // Deselect the item.
        //    if (sender is ListView lv) lv.SelectedItem = null;
        //};
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.IsLoading = true;
        _vm.ShowPage = false;

        try
        {
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
        }
    }


    private void GoGps(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;

        NavigationalParameters.SelectedRatingQuestion = imageButton.CommandParameter as RatingQuestionViewModel;
        _vm.SelectedQuestion = NavigationalParameters.SelectedRatingQuestion;
        _vm.GoQuestionGps.Execute(null);
    }

    private void GoComments(object sender, EventArgs args)
    {
        var imageButton = sender as ImageButton;
        NavigationalParameters.SelectedRatingQuestion = imageButton.CommandParameter as RatingQuestionViewModel;
        _vm.SelectedQuestion = NavigationalParameters.SelectedRatingQuestion;
        _vm.GoComments.Execute(null);
    }

    private void GoPhoto(object sender, EventArgs args)
    {
        try
        {
            var imageButton = sender as ImageButton;

            NavigationalParameters.SelectedRatingQuestion = imageButton.CommandParameter as RatingQuestionViewModel;
            _vm.SelectedQuestion = NavigationalParameters.SelectedRatingQuestion;

            _vm.GoPicture.Execute(null);
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
}