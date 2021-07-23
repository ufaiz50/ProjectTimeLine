using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class ModulRepository : GeneralRepository<MyContext, Modul, int>
    {
        private readonly MyContext myContext;
        public ModulRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public ICollection ViewModul()
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.Projects on md.ProjectId equals tm.ProjectId
                        select new
                        {
                            md.ModulId,
                            md.ModulName,
                            md.Date,
                            tm.Name
                        }).ToList();
            return data;
        }

        public ICollection ViewModul(int id)
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.Projects on md.ProjectId equals tm.ProjectId
                        where md.ModulId == id
                        select new
                        {
                            md.ModulId,
                            md.ModulName,
                            md.Date,
                            tm.Name
                        }).ToList();
            return data;
        }
        
        public ICollection ViewProjectModul(int id)
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.Projects on md.ProjectId equals tm.ProjectId
                        where md.ProjectId == id
                        select new
                        {
                            md.ModulId,
                            md.ModulName,
                            md.Date,
                            tm.Name
                        }).ToList();
            return data;
        }
        
        public ICollection ViewProjectModul()
        {
            var data = (from md in myContext.Moduls
                        join tm in myContext.Projects on md.ProjectId equals tm.ProjectId
                        select new
                        {
                            md.ModulId,
                            md.ModulName,
                            md.Date,
                            tm.Name
                        }).ToList();
            return data;
        }
    }
}
