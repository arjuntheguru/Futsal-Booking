using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goalza.booking.web.Models
{
    public class Notification
    {
        public Notification()
        {

        }

        public Notification(string Type, string Message)
        {
            this.Type = Type;
            this.Message = Message;
        }

        public string Type { get; set; }
        public string Message { get; set; }
    }
}
