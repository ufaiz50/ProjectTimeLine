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

        public string ViewProject(int id)
        {
            var result =  "asiap"+id;
            return result;
        }
    }
}
