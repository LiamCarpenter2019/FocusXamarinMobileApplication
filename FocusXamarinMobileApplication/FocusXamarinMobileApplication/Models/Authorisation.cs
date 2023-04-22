using System.Collections.Generic;

namespace FocusXamarinMobileApplication.Models
{
    public class Authorisation
    {
        public enum AuthorisationType { CHANGE_PIN,PIN_CONFIRM,PIN_SIGNITURE}
        public AuthorisationType AuthType { get; set; }

        public enum ReasonType { OPERATIVE, SUPERVISOR };
        public ReasonType Reason { get; set; }

       // public List<SignatureDetail> Signatures { get; set; }
        public bool PinAutorised { get; set; }
    }
}
