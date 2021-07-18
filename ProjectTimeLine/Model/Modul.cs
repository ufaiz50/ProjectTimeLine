using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class Modul
    {
        public int ModulId { get; set; }
        public string ModulName { get; set; }
        public DateTime Date { get; set; }
        public int ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }
        [JsonIgnore]
        public virtual ICollection<TaskModul> TaskModuls { get; set; }

    }
}
