﻿namespace FocusXamarinMobileApplication.Views;public partial class DailyDiaryPage : ContentPage, IFormsPage{    private readonly DailyDiaryPageViewModel _vm;    public DailyDiaryPage()    {        InitializeComponent();        NavigationPage.SetHasNavigationBar(this, false);        _vm = App.ViewModelLocator.DailyDiaryPageViewModel;        BindingContext = _vm;        ListStartAbb.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView lsa) lsa.SelectedItem = null;        };        ListStartNumber.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView lsn) lsn.SelectedItem = null;        };        ListStartEndpoints.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView lse) lse.SelectedItem = null;        };        ListEndAbb.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView lea) lea.SelectedItem = null;        };        ListEndNumber.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView len) len.SelectedItem = null;        };        ListEndEndpoints.ItemTapped += (sender, e) =>        {            // don't do anything if we just de-selected the row.            if (e.Item == null) return;            // Deselect the item.            if (sender is ListView lee) lee.SelectedItem = null;        };    }    public void RefreshPage()    {    }    protected override void OnAppearing()    {        base.OnAppearing();        _vm.SetStartOfDayPicture.Execute("CheckStartOfdayPicture");        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)            _vm.ScreenLoadedSupervisor.Execute(null);        else            _vm.ScreenLoaded4InputDiaries.Execute(null);    }    private void OnStartPhotoImageButtonClicked(object sender, EventArgs args)    {        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.STARTOFDAY;        _vm.AddDiaryPictures.Execute(null);    }    private void OnEndPhotoImageButtonClicked(object sender, EventArgs args)    {        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.ENDOFDAY;        _vm.AddDiaryPictures.Execute(null);    }    private void SelectedStartAbbreviation(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedStartAbbreviation.Execute(e.SelectedItem);    }    private void SelectedStartAddress(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedStartAddress.Execute(e.SelectedItem);    }    private void SelectedStartStreet(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedStartStreet.Execute(e.SelectedItem);    }    private void SelectedEndAbbreviation(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedEndAbbreviation.Execute(e.SelectedItem);    }    private void SelectedEndAddress(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedEndAddress.Execute(e.SelectedItem);    }    private void SelectedEndStreet(object sender, SelectedItemChangedEventArgs e)    {        if (e.SelectedItem != null) _vm.SelectedEndStreet.Execute(e.SelectedItem);    }}