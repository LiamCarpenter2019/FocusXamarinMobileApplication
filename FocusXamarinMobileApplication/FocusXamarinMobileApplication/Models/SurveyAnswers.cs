#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class SurveyAnswers : BusinessEntityBase
{
    public long QNumber { get; set; } = 0;
    public string StreetName { get; set; } = "Unavailable";
    public string LocationName { get; set; } = "";
    public decimal QuestionId { get; set; } = 0;
    public string AnswerGiven { get; set; } = "";
    public string Notifiable { get; set; } = "false";
    public string CompletedById { get; set; } = "";
    public DateTime SubmittedDateTime { get; set; } = DateTime.Now;
    public long RemoteTableId { get; set; } = 0;
    public string Complete { get; set; } = "";
    public string Category { get; set; } = "";
    public Guid? AssignmentId { get; set; } = Guid.Empty;
    public Guid? Identifier { get; set; } = Guid.NewGuid();
    public decimal Rating { get; set; } = 0;
    public string GpsCoordinates { get; set; } = "";
    public string AppStatus { get; set; } = "IPad";

    [Ignore] public List<SurveyComments> ResponseComments { get; set; } = new();
    [Ignore] public List<Pictures4Tablet> ResponsePictures { get; set; } = new();
}