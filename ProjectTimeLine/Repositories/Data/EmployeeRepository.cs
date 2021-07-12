using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
