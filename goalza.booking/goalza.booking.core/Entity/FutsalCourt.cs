using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class FutsalCourt : BaseEntity
    {
        public string Name { get; set; }
        public int FutsalCompanyId { get; set; }
        public string Dimension { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCompany FutsalCompany { get; set; }
        public virtual PricingInfo PricingInfo { get; set; }

        public virtual IList<Gallery> Gallery { get; set; }
        public virtual IList<Booking> Bookings { get; set; }
      
    }
}
