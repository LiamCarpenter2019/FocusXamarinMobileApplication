#region

using Syncfusion.SfPdfViewer.XForms;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class ToolboxTalkDocumentViewPage : ContentPage, IFormsPage
{
    private readonly ToolboxTalkDocumentPageViewModel _vm;

    public ToolboxTalkDocumentViewPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.ToolboxTalkDocumentPageViewModel;

        BindingContext = _vm;

        pdfViewerControl2.DocumentSaveInitiated += pdfViewerControl_DocumentSaveInitiated;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.GetDocument.Execute(null);

        if (NavigationalParameters.MultSignatures != null
            && NavigationalParameters.MultSignatures.All(x => !string.IsNullOrEmpty(x.Signature))
            && NavigationalParameters.MultSignatures.Count() > 0)
        {
            _vm.IsLoading = true;
            _vm.ShowButtons = false;
            _vm.UploadAllToolboxTalksCommand.Execute(null);
        }

        _vm.ShowButtons = true;
        _vm.IsLoading = false;
    }

    private void pdfViewerControl_DocumentSaveInitiated(object sender, DocumentSaveInitiatedEventArgs args)
    {
        var stream = args.SaveStream as MemoryStream;
    }
}