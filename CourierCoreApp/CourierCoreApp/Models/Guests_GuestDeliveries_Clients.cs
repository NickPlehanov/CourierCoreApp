using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public class Guests_GuestDeliveries_Clients {
        public Guid GestID { get; set; }
        public DateTime? GestDateOpen { get; set; }
        public DateTime? GestDateClose { get; set; }
        public string GestName { get; set; }
        public string GestComment { get; set; }
        public Guid? CourierID { get; set; }
        public int DlvrstID { get; set; }
        public Guid? ClntID { get; set; }
        public string ClntName { get; set; }
        public string ClntPhones { get; set; }
        public string PayType { get; set; }
        public decimal PaySum { get; set; }
        //private bool _PayChecks { get; set; }
        //public bool PayCheck { 
        //    get {
        //        return Decimal.TryParse(PaySum.ToString(),out _) ? true : false;
        //    }
        //}
        public bool PayCheck { get; set; }
    }
}
