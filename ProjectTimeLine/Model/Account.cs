﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }
        
        public virtual Employee Employee { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<TaskHistory> TaskHistories { get; set; }
        public virtual ICollection<AccountTask> AccountTasks{ get; set; }
    }
}
