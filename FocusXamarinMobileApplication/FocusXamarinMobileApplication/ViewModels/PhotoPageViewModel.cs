#region

using System.Collections.Generic;
using System.ComponentModel;
using FocusXamarinMobileApplication.Models;
using Photo = FocusXamarinMobileApplication.Models.Photo;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PhotoPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    // private PhotoView _photoView;

    public List<Photo> PhotoList { get; set; }

    public RelayCommand AddImage => new(async () =>
    {
        var p = new Pictures4Tablet();

        // await NavigateTo(new PhotoSelection(p, _photoView));
    });

    public RelayCommand<Pictures4Tablet> SelectImage => new(async p =>
    {
        //   await NavigateTo(new PhotoSelection(p, _photoView));
    });

    public RelayCommand RemoveImage => new(() => { });
}