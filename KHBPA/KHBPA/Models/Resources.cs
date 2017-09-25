using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KHBPA.Models
{
    public class Resources
    {
        public int Id { get; set; }
        public int ResourceType { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDesc { get; set; }
    }
}