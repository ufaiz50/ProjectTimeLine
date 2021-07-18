using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }
        
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        [JsonIgnore]
        public virtual ICollection<TaskHistory> TaskHistories { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountTask> AccountTasks{ get; set; }
    }
}
