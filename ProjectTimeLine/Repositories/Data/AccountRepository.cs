using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectTimeLine.Context;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var genValidation = Util.Hashing.ValidatePassword(login.Password, checkNIK.Password);
            return genValidation ? 1 : 3;
        }

        public IQueryable<DataLoginVM> GetDataLogin(LoginVM login)
        {

            var data = from em in myContext.Employees
                       join acc in myContext.Accounts on em.NIK equals acc.NIK
                       join accrole in myContext.AccountRoles on acc.NIK equals accrole.NIK
                       join role in myContext.Roles on accrole.RoleID equals role.Id
                       where em.Email == login.Email
                       select new DataLoginVM(em.NIK, em.Name, em.Email, role.Name );
            
            return data;
        }

        public int UpdatePassword(UpdatePasswordVM updatePassword)
        {
            var acc = myContext.Accounts.SingleOrDefault(a => a.NIK == updatePassword.NIK);
            if (acc == null) return 0;

            var checkPassword = Util.Hashing.ValidatePassword(updatePassword.OldPassword, acc.Password);
            if (!checkPassword) return 2;

            var getPassword = Util.Hashing.HashPassword(updatePassword.NewPassword);

            acc.Password = getPassword;


            myContext.Entry(acc).State = EntityState.Modified;
            var update = myContext.SaveChanges();
            return update;
        }

        public int ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            Guid guid = Guid.NewGuid();
            string emailGuid = guid.ToString("N");

            var account = new Account();

            var email = myContext.Employees.FirstOrDefault(a => a.Email == resetPasswordVM.Email);
            if (email != null)
            {
                account.NIK = email.NIK;
                account.Password = Util.Hashing.HashPassword(emailGuid);
                myContext.Entry(account).State = EntityState.Modified;
                var insert = myContext.SaveChanges();

                if (insert > 0)
                {
                    var fromAddress = new MailAddress("pandanaran1000@gmail.com", "Project Timeline");
                    var toAddress = new MailAddress(resetPasswordVM.Email, $"To {resetPasswordVM.Email}");
                    string fromPassword = "!23456Qwerty";
                    string subject = "Reset Password";
                    string body = "Hello " + email.Name + System.Environment.NewLine + "Ini password baru anda : " + emailGuid;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    return insert;

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
