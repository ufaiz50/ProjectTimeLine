using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class LogStatusVM
    {
        public string Name { get; set; }
        public string TaskName { get; set; }
        public Status StateAfter { get; set; }
        public DateTime EndDate { get; set; }
    }
}
