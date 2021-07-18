using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class AccountTaskRepository : GeneralRepository<MyContext, AccountTask, string>
    {
        public AccountTaskRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
