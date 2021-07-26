using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class LatesTaskVM
    {
        public string NIK { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
    }
}
