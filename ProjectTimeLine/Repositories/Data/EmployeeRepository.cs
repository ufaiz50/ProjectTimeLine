
using Microsoft.EntityFrameworkCore;
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
            var accRole = new AccountRole();

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

                    accRole.RoleID = 7;
                    accRole.NIK = registerVM.NIK;

                    myContext.Employees.Add(employee);
                    myContext.Accounts.Add(account);
                    myContext.AccountRoles.Add(accRole);

                    var insert = myContext.SaveChanges();
                    return insert;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
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

        //Faiz

        public ICollection ViewRegistrasiNIK(string NIK)
        {
            var data = (from em in myContext.Employees
                        join ac in myContext.Accounts on em.NIK equals ac.NIK
                        join ar in myContext.AccountRoles on ac.NIK equals ar.NIK
                        join rl in myContext.Roles on ar.RoleID equals rl.Id
                        where em.NIK == NIK
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

        public int InsertUser(UserVM registerVM)
        {
            var employee = new Employee();
            var account = new Account();
            var accountRole = new AccountRole();

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

                    accountRole.NIK = registerVM.NIK;
                    accountRole.RoleID = registerVM.RoleId;

                    

                    myContext.Employees.Add(employee);
                    myContext.Accounts.Add(account);
                    myContext.AccountRoles.Add(accountRole);

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

        public int DeleteUser(string NIK)
        {
            var ar = myContext.AccountRoles.FirstOrDefault(x => x.NIK == NIK);
            if (ar == null) return 1;
            myContext.AccountRoles.Remove(ar);
            var a = myContext.Accounts.Find(NIK);
            myContext.Accounts.Remove(a);
            var e = myContext.Employees.Find(NIK);
            myContext.Employees.Remove(e);
            var delete = myContext.SaveChanges();
            return delete;
        }

        public int UpdateUser(UserVM user, string NIK)
        {
            var ar = new AccountRole();
            var e = new Employee();
            ar.NIK = user.NIK;
            ar.RoleID = user.RoleId;


            e.NIK = user.NIK;
            e.Name = user.Name;
            e.Email = user.Email;
            e.PhoneNumber = user.PhoneNumber;
            e.Address = user.Address;
            e.BirthDate = user.BirthDate;
            e.Gender = (Model.Gender)user.Gender;

            myContext.Entry(ar).State = EntityState.Modified;
            myContext.Entry(e).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;
        }

    }
}