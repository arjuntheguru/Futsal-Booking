using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class TimeSettings : BaseEntity
    {
        public int FutsalCompanyId { get; set; }
        public TimeSpan MorningStartTime { get; set; }
        public TimeSpan MorningEndTime { get; set; }
        public TimeSpan AfternoonStartTime { get; set; }
        public TimeSpan AfternoonEndTime { get; set; }
        public TimeSpan EveningStartTime { get; set; }
        public TimeSpan EveningEndTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCompany FutsalCompany { get; set; }
    }
}
