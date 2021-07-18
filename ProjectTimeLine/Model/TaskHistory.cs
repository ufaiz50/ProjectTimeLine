using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class TaskHistory
    {
        public int TaskHistoryId { get; set; }
        public DateTime EndDate { get; set; }
        public int StateBefore { get; set; }
        public int StateAfter { get; set; }
        public string NIK { get; set; }
        public int TaskModulId { get; set; }

        [JsonIgnore]
        public virtual TaskModul TaskModul { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
