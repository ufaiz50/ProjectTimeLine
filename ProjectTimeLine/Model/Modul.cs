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
        public int ProjectId { get; set; }
        
        public virtual Project Project { get; set; }
        
        public virtual ICollection<TaskModul> TaskModuls { get; set; }

    }
}
