﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpMenuVolumeTypes {
        public TpMenuVolumeTypes() {
            TpMenuItems = new HashSet<TpMenuItems>();
            TpOrderItems = new HashSet<TpOrderItems>();
        }

        public Guid MvtpId { get; set; }
        public Guid? MvtpDelId { get; set; }
        public string MvtpName { get; set; }
        public string MvtpDescription { get; set; }
        public string MvtpTypeName { get; set; }
        public int? MvtpPrecision { get; set; }
        public string MvtpFormat { get; set; }

        public virtual ICollection<TpMenuItems> TpMenuItems { get; set; }
        public virtual ICollection<TpOrderItems> TpOrderItems { get; set; }
    }
}
