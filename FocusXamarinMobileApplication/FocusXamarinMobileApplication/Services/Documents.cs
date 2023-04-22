using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Microsoft.AppCenter.Analytics;

[assembly: Xamarin.Forms.Dependency(typeof(Documents))]

namespace FocusXamarinMobileApplication.Services;

public class Documents : IDocuments
{
    private string _holdCurrentFolders = "";

    [Time]
    public DocumentData2Display GetDocumentById(string path, string documentType, string jobQNumber,
        long operativeId, string documentId, long plantId = 0)
    {
        return GetDocuments(path, documentType, jobQNumber, operativeId, plantId)?
            .FirstOrDefault(x => x.DocumentId == documentId);
    }

    [Time]
    public List<DocumentData2Display> GetDocuments(string path, string documentType, string jobQNumber,
        long operativeId, long plantId = 0)
    {
        // List<Docs4Tablet> documents2Filter = new List<Docs4Tablet>() ;
        var documents2Return = new List<DocumentData2Display>();

        _holdCurrentFolders = "";
        //string cleanedUpPath = "";
        //int PathLevel = 1;

        try
        {
            //documents2Filter.AddRange( = NavigationalParameters.DataPassedToTablet.DocumentsData;//App.Database.GetItems<Docs4Tablet>().ToList();
            var docsList = new List<Docs4Tablet>();

            if (documentType == "OperativeDocs")
                docsList = NavigationalParameters.DataPassedToTablet?.DocumentsData
                    .Where(x => x.DocType == documentType && x.OperativeId == operativeId.ToString())?.ToList();
            else
                docsList = NavigationalParameters.DataPassedToTablet?.DocumentsData
                    .Where(x => x.DocType == documentType && x.QNumber == jobQNumber)?.ToList();

            foreach (var jobDoc2Check in docsList)
                switch (documentType)
                {
                    case "JobDocs" when jobDoc2Check?.DocType == "JobDocs" && jobDoc2Check?.QNumber == jobQNumber:
                    {
                        var dataReturned = SortDoc(path, jobDoc2Check);
                        if (dataReturned != null)
                        {
                            if (dataReturned.Item1.IsItaFolder)
                            {
                                if (documents2Return.Count <= 0) documents2Return?.Add(dataReturned?.Item1);

                                var tempList = documents2Return?.ToList();

                                if (dataReturned.Item1.FolderPath.Contains("\\"))
                                {
                                    if (tempList.All(x =>
                                            x.FolderPath?.Split('\\')[dataReturned.Item2] !=
                                            dataReturned?.Item1?.FolderPath?.Split('\\')[dataReturned.Item2]))
                                        documents2Return?.Add(dataReturned?.Item1);
                                }
                                else
                                {
                                    if (tempList.All(x =>
                                            x.FolderPath?.Split('/')[dataReturned.Item2] !=
                                            dataReturned?.Item1?.FolderPath?.Split('/')[dataReturned.Item2]))
                                        documents2Return?.Add(dataReturned?.Item1);
                                }
                            }
                            else
                            {
                                documents2Return?.Add(dataReturned?.Item1);
                            }
                        }

                        break;
                    }
                    case "CompanyDocs" when jobDoc2Check.DocType == "CompanyDocs":
                    {
                        var cutPath = path;
                        if (path.Length > 1) cutPath = path?.Substring(0, path.Length - 1);

                        var dataReturned = SortDoc(cutPath, jobDoc2Check);
                        if (dataReturned != null) documents2Return?.Add(dataReturned.Item1);

                        break;
                    }
                    case "OperativeDocs" when jobDoc2Check.DocType == "OperativeDocs":
                    {
                        if (operativeId != 0 && jobDoc2Check?.OperativeId == operativeId.ToString())
                        {
                            var docInfo = new DocumentData2Display
                            {
                                Id = Convert.ToInt32(jobDoc2Check?.OperativeId),
                                DocumentTitle = jobDoc2Check?.DocumentTitle,
                                FileName = jobDoc2Check?.FileName,
                                UploadedDate = jobDoc2Check.UploadedDate,
                                FolderPath = jobDoc2Check?.FolderPath,
                                TabletDocumentFolder = "OperativeDocs",
                                ExpiryDate = jobDoc2Check?.ExpiryDate.ToString("dd/MM/yyyy hh:MM:ss"),
                                IsItaFolder = false,
                                IsVisible = true
                            };

                            if (docInfo != null) documents2Return.Add(docInfo);
                        }

                        break;
                    }
                    case "PlantDocs" when jobDoc2Check.DocType == "PlantDocs" && plantId != 0:
                    {
                        if (jobDoc2Check.PlantId == plantId.ToString())
                        {
                            var docInfo = new DocumentData2Display
                            {
                                Id = Convert.ToInt32(jobDoc2Check?.PlantId),
                                DocumentTitle = jobDoc2Check?.DocumentTitle,
                                FileName = jobDoc2Check?.FileName,
                                UploadedDate = jobDoc2Check.UploadedDate,
                                FolderPath = jobDoc2Check?.FolderPath,
                                TabletDocumentFolder = jobDoc2Check?.FolderPath,
                                IsItaFolder = false,
                                IsVisible = true
                            };

                            if (docInfo != null) documents2Return?.Add(docInfo);
                        }

                        break;
                    }
                }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var v = ex.Message;
        }

        return documents2Return;
    }

