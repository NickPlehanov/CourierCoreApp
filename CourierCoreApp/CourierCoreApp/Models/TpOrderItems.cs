﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpOrderItems {
        public Guid OritId { get; set; }
        public Guid OritOrdrId { get; set; }
        public Guid? OritMasterId { get; set; }
        public Guid OritMitmId { get; set; }
        public Guid OritMvtpId { get; set; }
        public Guid? OritMmgrId { get; set; }
        public Guid OritSlgrId { get; set; }
        public Guid? OritSprvId { get; set; }
        public Guid? OritPcitId { get; set; }
        public decimal OritVolume { get; set; }
        public int OritCount { get; set; }
        public decimal OritPrice { get; set; }
        public decimal OritPriceDiscount { get; set; }
        public decimal OritPriceMargin { get; set; }
        public decimal OritVat { get; set; }
        public decimal OritPriceVat { get; set; }
        public int? OritCourse { get; set; }
        public int? OritSeatNumber { get; set; }
        public string OritComment { get; set; }
        public int? OritOrder { get; set; }
        public bool OritIsUnspentCredit { get; set; }
        public string OritMarkCode { get; set; }

        public virtual TpMenuItems OritMitm { get; set; }
        public virtual TpMenuVolumeTypes OritMvtp { get; set; }
        public virtual TpOrders OritOrdr { get; set; }
    }
}
