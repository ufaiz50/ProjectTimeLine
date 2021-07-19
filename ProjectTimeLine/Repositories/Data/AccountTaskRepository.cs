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
            try 
            {
                var find = myContext.AccountTasks.Where(x => x.TaskModulId == id);
                myContext.AccountTasks.RemoveRange(find);
                var delete = myContext.SaveChanges();
                return delete;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
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
                           p.EndDate
                       };
            return data;
        }

        public IQueryable GetModulTask(string NIK, int modulId)
        {

            var data = from at in myContext.AccountTasks
                       join tm in myContext.TaskModuls on at.TaskModulId equals tm.TaskId
                       join m in myContext.Moduls on tm.ModulId equals m.ModulId
                       join p in myContext.Projects on m.ProjectId equals p.ProjectId
                       where at.NIK == NIK && m.ModulId == modulId
                       select new
                       {
                           at.NIK,
                           tm.TaskId,
                           tm.TaskName,
                           tm.Status,
                           p.Name
                       };
            return data;
        }

    }
}
