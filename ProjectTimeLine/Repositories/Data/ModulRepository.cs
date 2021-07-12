using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class ModulRepository : GeneralRepository<MyContext, Modul, int>
    {
        public ModulRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
