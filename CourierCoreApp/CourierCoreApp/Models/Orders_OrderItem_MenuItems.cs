using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public class Orders_OrderItem_MenuItems {
        public Guid OrdrGestID { get; set; }
        public Guid OrdrID { get; set; }
        public Guid OrdrMitmID { get; set; }
        public decimal OritVolume { get; set; }
        public int OritCount { get; set; }
        public decimal OritPrice { get; set; }
        public string MitmName { get; set; }
    }
}
