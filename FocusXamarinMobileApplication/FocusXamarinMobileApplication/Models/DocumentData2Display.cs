using System;
using FocusXamarinMobileApplication.Helpers;
using SQLite;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Models;

public class DocumentData2Display
{
    private string _passedDate = string.Empty;
    public int Id { get; set; }
    public string DocumentId { get; set; }
    public string FolderPath { get; set; } //
    public string TabletDocumentFolder { get; set; } //
    public string FileName { get; set; } //
    public string DocumentTitle { get; set; }
    public string ContractNumber { get; set; }
    public string QNumber { get; set; }
    public DateTime UploadedDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");

    public string ExpiryDate
    {
        get => _passedDate;
        set
        {
            if (value == "" || value.Contains("01/01/0001") || value.Contains("01/01/1900"))
                _passedDate = "N/A";
            else
                _passedDate = value.Length > 10 ? value.Substring(0, 10) : value;
        }
    }

    public bool IsItaFolder { get; set; } //
    public string Section { get; internal set; }

    [Ignore] public bool IsVisible { get; set; }

    [Ignore] public ImageSource _image { get; set; } = SimpleStaticHelpers.GetImage("PDFicon");

    [Ignore]
    public ImageSource Image
    {
        get => _image;
        set => _image = value;
    }
}