    [Time]
    public bool CreateDBifNotExists()
    {
        var returnValue = true;

        try
        {
            App.Database.CreateTable<Docs4Tablet>(); //create table
        }
        catch (Exception)
        {
            return false;
        }

        return returnValue;
    }

    [Time]
    public void ClearAllRows()
    {
        App.Database.ClearTable<Docs4Tablet>();
    }

    [Time]
    public Task AddDocument(Docs4Tablet passedDoc)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItem(passedDoc); });
    }

    [Time]
    public Task AddDocuments(List<Docs4Tablet> passedDocs)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedDocs); });
    }

    //[Time] public Task<List<Docs4Tablet>> GetAllJobDocuments()
    //{
    //    return Task.Factory.StartNew(() => App.Database.GetItems<Docs4Tablet>().ToList());
    //}

    [Time]
    public List<Docs4Tablet> GetAllJobDocuments()
    {
        if (NavigationalParameters.DataPassedToTablet != null)
        {
            var documents = App.Database.GetItems<Docs4Tablet>()?.ToList();
            return documents;
        }

        return new List<Docs4Tablet>();
    }

    private Tuple<DocumentData2Display, int> SortDoc(string path, Docs4Tablet passedDoc)
    {
        try
        {
            var cleanedUpPath = "/";

            var pathLevel = path.Split('/').Length - 1;

            if (passedDoc?.FolderPath != "\\")
                cleanedUpPath =
                    passedDoc?.FolderPath.Replace("\\",
                        "/"); //"/" + SimpleStaticHelpers.SortPath(passedDoc.FolderPath);

            var path2Check = GetCorrectPathLevel(cleanedUpPath, pathLevel);

            var docTitle = path2Check?.Replace("/", "").Trim();

            if (path == "/") // Default - ONLY show folders
            {
                if (!_holdCurrentFolders.Contains("|" + path2Check))
                {
                    _holdCurrentFolders += "|" + path2Check;
                    var docInfo = new DocumentData2Display
                    {
                        Id = passedDoc.Id,
                        DocumentTitle = docTitle,
                        FolderPath = passedDoc?.FolderPath,
                        DocumentId = passedDoc?.DocumentId,
                        FileName = passedDoc?.FileName,
                        QNumber = passedDoc?.QNumber,
                        ContractNumber = passedDoc?.ContractNumber,
                        IsItaFolder = true,
                        Section = passedDoc?.Section,
                        Image = SimpleStaticHelpers.GetImage("Folder_icon"),
                        IsVisible = false
                    };

                    return new Tuple<DocumentData2Display, int>(docInfo, pathLevel);
                }
            }
            else if (cleanedUpPath.Contains(path)
                    ) // OK this is not the root path so there may be other levels or a doc to show
            {
                if (path == cleanedUpPath)
                {
                    // Doc to show
                    var docInfo = new DocumentData2Display
                    {
                        Id = passedDoc.Id,
                        DocumentTitle = passedDoc?.DocumentTitle,
                        FileName = passedDoc?.FileName,
                        ExpiryDate = passedDoc?.ExpiryDate == DateTime.Parse("01/01/1900")
                            ? "N/A"
                            : passedDoc?.ExpiryDate.Date.ToString("dd/MM/yyyy"),
                        FolderPath = passedDoc?.FolderPath,
                        TabletDocumentFolder = passedDoc?.DocType == "JobDocs"
                            ? "JobPackFiles"
                            : passedDoc?.DocType,
                        IsItaFolder = false,
                        IsVisible = true,
                        Image = SimpleStaticHelpers.GetImage("PDFicon")
                    };

                    return new Tuple<DocumentData2Display, int>(docInfo, pathLevel);
                }

                // Not a doc so need to sort data for selected folder
                if (cleanedUpPath?.Length >= path?.Length)
                {
                    var checker = true;

                    if (_holdCurrentFolders != "" && _holdCurrentFolders.Contains("|"))
                    {
                        var items2Check = _holdCurrentFolders.Split('|');

                        if (items2Check.Where(item => item != "").Any(item => cleanedUpPath.Contains(item)))
                            checker = false;
                    }

                    if (checker)
                    {
                        _holdCurrentFolders += "|" + cleanedUpPath;
                        var docInfo = new DocumentData2Display
                        {
                            Id = passedDoc.Id,
                            FolderPath = cleanedUpPath, //.Replace(path, ""),
                            IsItaFolder = true,
                            Image = SimpleStaticHelpers.GetImage("Folder_icon"),
                            IsVisible = false,
                            DocumentTitle = docTitle
                        };
                        return new Tuple<DocumentData2Display, int>(docInfo, pathLevel);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var v = ex.Message;
        }

        return null;
    }

    private string GetCorrectPathLevel(string path, int levelNumber)
    {
        return (from item in path?.Split('/')
            where item != "/"
            select item)?.ToList()[levelNumber];
    }
}