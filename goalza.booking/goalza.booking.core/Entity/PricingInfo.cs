using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class PricingInfo : BaseEntity
    {
        public int FutsalCourtId { get; set; }
        public string Day { get; set; }
        public decimal Morning { get; set; }
        public decimal AfterNoon { get; set; }
        public decimal Evening { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCourt FutsalCourt { get; set; }
    }
}
