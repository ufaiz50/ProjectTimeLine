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
    [Authorize(Roles ="SA,BA,Developer,QA")]
    public class TaskController : BaseController<Account, TaskRepository, string>
    {
        private readonly TaskRepository repository;
        private readonly TaskHistoryRepository historyRepository;

        public TaskController(TaskRepository repository, TaskHistoryRepository repository1) : base(repository)
        {
            this.repository = repository;
            historyRepository = repository1;
        }

        public async Task<IActionResult> Index()
        {
            var Role = await repository.GetJwt();
            ViewBag.Roles = Role.AllRole;
            return View();
        }

        public async Task<IActionResult> Taskview(string NIK, int ProjectId)
        {
            List<TaskModulVM> result = await repository.GetModulTask(NIK, ProjectId);
            return View(result);
        }

        public async Task<JsonResult> GetProjectView(string NIK)
        {
            var result = await repository.GetProjectTask(NIK);
            var uniq = result.Distinct(new MyComparer());
           
            return Json(uniq);
        }

        [HttpGet("/{NIK}/{ModulId}")]
        public async Task<JsonResult> GetModulView(string NIK, int ModulId)
        {
            var result = await repository.GetModulTask(NIK, ModulId);
            return Json(result);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetJWT()
        {
            var result = await repository.GetJwt ();
            return Json(result);
        }

        public async Task<string> DeleteTaskMember(int id)
        {
            var message = await repository.DeleteTaskMember(id);
            return message;
        }
        
        public async Task<string> UpdateStatus(TaskModul taskModul)
        {
            var result = await repository.UpdateStatus(taskModul);
            return result;
        }

        public async Task<JsonResult> AddHistory(TaskHistory taskHistory)
        {
            var result = await repository.AddHistory(taskHistory);
            return Json(result);
        }

        [AllowAnonymous]
        public async Task<JsonResult> LogStatus(int id)
        {
            var result = await repository.LogStatus(id);
            return Json(result);
        }

        [AllowAnonymous]
        public async Task<JsonResult> DetailTask(int id)
        {
            var result = await repository.DetailTask(id);
            return Json(result);
        }
    }

    class MyComparer : IEqualityComparer<TaskProjectVM>
    {
        public bool Equals(TaskProjectVM x, TaskProjectVM y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(TaskProjectVM obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
