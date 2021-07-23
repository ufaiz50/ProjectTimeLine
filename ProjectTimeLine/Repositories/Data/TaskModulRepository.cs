using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class TaskModulRepository : GeneralRepository<MyContext, TaskModul, int>
    {
        private readonly MyContext myContext;
        public TaskModulRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public ICollection ViewTask()
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.TaskModuls on md.ModulId equals tm.ModulId
                        join at in myContext.AccountTasks on tm.TaskId equals at.TaskModulId
                        join ac in myContext.Accounts on at.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        select new
                        {
                            tm.TaskId,
                            tm.TaskName,
                            tm.Date,
                            tm.Description,
                            tm.Status,
                            tm.PriorityTask,
                            tm.ModulId,
                            md.ModulName,
                            em.Name,
                        }).ToList();
            return data;
        }

        //Faiz

        public ICollection ViewTaskId(int Id)
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.TaskModuls on md.ModulId equals tm.ModulId
                        join at in myContext.AccountTasks on tm.TaskId equals at.TaskModulId
                        join ac in myContext.Accounts on at.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        where tm.TaskId == Id
                        select new
                        {
                            tm.TaskId,
                            tm.TaskName,
                            tm.Date,
                            tm.Description,
                            tm.Status,
                            tm.PriorityTask,
                            tm.ModulId,
                            md.ModulName,
                            ac.NIK,
                            em.Name,
                        }).ToList();
            return data;
        }
        
        public ICollection ViewModulTask(int pId)
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.TaskModuls on md.ModulId equals tm.ModulId
                        join at in myContext.AccountTasks on tm.TaskId equals at.TaskModulId
                        join ac in myContext.Accounts on at.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        where md.ProjectId == pId
                        select new
                        {
                            tm.TaskId,
                            tm.TaskName,
                            tm.Date,
                            tm.Description,
                            tm.Status,
                            tm.PriorityTask,
                            tm.ModulId,
                            md.ModulName,
                            ac.NIK,
                            em.Name,
                        }).ToList();
            return data;
        }
        
        public ICollection ViewModulTask()
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.TaskModuls on md.ModulId equals tm.ModulId
                        join at in myContext.AccountTasks on tm.TaskId equals at.TaskModulId
                        join ac in myContext.Accounts on at.NIK equals ac.NIK
                        join em in myContext.Employees on ac.NIK equals em.NIK
                        select new
                        {
                            tm.TaskId,
                            tm.TaskName,
                            tm.Date,
                            tm.Description,
                            tm.Status,
                            tm.PriorityTask,
                            tm.ModulId,
                            md.ModulName,
                            ac.NIK,
                            em.Name,
                        }).ToList();
            return data;
        }

        // Update Status
        public int UpdateStatus(TaskModul taskModul)
        {
            var result = myContext.TaskModuls.Find(taskModul.TaskId);
            if (result == null) return 0;

            result.Status = taskModul.Status;
            var done = myContext.SaveChanges();
            return done;
        }
    }
}
