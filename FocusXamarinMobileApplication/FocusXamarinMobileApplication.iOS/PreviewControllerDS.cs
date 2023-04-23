﻿#region

using System;
using System.IO;
using Foundation;
using QuickLook;

#endregion

namespace FocusXamarinMobileApplication.iOS;

public class PreviewControllerDS : QLPreviewControllerDataSource
{
    //Document cache
    private readonly QLPreviewItem _item;

    //Setting the document
    public PreviewControllerDS(QLPreviewItem item)
    {
        _item = item;
    }

    //Setting document count to 1
    public override nint PreviewItemCount(QLPreviewController controller)
    {
        return 1;
    }

    //Return the document
    public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
    {
        return _item;
    }
}

public class QLPreviewItemFileSystem : QLPreviewItem
{
    private readonly string _filePath;

    //Setting file name and path
    public QLPreviewItemFileSystem(string fileName, string filePath)
    {
        ItemTitle = fileName;
        _filePath = filePath;
    }

    //Return file name
    public override string ItemTitle { get; }

    //Retun file path as NSUrl
    public override NSUrl ItemUrl => NSUrl.FromFilename(_filePath);
}

public class QLPreviewItemBundle : QLPreviewItem
{
    private readonly string _filePath;

    //Setting file name and path
    public QLPreviewItemBundle(string fileName, string filePath)
    {
        ItemTitle = fileName;
        _filePath = filePath;
    }

    //Return file name
    public override string ItemTitle { get; }

    //Retun file path as NSUrl
    public override NSUrl ItemUrl
    {
        get
        {
            var documents = NSBundle.MainBundle.BundlePath;
            var lib = Path.Combine(documents, _filePath);
            var url = NSUrl.FromFilename(lib);
            return url;
        }
    }
}