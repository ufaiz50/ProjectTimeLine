using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class ProjectRepository : GeneralRepository<MyContext, Project, int>
    {
        private readonly MyContext myContext;
        public ProjectRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IQueryable GanttChartView(int ProjectId)
        {

            var data = from tm in myContext.TaskModuls
                       join m in myContext.Moduls on tm.ModulId equals m.ModulId
                       join p in myContext.Projects on m.ProjectId equals p.ProjectId
                       where p.ProjectId == ProjectId
                       select new {
                           ProjectName = p.Name,
                           p.StartDate,
                           p.EndDate,
                           m.ModulName,
                           ModulStartDate = m.StartDate,
                           ModulEndDate = m.Date,
                           tm.TaskName,
                           TaskStartDate = tm.StartDate,
                           TaskEndDate = tm.Date
                       };

            return data;
        }
    }
}
