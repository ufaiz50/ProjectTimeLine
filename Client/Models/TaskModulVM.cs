using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class TaskModulVM
    {
        public string NIK { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
    }
}
