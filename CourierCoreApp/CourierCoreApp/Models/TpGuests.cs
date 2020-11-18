﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpGuests {
        public TpGuests() {
            TpOrders = new HashSet<TpOrders>();
        }

        public Guid GestId { get; set; }
        public Guid GestDvsnId { get; set; }
        public Guid? GestGestIdOriginal { get; set; }
        public Guid? GestSprvId { get; set; }
        public Guid? GestClntId { get; set; }
        public Guid? GestIdntId { get; set; }
        public Guid? GestUsrId { get; set; }
        public Guid GestDevId { get; set; }
        public Guid? GestPlacId { get; set; }
        public Guid? GestLggrId { get; set; }
        public int GestGsstId { get; set; }
        public Guid? GestDevIdSalePrivilege { get; set; }
        public DateTime GestDateOpen { get; set; }
        public DateTime? GestDateClose { get; set; }
        public string GestName { get; set; }
        public string GestClientName { get; set; }
        public string GestClientPhone { get; set; }
        public string GestClientEmail { get; set; }
        public string GestClientAddress { get; set; }
        public int? GestPlaceNumber { get; set; }
        public int? GestCount { get; set; }
        public byte[] GestSalePrivilegeData { get; set; }
        public Guid? GestInformationTransactionId { get; set; }
        public string GestComment { get; set; }

        public virtual TpUsers GestUsr { get; set; }
        public virtual TpGuestDeliveries TpGuestDeliveries { get; set; }
        public virtual ICollection<TpOrders> TpOrders { get; set; }
    }
}
