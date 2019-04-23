using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goalza.booking.web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public int? TeamId { get; set; }
        public int? FutsalCompanyId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
