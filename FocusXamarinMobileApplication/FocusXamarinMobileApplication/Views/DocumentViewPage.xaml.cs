#region

using System.IO;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Syncfusion.SfPdfViewer.XForms;
using Xamarin.Forms;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class DocumentViewPage : ContentPage, IFormsPage
{
    private readonly DocumentViewPageViewModel _vm;

    public DocumentViewPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.DocumentViewPageViewModel;
        BindingContext = _vm;
        pdfViewerControl.DocumentSaveInitiated += pdfViewerControl_DocumentSaveInitiated;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        pdfViewerControl.AnnotationSettings.IsLocked = true;
        pdfViewerControl.MaximumZoomPercentage = 400;

        _vm.ScreenLoaded.Execute(null);
    }


    private void pdfViewerControl_DocumentSaveInitiated(object sender, DocumentSaveInitiatedEventArgs args)
    {
        var stream = args.SaveStream as MemoryStream;
    }
}