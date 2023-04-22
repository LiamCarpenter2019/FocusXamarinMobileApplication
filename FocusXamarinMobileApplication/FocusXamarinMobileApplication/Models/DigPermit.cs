#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class DigPermit : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public Guid PermitId { get; set; } = Guid.NewGuid();
    public Guid? AssignmentId { get; set; } = Guid.Empty;
    public string FromAddress { get; set; } = "";
    public string ToAddress { get; set; } = "";
    public string GpsStart { get; set; } = "";
    public string GpsEnd { get; set; } = "";
    public decimal Distance { get; set; } = 0;
    public bool Granted { get; set; } = true;
    public string OperativeFocusId { get; set; } = "0";
    public string OperativeSignature { get; set; } = "";
    public string SupervisorFocusId { get; set; } = "0";
    public string SupervisorSignature { get; set; } = "";
    public string PermitType { get; set; } = "PermitToDig";

    [Ignore] public Tuple<List<SurveyAnswers>, List<SurveyComments>> Answers { get; set; }

    [Ignore] public List<AuthorisationDetail> SignatureDetails { get; set; }

    /// <summary>
    ///     Updates the operative and supervisor focusId and signatures from the SignatureDetails list
    /// </summary>
    public void UpdateFromSig()
    {
        if (SignatureDetails != null && SignatureDetails.Count == 2)
        {
            if (SignatureDetails[0].FocusId == NavigationalParameters.CurrentSelectedJob.GangLeaderId)
            {
                OperativeFocusId = SignatureDetails[0].FocusId.ToString();
                OperativeSignature = SignatureDetails[0].SignatureFileName;
                SupervisorFocusId = SignatureDetails[1].FocusId.ToString();
                SupervisorSignature = SignatureDetails[1].SignatureFileName;
            }
            else
            {
                OperativeFocusId = SignatureDetails[1].FocusId.ToString();
                OperativeSignature = SignatureDetails[1].SignatureFileName;
                SupervisorFocusId = SignatureDetails[0].FocusId.ToString();
                SupervisorSignature = SignatureDetails[0].SignatureFileName;
            }
        }
    }
}