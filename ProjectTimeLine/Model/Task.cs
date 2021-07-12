using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public PriorityTask PriorityTask { get; set; }

        public Modul Modul { get; set; }
        public IList<TaskHistory> TaskHistories { get; set; }
    }

    public enum Status
    {

    }

    public enum PriorityTask
    {

    }
}
