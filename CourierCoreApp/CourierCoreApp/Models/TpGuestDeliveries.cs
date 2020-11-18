﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpGuestDeliveries {
        public Guid GsdlvGestId { get; set; }
        public Guid? GsdlvUsrIdCourier { get; set; }
        public int GsdlvDlvrstId { get; set; }
        public int GsdlvDlvrmtId { get; set; }
        public Guid? GsdlvCncptId { get; set; }
        public Guid? GsdlvPytpIdPrepaid { get; set; }
        public DateTime GsdlvDate { get; set; }
        public DateTime? GsdlvDateDelivered { get; set; }
        public bool GsdlvIsAutoStart { get; set; }
        public int? GsdlvCookingTime { get; set; }
        public int? GsdlvDeliveryTime { get; set; }
        public bool GsdlvSendSooner { get; set; }
        public bool GsdlvNeedConfirmation { get; set; }
        public decimal? GsdlvCashAmount { get; set; }
        public string GsdlvPayDescription { get; set; }
        public string GsdlvGeoRegionId { get; set; }
        public string GsdlvGeoCoordinates { get; set; }
        public string GsdlvExtraInfo { get; set; }

        public virtual TpGuests GsdlvGest { get; set; }
        public virtual TpUsers GsdlvUsrIdCourierNavigation { get; set; }
    }
}
