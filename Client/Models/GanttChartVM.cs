using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class GanttChartVM
    {
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ModulName { get; set; }
        public DateTime ModulStartDate { get; set; }
        public DateTime ModulEndDate { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
    }
}
