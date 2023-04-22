#region

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using PCLStorage;
using FileAccess = PCLStorage.FileAccess;
using FileSystem = PCLStorage.FileSystem;
using List = Microsoft.SharePoint.Client.List;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class LocalStorage : ILocalStorage
{
    //    [Time] public static string Root { get; set; } =

    [Time]
    public async Task<string> UpdateFileFromAzure(string filename, string localFolderPath, string remoteFolderPath)
    {
        var returnValue = "";

        if (filename == null || localFolderPath == null || remoteFolderPath == null)
        {
            var n = 1;
        }
        else
        {
            byte[] imageFromAzure;
            try
            {
                var cv2 = new Constants();
                imageFromAzure =
                    await GetAzureBlob2ByteArray($"{remoteFolderPath}{filename}", cv2.FocusDataContainerInAzure);
                if (imageFromAzure != null && imageFromAzure.Length > 0)
                {
                    returnValue = "GOOD";
                    await StoreImagesLocallyAndUpdatePath(localFolderPath, filename, imageFromAzure);
                }
                else
                {
                    returnValue = "NothingDownloaded";
                }
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent(
                    $"Data passed to the {ToString()} has thrown this error {ex} : fileName : {filename} for: {NavigationalParameters.LoggedInUser?.FullName}");

                returnValue = ex.Message;
            }
        }

        return returnValue;
    }


    [Time]
    public async Task<Stream> GetImageStreamFromLocalstorageAsync(string passedFolder, string filename)
    {
        //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        // var path = Path.Combine(documentsPath, "..", "Library", "HnSDocuments");

        var rootFolder = FileSystem.Current.LocalStorage; //.GetFolderFromPathAsync(Root);

        ///var/mobile/Containers/Data/Application/29CC01A7-B852-42F7-834B-5B2D693DE899/Documents/../Library/\Company Documents/Complete Handbook.pdf
        var file = await rootFolder.GetFileAsync($"{passedFolder}/{filename}");

        using (var inStream = await file.OpenAsync(FileAccess.Read))
        {
            var memStream = new MemoryStream();
            await inStream.CopyToAsync(memStream);
            return memStream;
        }
    }

    //"CompanyDocs/WasteManagement[99].pdf"
    [Time]
    public async Task<Stream> GetImageStreamFromLocalstorageAsync(string documentToView)
    {
        try
        {
            var rootFolder = FileSystem.Current.LocalStorage; //.GetFolderFromPathAsync(Root);

            ///var/mobile/Containers/Data/Application/29CC01A7-B852-42F7-834B-5B2D693DE899/Documents/../Library/\Company Documents/Complete Handbook.pdf
            var file = await rootFolder.GetFileAsync($"{documentToView}");

            using (var inStream = await file.OpenAsync(FileAccess.Read))
            {
                var memStream = new MemoryStream();
                await inStream.CopyToAsync(memStream);
                return memStream;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    //"CompanyDocs/WasteManagement[99].pdf"
    [Time]
    public async Task<string> GetFilePathAsync(string documentToView)
    {
        try
        {
            var rootFolder = FileSystem.Current.LocalStorage; //.GetFolderFromPathAsync(Root);

            ///var/mobile/Containers/Data/Application/29CC01A7-B852-42F7-834B-5B2D693DE899/Documents/../Library/\Company Documents/Complete Handbook.pdf
            var file = await rootFolder.GetFileAsync($"{documentToView}");

            return file?.Path;
        }
        catch (Exception ex)
        {
            return "";
        }
    }


    // FROM Abrar HnS App
    // [Time] public async Task DownloadAndSaveFile(string FileName)
    //{

    //    
    //    Directory.CreateDirectory(path);

    //    var pathToUpload = Path.Combine(path, FileName);

    //    var request = new TransferUtilityDownloadRequest
    //    {
    //        BucketName = Constants.JOB_FILES_BUCKET_NAME,
    //        Key = FileName
    //    };

    //    request.FilePath = pathToUpload;

    //    var client = Constants.S3Client;
    //    var transferUtility = new TransferUtility(client);
    //    await transferUtility.DownloadAsync(request);

    //}

    // [Time] public async Task<bool> CheckFileExists(string path)
    //{
    //    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    //    var docPath = Path.Combine(documentsPath, "..", "Library", path);
    //    if (File.Exists(docPath))
    //    {
    //        return await Task.FromResult(true);
    //    }
    //    else
    //    {
    //        return await Task.FromResult(false);
    //    }
    //}

    // [Time] public Task<string> GetFile(string FileName)
    //{
    //    string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    //    var docPath = Path.Combine(documentsPath, "..", "Library", FileName);

    //    return Task.FromResult(docPath);
    //}

    // [Time] public Task<bool> DeleteImage(string FilePath)
    //{
    //    try
    //    {
    //        if (File.Exists(FilePath))
    //        {
    //            File.Delete(FilePath);
    //            return Task.FromResult(true);
    //        }
    //    }
    //    catch
    //    {

    //    }
    //    return Task.FromResult(false);
    //}

    // [Time] public Task<string> ReadFile(string documentsPath, string fileName)
    //{
    //    string documentsPath = ;
    //    var docPath = Path.Combine(documentsPath, fileName);

    //    return Task.FromResult(docPath);
    //}

    [Time]
    public string GetAzureBlob(string strFilenamePath)
    {
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        // Retrieve reference to a previously created container.
        var container = blobClient.GetContainerReference(cv2.CloudBlobContainer);
        // Retrieve reference to a blob named "myblob".
        var blockBlob = container.GetBlockBlobReference(strFilenamePath.Replace("%20", " "));

        using (var memoryStream = new MemoryStream())
        {
            blockBlob.DownloadToStreamAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
            //return "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";
        }
    }

    [Time]
    public static async Task<MemoryStream> GetAzureBlob2MemoryStream(string strFilenamePath, string container)
    {
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        // Retrieve reference to a previously created container.
        var container2 = blobClient.GetContainerReference(container);
        // Retrieve reference to a blob named "myblob".
        var blockBlob = container2.GetBlockBlobReference(strFilenamePath.Replace("%20", " "));

        var memoryStream = new MemoryStream();
        //using (var memoryStream = new MemoryStream())
        //{
        await blockBlob.DownloadToStreamAsync(memoryStream);

        //return "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";
        //}
        return memoryStream;
    }

    [Time]
    public static bool Save2AzureBlob(string strFileContents, string strFileName)
    {
        var returnValue = false;
        byte[] filebytes = null;
        // Create the blob client.
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();

        // Retrieve reference to a previously created container.
        var container = blobClient.GetContainerReference(cv2.CloudBlobContainer);

        // Retrieve reference to a blob named "myblob".
        var blockBlob = container.GetBlockBlobReference(strFileName);

        filebytes = Convert.FromBase64String(strFileContents);
        //var memStream = new MemoryStream(filebytes);

        // Create or overwrite the "myblob" blob with contents from a local file.
        using (var memStream = new MemoryStream(filebytes))
        {
            blockBlob.UploadFromStreamAsync(memStream);
        }

        return returnValue;
    }

    [Time]
    public static bool Save2AzureBlobMemoryStream(MemoryStream msFileContents, string strFileName,
        string strContainerName)
    {
        var returnValue = true;

        // Create the blob client.
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        try
        {
            // Retrieve reference to a previously created container.
            var container =
                blobClient.GetContainerReference(
                    strContainerName); // WebConfigurationManager.AppSettings["thisAzureDataContainer"]);

            // Retrieve reference to a blob named "myblob".
            var blockBlob = container.GetBlockBlobReference(strFileName);

            msFileContents.Position = 0;
            blockBlob.UploadFromStreamAsync(msFileContents);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the Localstorage.Save2AzureBlobMemoryStream has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message + " ... " + ex.StackTrace;
            returnValue = false;
        }

        return returnValue;
    }

    [Time]
    public async Task<bool> Save2AzureBlobByByteArray(byte[] fileContents, string strFileName, string strContainerName)
    {
        var returnValue = true;
        // Create the blob client.
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        try
        {
            // Retrieve reference to a previously created container.
            var container =
                blobClient.GetContainerReference(
                    strContainerName); // WebConfigurationManager.AppSettings["thisAzureDataContainer"]);

            // Retrieve reference to a blob named "myblob".
            var blockBlob = container.GetBlockBlobReference(strFileName);

            await blockBlob.UploadFromByteArrayAsync(fileContents, 0, fileContents.Length);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message + " ... " + ex.StackTrace;
            returnValue = false;
        }

        return returnValue;
    }

    [Time]
    public static void CreateFolders4Files(string strFolder, string strClientsOrCostings)
    {
        var cv2 = new Constants();
        var fileClient = cv2.StorageAccount.CreateCloudFileClient();
        var share = fileClient.GetShareReference(
            strClientsOrCostings); // strClientsOrCostings.Replace(" ", "%20"));// "focusspclients");

        try
        {
            var directory =
                share.GetRootDirectoryReference()
                    .GetDirectoryReference(strFolder); // strFolder.Replace(" ", "%20"));
            directory.CreateAsync();
            //Assert.IsTrue(directory.Exists());
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the Localstorage.CreateFolders4Files has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message + " ... " + ex.StackTrace;
        }
    }

    //JobPictures/{FileName}, cv2.FocusDataContainerInAzure
    [Time]
    public static bool DoesFileFolderExist(string strFileOrFolderName, string strContainerName)
    {
        var blnReturnValue = true;
        var cv2 = new Constants();
        strContainerName = strContainerName == "" ? cv2.FocusDataContainerInAzure : strContainerName;
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        // Retrieve reference to a previously created container.
        var container = blobClient.GetContainerReference(strContainerName);
        // Retrieve reference to a blob named "myblob".
        var blockBlob = container.GetBlockBlobReference(strFileOrFolderName);

        using (var memoryStream = new MemoryStream())
        {
            try
            {
                blockBlob.DownloadToStreamAsync(memoryStream);
                blnReturnValue = true;
            }
            catch (Exception ex)
            {
                // There cannot be anything there if there is an exception so return false
                var x = ex.Message + "|" + ex.StackTrace;
                blnReturnValue = false;
            }
        }

        return blnReturnValue;
    }

    [Time]
    public static S3Image GetS3ImageFromAzure(string strPath, string strFilename, string strContainer)
    {
        var ms = new MemoryStream();

        var thisImage = new S3Image();
        thisImage.FileName = strFilename;
        thisImage.Extension = strFilename.Split('.')[1];
        thisImage.MimeType = "image/" + strFilename.Split('.')[1];

        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(strContainer);
        var blockBlob = container.GetBlockBlobReference(strPath + strFilename);

        if (blockBlob != null)
        {
            blockBlob.DownloadToStreamAsync(ms);
            ms.Position = 0;
            thisImage.File = ms;
        }

        thisImage.FileSize = ms.Length;

        return thisImage;
    }

    [Time]
    public static async Task<byte[]> GetAzureBlob2ByteArray(string strFilenamePath, string strContainer)
    {
        var cv2 = new Constants();
        var blobClient = cv2.StorageAccount.CreateCloudBlobClient();
        // Retrieve reference to a previously created container.
        var container = blobClient.GetContainerReference(strContainer);
        // Retrieve reference to a blob named "myblob".
        var blockBlob = container.GetBlockBlobReference(strFilenamePath.Replace("%20", " "));

        using (var memoryStream = new MemoryStream())
        {
            await blockBlob.DownloadToStreamAsync(memoryStream);
            return memoryStream.ToArray();
            //return "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";
        }
    }

    public static byte[] DownloadSharePointFile(Docs4Tablet document, ClientContext clientContext)
    {
        var DocLibraryPathArray = document.DocLibraryPath.Split('/');
        var qNumber = 0;
        int.TryParse(DocLibraryPathArray[3], out qNumber);


        if (qNumber >= 0)
        {
            List spList = null;
            if (qNumber != 0) spList = clientContext.Web.Lists.GetByTitle(Convert.ToString(qNumber));
            if (qNumber == 0) spList = clientContext.Web.Lists.GetByTitle("CompanyDocument");
            clientContext.Load(spList);
            clientContext.Load(spList.RootFolder);
            clientContext.ExecuteQuery();

            //string ThisFileURL = ClientSiteURL + path;
            // /sites/(Staging)Projects + /{quoteId}/Project/Master Job Pack/Works/CXN-3.31-PL-XE-1/PreSitePoleSurvey/CXN-3.31-PL-XE-1_23022022_160601.pdf
            // /sites/(Staging)Projects + /CompanyDocument/CompanyDocument/COSHH/xxx.pdf

            var file = clientContext.Web.GetFileByServerRelativeUrl(document.DocLibraryPath);
            try
            {
                clientContext.Load(file, f => f.Exists);
                clientContext.ExecuteQuery();
            }
            catch (Exception ex)
            {
                var zzz = ex.Message;
            }

            if (file.Exists)
            {
                clientContext.Load(file.ListItemAllFields);
                clientContext.Load(file, vs => vs.Exists, vs => vs.ListItemAllFields.Id, vs => vs.UIVersionLabel,
                    vs => vs.ListItemAllFields.DisplayName);
                clientContext.ExecuteQuery();
                var mstream = file.OpenBinaryStream();
                clientContext.ExecuteQuery();

                if (Convert.ToInt32(file.ListItemAllFields.FieldValues["File_x0020_Size"]) > 0)
                    using (var binaryReader = new BinaryReader(mstream.Value))
                    {
                        var data = binaryReader.ReadBytes(
                            Convert.ToInt32(file.ListItemAllFields.FieldValues["File_x0020_Size"]));
                        return data;
                    }
            }
        }

        return null;
    }

    #region Job Pictures

    [Time]
    public async Task<int> CountNumberOfJobPicturesInStorage(List<Pictures4Tablet> picsListing)
    {
        var returnValue = 0;

        foreach (var item in picsListing)
        {
            var result = await DoesDocExistOnTablet("JobPictures", item.FileName);
            if (result) returnValue++;
        }

        return returnValue;
    }

    [Time]
    public async Task<bool> DoesJobPictureExistOnTablet(string localFolder, string filename)
    {
        var rootFolder = FileSystem.Current.LocalStorage;
        var ct = new CancellationToken();
        var checkResult = await rootFolder.CheckExistsAsync($"{localFolder}/{filename}", ct);
        if (checkResult == ExistenceCheckResult.FileExists) return true; //Exists

        return false; //Doesnt exist
    }

    #endregion

    #region Documents

    [Time]
    public async Task<int> CheckIfDocExistsLocallyAndIfNotThenDownload(Docs4Tablet document,
        ClientContext clientContext)
    {
        var returnValue = 0;
        try
        {
            var localFolderName = "";
            var remoteFolderName = "";
            var DocLibraryPathArray = document.DocLibraryPath.Split('/');
            var filenameIndex = DocLibraryPathArray.Length - 1;

            switch (document.DocType)
            {
                case "CompanyDocs":
                {
                    localFolderName = "CompanyDocs";

                    var result1 = await DoesDocExistOnTablet(localFolderName, DocLibraryPathArray[filenameIndex]);

                    if (result1)
                    {
                        returnValue = 1;
                    }
                    else
                    {
                        // Go get from Sharepoint Doc Lib
                        var imageByteArray = DownloadSharePointFile(document, clientContext);
                        if (imageByteArray != null && imageByteArray.Length > 1000)
                        {
                            await StoreImagesLocallyAndUpdatePath(localFolderName, DocLibraryPathArray[filenameIndex],
                                imageByteArray);
                            returnValue = 5;
                        }
                        else
                        {
                            NavigationalParameters.MissingDocuments.Add(document);
                        }
                        //remoteFolderName = $"CompanyDocs{document.FolderPath.Replace("\\", "/")}/";
                        //var updateDocResult1 = await UpdateFileFromAzure(document.FileName, localFolderName, remoteFolderName);

                        //if (updateDocResult1.ToLower() == "good")
                        //{
                        //    returnValue = 1;
                        //}
                        //else
                        //{
                        //    NavigationalParameters.MissingDocuments.Add(document);
                        //}
                    }
                }
                    break;
                case "JobDocs":
                {
                    localFolderName = "JobPackFiles";

                    var result = await DoesDocExistOnTablet(localFolderName, DocLibraryPathArray[filenameIndex]);

                    if (result)
                    {
                        returnValue = 1;
                    }
                    else
                    {
                        // Go get from Sharepoint Doc Lib
                        var imageByteArray = DownloadSharePointFile(document, clientContext);
                        if (imageByteArray != null && imageByteArray.Length > 1000)
                        {
                            await StoreImagesLocallyAndUpdatePath(localFolderName, DocLibraryPathArray[filenameIndex],
                                imageByteArray);
                            returnValue = 5;
                        }
                        else
                        {
                            NavigationalParameters.MissingDocuments.Add(document);
                        }
                        //remoteFolderName = $"JobPackFiles/{SortJobPackPath(document.FolderPath, document.QNumber)}";
                        //var updateDocResult2 = await UpdateFileFromAzure(document.FileName, localFolderName, remoteFolderName);

                        //if (updateDocResult2.ToLower() == "good")
                        //{
                        //    returnValue = 1;
                        //}
                        //else
                        //{
                        //    NavigationalParameters.MissingDocuments.Add(document);
                        //}
                    }
                }
                    break;
                case "PlantDocs":
                {
                    localFolderName = "PlantDocs";
                    var result2 = await DoesDocExistOnTablet(document.DocType, document.FileName);

                    if (result2)
                    {
                        returnValue = 1;
                    }
                    else
                    {
                        remoteFolderName = $"PlantFiles/{SortPlantDocPath(document.FolderPath, document.PlantId)}";
                        var updateDocResult3 =
                            await UpdateFileFromAzure(document.FileName, localFolderName, remoteFolderName);

                        if (updateDocResult3.ToLower() == "good")
                            returnValue = 1;
                        else
                            NavigationalParameters.MissingDocuments.Add(document);
                    }
                }
                    break;
                default:
                    localFolderName = "OperativeDocs";
                    var result3 = await DoesDocExistOnTablet(document.DocType, document.FileName);

                    if (result3)
                    {
                        returnValue = 1;
                    }
                    else
                    {
                        remoteFolderName = "OperativesData/";
                        var updateDocResult =
                            await UpdateFileFromAzure(document.FileName, localFolderName, remoteFolderName);

                        if (updateDocResult.ToLower() == "good")
                            returnValue = 1;
                        else
                            NavigationalParameters.MissingDocuments.Add(document);
                    }

                    break;
            }

            return returnValue;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }

        return 0;
    }

    [Time]
    public async Task<int> CountNumberOfDocumentsInStorage(List<Docs4Tablet> docsListing)
    {
        var returnValue = 0;
        try
        {
            foreach (var item in docsListing)
            {
                var tabletFolder = item.DocType;
                if (item.DocType == "JobDocs") tabletFolder = "JobPackFiles";

                var result = await DoesDocExistOnTablet(tabletFolder, item.FileName);

                if (result)
                {
                    returnValue++;
                }
                else
                {
                    var localFolderName = "";
                    var remoteFolderName = "";
                    switch (item.DocType)
                    {
                        case "CompanyDocs":
                        {
                            localFolderName = "CompanyDocs";

                            remoteFolderName =
                                $"CompanyDocs{item.FolderPath.Replace("\\", "/")}/";
                            var updateDocResult1 =
                                await UpdateFileFromAzure(
                                    item.FileName, localFolderName,
                                    remoteFolderName);

                            if (updateDocResult1.ToLower() == "good") returnValue++;
                        }
                            break;
                        case "JobDocs":
                        {
                            localFolderName = "JobPackFiles";
                            remoteFolderName =
                                $"JobPackFiles/{SortJobPackPath(item.FolderPath, item.QNumber)}";
                            var updateDocResult2 =
                                await UpdateFileFromAzure(
                                    item.FileName, localFolderName,
                                    remoteFolderName);

                            if (updateDocResult2.ToLower() == "good") returnValue++;
                        }
                            break;
                        case "PlantDocs":
                        {
                            localFolderName = "PlantDocs";

                            remoteFolderName =
                                $"PlantFiles/{SortPlantDocPath(item.FolderPath, item.PlantId)}";
                            var updateDocResult3 =
                                await UpdateFileFromAzure(
                                    item.FileName, localFolderName,
                                    remoteFolderName);

                            if (updateDocResult3.ToLower() == "good") returnValue++;
                        }
                            break;
                        default:
                            localFolderName = "OperativeDocs";

                            remoteFolderName = "OperativesData/";
                            var updateDocResult =
                                await UpdateFileFromAzure(
                                    item.FileName, localFolderName,
                                    remoteFolderName);

                            if (updateDocResult.ToLower() == "good") returnValue++;
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }

        return returnValue;
    }

    [Time]
    public async Task StoreImagesLocallyAndUpdatePath(string passedFolder, string filename, byte[] imageBuffer)
    {
        try
        {
            var rootFolder = FileSystem.Current.LocalStorage;

            var folder = await rootFolder.CreateFolderAsync(passedFolder, CreationCollisionOption.OpenIfExists);

            var file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (var fileHandler = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                await fileHandler.WriteAsync(imageBuffer, 0, imageBuffer.Length);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public async Task<byte[]> GetImageFromLocalstorage(string passedFolder, string filename)
    {
        var rootFolder = FileSystem.Current.LocalStorage;

        var file = await rootFolder.GetFileAsync($"{passedFolder}/{filename}");

        using (var inStream = await file.OpenAsync(FileAccess.Read))
        {
            var memStream = new MemoryStream();
            await inStream.CopyToAsync(memStream);
            return memStream.ToArray();
        }
    }

    [Time]
    public async Task<bool> DoesDocExistOnTablet(string tabletFolder, string filename)
    {
        try
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var ct = new CancellationToken();
            var checkResult = await rootFolder.CheckExistsAsync($"{tabletFolder}/{filename}", ct);
            if (checkResult == ExistenceCheckResult.FileExists) return true; //Exists

            return false; //Doesnt exist
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
            return false; //Doesnt exist
        }
    }

    [Time]
    public async Task<bool> RemoveImageFromLocalStorage(string folders, string filename)
    {
        var folder = FileSystem.Current.LocalStorage;
        var exist = await DoesJobPictureExistOnTablet(folders, filename);
        if (exist)
        {
            var file = await folder.GetFileAsync($"{folders}/{filename}");
            await file.DeleteAsync();
            return true;
        }

        return false;
    }

    [Time]
    public Task<IFolder> GetTabletDocsPath()
    {
        var folder = FileSystem.Current.LocalStorage;

        return FileSystem.Current.GetFolderFromPathAsync("JobPackFiles/JobDocs");
    }

    private string SortPlantDocPath(string path, string plantId)
    {
        var returnValue = new StringBuilder();

        var splitItUpArray = path.Split('\\');
        foreach (var item in splitItUpArray)
            if (item != null && item.Length > 1)
                returnValue.Append($"[{plantId}]{item}/");

        return returnValue.ToString();
    }

    private string SortJobPackPath(string path, string qNumber)
    {
        var returnValue = new StringBuilder();

        var splitItUpArray = path.Split('\\');
        var counter = 0;
        foreach (var item in splitItUpArray)
            if (item != null && item.Length > 1)
            {
                if (counter == 0)
                    returnValue.Append($"[{qNumber}]{item}/");
                else
                    returnValue.Append($"{item}/");

                counter++;
            }

        return returnValue.ToString();
    }

    #endregion
}