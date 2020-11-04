using System;

namespace CourierCoreApp.Models {
    public class TpUsers {
        public Guid UsrId { get; set; }
        public Guid UsrUsrrId { get; set; }
        public Guid? UsrLggrId { get; set; }
        public Guid? UsrPeplId { get; set; }
        public Guid? UsrDelId { get; set; }
        public string UsrName { get; set; }
        public string UsrDescription { get; set; }
        public string UsrDocumentNameSignature { get; set; }
        public string UsrLogin { get; set; }
        public byte[] UsrPassword { get; set; }
        public string UsrQuickCode { get; set; }
        public bool UsrSimpleLogonEnabled { get; set; }
        public bool UsrIsDisabled { get; set; }
        public bool UsrIsPortalLogin { get; set; }
    }
}
