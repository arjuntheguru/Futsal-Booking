using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class Gallery : BaseEntity
    {
        public int FustalCourtId { get; set; }
        public string ImageSrc { get; set; }
        public string Caption { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FutsalCourt FutsalCourt { get; set; }
    }
}
