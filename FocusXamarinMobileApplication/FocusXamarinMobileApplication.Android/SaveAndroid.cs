#region

using System.IO;
using System.Threading.Tasks;
using Java.IO;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

#endregion

[assembly: Dependency(typeof(SaveAndroid))]

namespace FocusXamarinForms20082020V1.Droid;

internal class SaveAndroid : ISave
{
    //Method to save document as a file in Android and view the saved document
    public async Task SaveAndView(string fileName, string contentType, MemoryStream stream)
    {
        string root = null;
        //Get the root path in android device.
        if (Environment.IsExternalStorageEmulated)
            root = Environment.ExternalStorageDirectory.ToString();
        else
            root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        //Create directory and file 
        var myDir = new File(root + "/Syncfusion");
        myDir.Mkdir();

        var file = new File(myDir, fileName);

        //Remove if the file exists
        if (file.Exists()) file.Delete();

        //Write the stream into the file
        var outs = new FileOutputStream(file);
        outs.Write(stream.ToArray());

        outs.Flush();
        outs.Close();

        //Invoke the created file for viewing
        if (file.Exists())
        {
            var path = Uri.FromFile(file);
            var extension = MimeTypeMap.GetFileExtensionFromUrl(Uri.FromFile(file).ToString());
            var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(path, mimeType);
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
        }
    }
}