#region

using GalaSoft.MvvmLight.Messaging;

#endregion

namespace FocusXamarinMobileApplication.Models;

public class MessageConfirmation : MessageBase
{
    public long OperativeId { get; set; }
}