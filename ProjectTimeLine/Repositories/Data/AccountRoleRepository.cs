using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, string>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Userdata(string NIK, int id)
        {
            var ar = myContext.AccountRoles.FirstOrDefault(x => x.NIK == NIK && x.RoleID == id);
            if (ar == null) return 1;
            myContext.AccountRoles.Remove(ar);
            var delete = myContext.SaveChanges();
            return delete;
        }
    }
}
