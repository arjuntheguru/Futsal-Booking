using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class SocialMedia : BaseEntity
    {
        public int FutsalCompanyId { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCompany FutsalCompany { get; set; }
    }
}
