using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class TaskProjectVM
    {
        public string NIK { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int ModulId { get; set; }
        public string ModulName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
