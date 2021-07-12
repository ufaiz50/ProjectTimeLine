using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class TaskHistory
    {
        public int TaskHistoryId { get; set; }
        public DateTime EndDate { get; set; }
        public int StateBefore { get; set; }
        public int StateAfter { get; set; }

        public TaskModul TaskModul { get; set; }

        public Account Account { get; set; }
    }
}
