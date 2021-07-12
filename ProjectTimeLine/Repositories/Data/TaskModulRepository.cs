using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class TaskModulRepository : GeneralRepository<MyContext, TaskModul, int>
    {
        public TaskModulRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
