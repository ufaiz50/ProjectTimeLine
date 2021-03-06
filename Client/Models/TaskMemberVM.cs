using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class TaskMemberVM
    {
        public TaskMemberVM()
        {
            Member = new List<string>();
            NIKMember = new List<string>();
        }

        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime Date{ get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PriorityTask PriorityTask { get; set; }

        public int ModulId { get; set; }
        public string ModulName{ get; set; }
        public string Name{ get; set; }
        public string NIK{ get; set; }
        public IList<string> Member { get; set; }
        public IList<string> NIKMember { get; set; }
    }

    public enum Status
    {
        ToDo,
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
