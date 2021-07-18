using Client.BaseController;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountController : BaseController<Employee, AccountRepository, string>
    {
        private readonly AccountRepository repository;

        public AccountController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetJWTNIK()
        {
            //var result = await repository.UpdateEmployee(userVM);

            var result = await repository.GetJWTNIK();
            return Json(result);
        }

        public async Task<JsonResult> GetEmployee(string nik)
        {
            //var result = await repository.UpdateEmployee(userVM);

            var result = await repository.Get(nik);
            return Json(result);
        }

        public async Task<string> UpdateEmployee(Employee employee)
        {
            //var result = await repository.UpdateEmployee(userVM);

            var result = await repository.UpdateEmployee(employee);
            return result;
        }
    }
}
