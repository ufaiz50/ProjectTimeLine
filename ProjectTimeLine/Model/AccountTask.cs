using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class AccountTask
    {
        public string NIK { get; set; }
        public int TaskModulId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual TaskModul TaskModul { get; set; }
    }
}
