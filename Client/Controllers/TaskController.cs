using Client.BaseController;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class TaskController : BaseController<Account, TaskRepository, string>
    {
        private readonly TaskRepository repository;

        public TaskController(TaskRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Baview()
        {
            return View();
        }

        public async Task<JsonResult> GetProjectView()
        {
            var result = await repository.Get("E0001");
            return Json(result);
        }

        public async Task<JsonResult> GetJWT()
        {
            var result = await repository.GetJwt ();
            return Json(result);
        }
    }
}
