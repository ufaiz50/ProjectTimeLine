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
        public ProjectRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
