using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using ProjectTimeLine.Util;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var employee = new Employee();
            var account = new Account();

            var cekNIK = myContext.Employees.Find(registerVM.NIK);
            if (cekNIK == null)
            {
                var cekEmail = myContext.Employees.FirstOrDefault(a => a.Email == registerVM.Email);
                if (cekEmail == null)
                {
                    employee.NIK = registerVM.NIK;
                    employee.Name = registerVM.Name;
                    employee.Email = registerVM.Email;
                    employee.BirthDate = registerVM.BirthDate;
                    employee.PhoneNumber = registerVM.PhoneNumber;
                    employee.Gender = (Model.Gender)registerVM.Gender;
                    employee.Address = registerVM.Address;

                    account.NIK = registerVM.NIK;
                    account.Password = Hashing.HashPassword(registerVM.Password);

                    myContext.Employees.Add(employee);
                    myContext.Accounts.Add(account);

                    var insert = myContext.SaveChanges();
                    return insert;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        public ICollection ViewRegistrasi()
        {
            var data = (from em in myContext.Employees
                        join ac in myContext.Accounts on em.NIK equals ac.NIK
                        join ar in myContext.AccountRoles on ac.NIK equals ar.NIK
                        join rl in myContext.Roles on ar.RoleID equals rl.Id
                        select new
                        {
                            em.NIK,
                            em.Name,
                            em.Email,
                            em.Gender,
                            em.BirthDate,
                            em.Address,
                            em.PhoneNumber,
                            RoleName = rl.Name
                        }).ToList();
            return data;
        }

    }
}