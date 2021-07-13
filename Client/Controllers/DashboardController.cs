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
    }
}
