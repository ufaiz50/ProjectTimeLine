using Client.BaseController;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class DashboardController : BaseController<Employee, UserdataRepository, string>
    {
        private readonly UserdataRepository repository;

        public DashboardController(UserdataRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Userdata()
        {
            return View();
        }

        public async Task<JsonResult> GetRegistrasiView()
        {
            var result = await repository.GetRegistrasiView();
            return Json(result);
        }

        public async Task<JsonResult> GetEmployeeView()
        {
            var result = await repository.GetEmployeeView();
            return Json(result);
        }
        public async Task<JsonResult> GetUserDataView(string NIK)
        {
            var result = await repository.GetUserDataView(NIK);
            return Json(result);
        }

        public async Task<string> InsertEmployee(UserVM userVM)
        {
            /*var tahunlahir = Convert.ToDateTime("2020-01-01");
            var user = new UserVM("E0003", "Iza", "iza@gmail.com", "08123456778", tahunlahir, "Jakarta", 0, "Password", 1);*/
            var result = await repository.InsertEmployee(userVM);
            return result;
        }

        public async Task<string> UpdateEmployee(UserVM userVM)
        {
            var result = await repository.UpdateEmployee(userVM);
            return result;
        }

        public async Task<string> DeleteEmployee(string NIK)
        {
            var result = await repository.DeleteEmployee(NIK);
            return result;
        }
    }
}
