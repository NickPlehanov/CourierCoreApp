﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpOrders {
        public TpOrders() {
            TpOrderItems = new HashSet<TpOrderItems>();
        }

        public Guid OrdrId { get; set; }
        public Guid OrdrGestId { get; set; }
        public Guid OrdrUsrId { get; set; }
        public Guid OrdrUsrIdOperator { get; set; }
        public Guid OrdrDevId { get; set; }
        public Guid? OrdrDvsnId { get; set; }
        public Guid? OrdrArchId { get; set; }
        public Guid? OrdrSprvId { get; set; }
        public int OrdrOrstId { get; set; }
        public DateTime OrdrDate { get; set; }
        public string OrdrName { get; set; }
        public byte[] OrdrExternalTransactionId { get; set; }

        public virtual TpGuests OrdrGest { get; set; }
        public virtual TpUsers OrdrUsr { get; set; }
        public virtual TpUsers OrdrUsrIdOperatorNavigation { get; set; }
        public virtual ICollection<TpOrderItems> TpOrderItems { get; set; }
    }
}
