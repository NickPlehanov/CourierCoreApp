using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public class TpLocations {
        public Guid LocId { get; set; }
        public Guid? LocDelId { get; set; }
        public string LocName { get; set; }
        public string LocDescription { get; set; }
    }
}
