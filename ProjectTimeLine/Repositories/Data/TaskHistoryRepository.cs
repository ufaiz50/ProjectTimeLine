using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class TaskHistoryRepository : GeneralRepository<MyContext, TaskHistory, int>
    {
        private readonly MyContext myContext;
        public TaskHistoryRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public ICollection LogStatus(int id)
        {
            var data = (from tm in myContext.TaskModuls 
                        join th in myContext.TaskHistories on tm.TaskId equals th.TaskModulId
                        join ac in myContext.Accounts on th.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        where tm.TaskId == id
                        select new
                        {
                            th.StateAfter,
                            th.EndDate,
                            em.Name,
                        }).ToList();
            return data;
        }
        public ICollection LastActivity(string NIK)
        {
            var data = (from tm in myContext.TaskModuls
                        join th in myContext.TaskHistories on tm.TaskId equals th.TaskModulId
                        join ac in myContext.Accounts on th.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        where ac.NIK == NIK
                        select new
                        {
                            tm.TaskName,
                            th.StateAfter,
                            th.EndDate,
                            em.Name,
                        }).ToList();
            return data;
        }
    }
}
