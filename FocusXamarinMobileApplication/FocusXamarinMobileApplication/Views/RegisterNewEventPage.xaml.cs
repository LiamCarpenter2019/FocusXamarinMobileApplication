#region

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;
using Photo = FocusXamarinMobileApplication.Models.Photo;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class RegisterNewEventPage : ContentPage, IFormsPage
{
    private readonly RegisterNewEventPageViewModel _vm;

    private List<string> _answers = new();

    public RegisterNewEventPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.RegisterNewEventPageViewModel;

        BindingContext = _vm;
    }

    public List<Photo> PhotoList { get; set; }

    public void RefreshPage()
    {
        _vm.RefreshData.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.RefreshData.Execute(null);
    }

    private void InjuredSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.InjuredPersonSelected.Execute(e.SelectedItemIndex);
    }

    private void Handle_Unfocused(object sender, FocusEventArgs e)
    {
        if (sender is DatePicker dp)
        {
            var tmp = dp.Date;
            dp.Date = dp.MinimumDate;
            dp.Date = tmp;
        }

        if (sender is TimePicker tp)
        {
            var tmp = tp.Time;
            tp.Time = new TimeSpan(123456789L);
            tp.Time = tmp;
        }
    }

    public void EventPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var em = ((Picker)sender).SelectedItem as EventManagementType;
            if (em != null)
            {
                _vm.SelectedEventTypeItem = em;
                _vm.EventPicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }

    public void SeverityPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var sv = ((Picker)sender).SelectedItem as PublicUtilityDamageQuestion;
            if (sv != null)
            {
                _vm.SelectedDamageType = sv;
                _vm.SeverityPicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }

    public void HospitalPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var hp = ((Picker)sender).SelectedItem as string;
            if (hp != null)
            {
                _vm.RequiresHospital = hp;

                _vm.HospitalPicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }

    public void ThirdPartyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var tp = ((Picker)sender).SelectedItem as string;
            if (tp != null)
            {
                _vm.ThirdPartyDamage = tp;

                _vm.ThirdPartyPicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }

    public void UtilityPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var up = ((Picker)sender).SelectedItem as UtilityCompany;
            if (up != null)
            {
                _vm.UtilityCompanySelection = up;
                _vm.UtilityPicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }

    private void DamagePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is Picker)
        {
            var dp = ((Picker)sender).SelectedItem as string;
            if (dp != null)
            {
                _vm.SelectedDamageLocation = dp;
                _vm.DamnagePicker_SelectedIndexChanged.Execute(null);
            }
        }

        ;
    }
}