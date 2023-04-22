#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class SurveyComments : BusinessEntityBase
{
    public decimal QuestionId { get; set; } = 0;
    public long QNumber { get; set; } = 0;
    public string Text { get; set; } = "";
    public string CompletedById { get; set; } = "";
    public string StreetName { get; set; } = "";
    public string FromAddress { get; set; } = "";
    public string ToAddress { get; set; } = "";
    public DateTime SubmittedDateTime { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public long RemoteTableId { get; set; } = 0;
    public string Category { get; set; } = "";
    public Guid? AssignmentId { get; set; } = Guid.Empty;
    public Guid? Identifier { get; set; } = Guid.Empty;

    public Guid WorksIdForDelete { get; set; } = Guid.NewGuid();
}