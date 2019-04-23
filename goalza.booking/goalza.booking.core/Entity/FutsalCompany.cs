using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class FutsalCompany : BaseEntity
    {
        public string Name { get; set; }
        public string RegistrationNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Description { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TimeSettings TimeSettings { get; set; }
        public virtual SocialMedia SocialMedia { get; set; }

        public virtual IList<FutsalCourt> FutsalCourts { get; set; }

    }
}
