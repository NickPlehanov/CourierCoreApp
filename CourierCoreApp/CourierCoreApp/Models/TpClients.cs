using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpClients {
        public Guid ClntId { get; set; }
        public Guid ClntClgrId { get; set; }
        public Guid? ClntFirmId { get; set; }
        public Guid? ClntLggrId { get; set; }
        public Guid? ClntPeplId { get; set; }
        public Guid? ClntSprvId { get; set; }
        public Guid? ClntDelId { get; set; }
        public string ClntName { get; set; }
        public string ClntPhones { get; set; }
        public string ClntDescription { get; set; }
        public bool ClntIsDisabled { get; set; }

        public virtual TpPeople ClntPepl { get; set; }
    }
}
