#region

#endregion

using System.Text.Json.Serialization;

namespace FocusXamarinMobileApplication.Models;

//[Table("SelectedUserOptions")]
public class Options : BusinessEntityBase
{
    //private bool disposed = false;
    //[PrimaryKey, AutoIncrement, JsonIgnorße]
    //public int id { get; set; }

    [JsonIgnore] public long FkFocusId { get; set; } = 0;

    public string Button1Image { get; set; } = "";
    public string Button1Text { get; set; } = "";
    public string Button2Image { get; set; } = "";
    public string Button2Text { get; set; } = "";
    public string Button3Image { get; set; } = "";
    public string Button3Text { get; set; } = "";
    public string Button4Image { get; set; } = "";
    public string Button4Text { get; set; } = "";
    public string Button5Image { get; set; } = "";
    public string Button5Text { get; set; } = "";
    public string Button6Image { get; set; } = "";
    public string Button6Text { get; set; } = "";
    public string Button7Image { get; set; } = "";
    public string Button7Text { get; set; } = "";
    public string Button8Image { get; set; } = "";
    public string Button8Text { get; set; } = "";
    public string Button9Image { get; set; } = "";
    public string Button9Text { get; set; } = "";
    public string Button10Image { get; set; } = "";
    public string Button10Text { get; set; } = "";
    public string Button11Image { get; set; } = "";
    public string Button11Text { get; set; } = "";
    public string Button12Image { get; set; } = "";
    public string Button12Text { get; set; } = "";
    public string Button13Image { get; set; } = "";
    public string Button13Text { get; set; } = "";
    public string Button14Image { get; set; } = "";
    public string Button14Text { get; set; } = "";
    public string Button15Image { get; set; } = "";
    public string Button15Text { get; set; } = "";
}