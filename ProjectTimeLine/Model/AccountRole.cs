using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Model
{
    public class AccountRole
    {
        [Key]
        public string NIK { get; set; }
        public int RoleID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Role Role { get; set; }
    }
}
