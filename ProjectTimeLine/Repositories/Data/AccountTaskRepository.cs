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
    }
}
