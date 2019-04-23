using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class Booking : BaseEntity
    {
        public int FutsalCourtId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCourt FutsalCourt { get; set; }
        public virtual PartialPayment PartialPayment { get; set; }
    }
}
