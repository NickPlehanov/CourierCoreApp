﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpMenuItems {
        public TpMenuItems() {
            TpOrderItems = new HashSet<TpOrderItems>();
        }

        public Guid MitmId { get; set; }
        public Guid? MitmMitmIdAncestor { get; set; }
        public Guid MitmMgrpId { get; set; }
        public int? MitmMiclId { get; set; }
        public Guid? MitmMtypId { get; set; }
        public Guid? MitmMsrvId { get; set; }
        public Guid? MitmMvtpId { get; set; }
        public Guid? MitmMmodId { get; set; }
        public Guid? MitmMsprId { get; set; }
        public Guid? MitmPbfmId { get; set; }
        public Guid? MitmMpicId { get; set; }
        public int? MitmMifsctId { get; set; }
        public Guid? MitmDelId { get; set; }
        public string MitmName { get; set; }
        public string MitmShortName { get; set; }
        public string MitmArticle { get; set; }
        public string MitmDescription { get; set; }
        public decimal? MitmPrice { get; set; }
        public decimal? MitmVolume { get; set; }
        public decimal? MitmVat { get; set; }
        public string MitmQuickCode { get; set; }
        public string MitmPhotoFileName { get; set; }
        public byte[] MitmPhoto { get; set; }
        public bool? MitmIsInheritIdentifiers { get; set; }
        public bool? MitmIsInheritTariffs { get; set; }
        public bool? MitmIsShortcut { get; set; }
        public bool? MitmIsDisabled { get; set; }
        public int? MitmOrder { get; set; }

        public virtual TpMenuVolumeTypes MitmMvtp { get; set; }
        public virtual ICollection<TpOrderItems> TpOrderItems { get; set; }
    }
}
