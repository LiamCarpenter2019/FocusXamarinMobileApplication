#region

#endregion

using System;
using System.IO;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Pictures4Tablet : BusinessEntityBase
{
    public long JobId { get; set; }

    public long SeverPictureId { get; set; } = 0;
    public DateTime DateTimeTaken { get; set; } = DateTime.Now;
    public long? QNumber { get; set; } = 0;
    public long? OperativeId { get; set; } = 0;
    public string OperativeRole { get; set; } = "";
    public string FileName { get; set; } = "";
    public string PictureComments { get; set; } = "";
    public string Latitude { get; set; } = "";
    public string Longitude { get; set; } = "";
    public string PictureReason { get; set; } = "";
    public string PicturePath { get; set; } = "";
    public string FromAddress { get; set; } = "";
    public string ToAddress { get; set; } = "";
    public string ResponseId { get; set; } = "";
    public Guid Identifier { get; set; } = Guid.Empty;
    public string Category { get; set; } = "";
    public string QuestionId { get; set; } = "0";
    public string StreetName { get; set; } = "";
    public Guid AssignmentId { get; set; } = Guid.Empty;
    public string ContractReference { get; set; } = "";
    public int RotateDegree { get; set; } = 0;
    public string GangLeaderId { get; set; } = "0";
    public string SupervisorId { get; set; } = "0";

    // [Ignore] public string DateReason => $"{DateTimeTaken} - {PictureReason}";
    [Ignore] public byte[] Image { get; set; }

    public string Status { get; set; }
    public string PicturePathOnTablet { get; set; }

    public byte[] ImgToByteArray(Stream stream)
    {
        stream.Position = 0;
        var buffer = new byte[stream.Length];
        for (var totalBytesCopied = 0; totalBytesCopied < stream.Length;)
            totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                Convert.ToInt32(stream.Length) - totalBytesCopied);

        return buffer;
    }

    public void SetImage(Stream stream)
    {
        Image = ImgToByteArray(stream);
    }
}