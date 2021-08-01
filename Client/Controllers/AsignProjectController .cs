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
    [Authorize(Roles = "Manager")]
    public class AsignProjectController : BaseController<Project, AsignProjectRepository, int>
    {
        private readonly AsignProjectRepository repository;

        public AsignProjectController(AsignProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Project(int id)
        {
            ViewData["date"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            var project = await repository.Get(id);
            if (project == null) return RedirectToAction("Index");
            ViewData["id"] = project.ProjectId;
            ViewData["name"] = project.Name;
            var result = await repository.GetAllTask(id);
            return View(result);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetProjectView()
        {
            var result = await repository.GetProjectView();
            return Json(result);
        }

        public async Task<string> InsertProject(Project project)
        {
            var result = await repository.InsertProject(project);
            return result;
        }
        
        public async Task<string> InsertModul(Modul modul)
        {
            var result = await repository.InsertModul(modul);
            return result;
        }

        public async Task<JsonResult> GetModul(int ProjectId)
        {
            var result = await repository.GetModul(ProjectId);
            return Json(result);
        }

        public async Task<string> InsertTask(TaskModul task)
        {
            var result = await repository.InsertTask(task);
            return result;
        }

        public async Task<JsonResult> GetAllTask()
        {
            var result = await repository.GetAllTask(1);
            return Json(result);
        }

        public async Task<string> UpdateDescription(TaskModul taskModul)
        {
            var result = await repository.UpdateDescription(taskModul);
            return result;
        }

        public async Task<string> UpdateDate(TaskModul taskModul)
        {
            var result = await repository.UpdateDate(taskModul);
            return result;
        }

        public async Task<JsonResult> GetAccounts()
        {

            var result = await repository.GetAccounts();
            var re = result.OrderBy(x => x.NIK).ToList();

            for (int i = 0; i < re.Count; i++)
            {
                for (int j = 0; j < re.Count; j++)
                {
                    if (re[i].NIK == re[j].NIK)
                    {
                        re[i].AllRoleName.Add(re[j].RoleName);
                    }
                }
            }
            var final = re.GroupBy(p => p.NIK).Select(grp => grp.First()).ToArray();
            return Json(final);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetTaskAccount(int id)
        {
            var result = await repository.GetTaskAccount(id);
            return Json(result);
        }

        public async Task<string> InsertMember(AccountTask accountTask)
        {
            var result = await repository.InsertMember(accountTask);
            return result;
        }
        public async Task<string> DeleteMember(string NIK, int TaskModulId)
        {
            var result = await repository.DeleteMember(NIK, TaskModulId);
            return result;
        }
    }
}
