using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpUserLocationPresence {
        public Guid UslpId { get; set; }
        public Guid UslpUsrId { get; set; }
        public Guid UslpLocId { get; set; }
        public Guid? UslpUsrIdBegin { get; set; }
        public Guid? UslpUsrIdEnd { get; set; }
        public DateTime UslpDateBegin { get; set; }
        public DateTime? UslpDateEnd { get; set; }
    }
}
