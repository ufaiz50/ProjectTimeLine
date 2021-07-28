using Microsoft.EntityFrameworkCore;
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
        private readonly MyContext myContext;
        public AccountTaskRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int DeleteTaskMember(int id)
        {
            //var query = myContext.AccountTasks.AsEnumerable().Where( x => x.TaskModulId == id);

            var query = (from at in myContext.AccountTasks
                         where at.TaskModulId == id
                         select at).ToList();

            foreach (var row in query.ToList())
            {
                myContext.AccountTasks.Remove(row);
            }

            //var find = myContext.AccountTasks.Where(x => x.TaskModulId == id);
            //myContext.AccountTasks.RemoveRange(find);

            var delete = myContext.SaveChanges();
            return delete;
        }

        public IQueryable GetProjectTask(string NIK)
        {

            var data = from at in myContext.AccountTasks
                       join tm in myContext.TaskModuls on at.TaskModulId equals tm.TaskId
                       join m in myContext.Moduls on tm.ModulId equals m.ModulId
                       join p in myContext.Projects on m.ProjectId equals p.ProjectId
                       where at.NIK == NIK
                       select new {
                           at.NIK,
                           p.Name,
                           m.ModulId,
                           m.ModulName,
                           p.StartDate,
                           p.EndDate,
                           p.ProjectId
                       };
            return data;
        }

        public IQueryable GetModulTask(string NIK, int ProjectId)
        {

            var data = from at in myContext.AccountTasks
                       join tm in myContext.TaskModuls on at.TaskModulId equals tm.TaskId
                       join m in myContext.Moduls on tm.ModulId equals m.ModulId
                       join p in myContext.Projects on m.ProjectId equals p.ProjectId
                       where at.NIK == NIK && p.ProjectId == ProjectId
                       select new
                       {
                           at.NIK,
                           m.ModulId,
                           m.ModulName,
                           tm.TaskId,
                           tm.TaskName,
                           tm.Status,
                           tm.StartDate,
                           tm.Date,
                           p.Name,
                           p.ProjectId
                       };
            return data;
        }

        public IQueryable GetTask(string NIK)
        {

            var data = from at in myContext.AccountTasks
                       join tm in myContext.TaskModuls on at.TaskModulId equals tm.TaskId
                       join m in myContext.Moduls on tm.ModulId equals m.ModulId
                       join p in myContext.Projects on m.ProjectId equals p.ProjectId
                       where at.NIK == NIK
                       select new
                       {
                           at.NIK,
                           tm.TaskId,
                           tm.TaskName,
                           tm.StartDate,
                           p.Name,
                           p.ProjectId
                       };
            return data;
        }

    }
}
