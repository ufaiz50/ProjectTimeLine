using Microsoft.AspNetCore.Authorization;
using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Login(LoginVM login)
        {
            var checkNIK = myContext.Accounts.FirstOrDefault(x => x.Employee.Email == login.Email);
            if (checkNIK == null) return 2;
            var genValidation = Util.Hashing.ValidatePassword(login.password, checkNIK.Password);
            return genValidation ? 1 : 3;
        }

        public IQueryable<DataLoginVM> GetDataLogin(LoginVM login)
        {

            var data = from em in myContext.Employees
                       join acc in myContext.Accounts on em.NIK equals acc.NIK
                       join accrole in myContext.AccountRoles on acc.NIK equals accrole.NIK
                       join role in myContext.Roles on accrole.RoleID equals role.Id
                       where em.Email == login.Email
                       select new DataLoginVM(em.Name, em.Email, role.Name );
            
            return data;
        }


    }
}
