using System;
using System.Collections.Generic;
using System.Text;

namespace goalza.booking.core.Entity
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public int PlayersCount { get; set; }
        public string Captain { get; set; }
        public string Manager { get; set; }
        public string Logo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
