using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class Modul
    {
        public int ModulId { get; set; }
        public int ModulName { get; set; }
        public int Date { get; set; }
        
        public Project Project { get; set; }

        public IList<Task> Tasks { get; set; }
    }
}
