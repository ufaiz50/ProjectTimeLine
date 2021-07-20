using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class TaskModul
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        public PriorityTask PriorityTask { get; set; }
        public int ModulId { get; set; }

        public virtual Modul Modul { get; set; }
        [JsonIgnore]
        public virtual ICollection<TaskHistory> TaskHistories { get; set; }
        public virtual ICollection<AccountTask> AccountTasks { get; set; }
    }

    public enum Status
    {
        Todo,
        Design,
        Doing,
        CodeReview,
        Testing,
        Done
    }

    public enum PriorityTask
    {
        Priority,
        Medium,
        Normal
    }
}
