using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class TaskHistoryRepository : GeneralRepository<MyContext, TaskHistory, int>
    {
        public TaskHistoryRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
