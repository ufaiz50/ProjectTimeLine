using Client.BaseController;
using Client.Models;
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

        public async Task<IActionResult> Taskview(string NIK, int ModulId)
        {
            List<TaskModulVM> result = await repository.GetModulTask(NIK, ModulId);
            return View(result);
        }

        public async Task<JsonResult> GetProjectView(string NIK)
        {
            var result = await repository.GetProjectTask(NIK);
            var uniq = result.Distinct(new MyComparer());
           
            return Json(uniq);
        }

        public async Task<JsonResult> GetModulView(string NIK, int ModulId)
        {
            var result = await repository.GetModulTask(NIK, ModulId);
            return Json(result);
        }

        public async Task<JsonResult> GetJWT()
        {
            var result = await repository.GetJwt ();
            return Json(result);
        }

        public async Task<string> UpdateStatus(TaskModul taskModul)
        {
            var result = await repository.UpdateStatus(taskModul);
            return result;
        }
    }

    class MyComparer : IEqualityComparer<TaskProjectVM>
    {
        public bool Equals(TaskProjectVM x, TaskProjectVM y)
        {
            return x.ModulName.Equals(y.ModulName);
        }

        public int GetHashCode(TaskProjectVM obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
