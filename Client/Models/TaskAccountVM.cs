using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class TaskAccountVM
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        public PriorityTask PriorityTask { get; set; }
        public string NIK { get; set; }
        public string Name { get; set; }
    }
}
