﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CourierCoreApp.Models {
    public partial class TpPeople {
        public TpPeople() {
            TpClients = new HashSet<TpClients>();
            TpUsers = new HashSet<TpUsers>();
        }

        public Guid PeplId { get; set; }
        public Guid PeplPpgrId { get; set; }
        public int? PeplSexId { get; set; }
        public Guid? PeplLggrId { get; set; }
        public Guid? PeplDelId { get; set; }
        public DateTime? PeplDateBirthday { get; set; }
        public string PeplFirstName { get; set; }
        public string PeplSecondName { get; set; }
        public string PeplPatronymic { get; set; }
        public string PeplDocument { get; set; }
        public string PeplWorkplace { get; set; }
        public string PeplPosition { get; set; }
        public string PeplPhoneWork { get; set; }
        public string PeplPhoneHome { get; set; }
        public string PeplPhoneCell { get; set; }
        public string PeplEmail { get; set; }
        public string PeplPhotoFileName { get; set; }
        public byte[] PeplPhoto { get; set; }
        public string PeplInn { get; set; }
        public string PeplComment { get; set; }

        public virtual ICollection<TpClients> TpClients { get; set; }
        public virtual ICollection<TpUsers> TpUsers { get; set; }
    }
}
