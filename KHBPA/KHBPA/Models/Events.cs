using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class Events
    {
        public int ID { get; set; }

        public string EventName { get; set; }

        public string EventLocation { get; set; }

        public DateTime EventStartDate { get; set; }

        public DateTime EventEndDate { get; set; }

        public string EventDescription { get; set; }
    }
}