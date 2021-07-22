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
    public class GanttChartController : BaseController<Project, GanttChartRepository, int>
    {
        private readonly GanttChartRepository repository;

        public GanttChartController(GanttChartRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var Role = await repository.GetJwt();
            ViewBag.Roles = Role.AllRole;
            var getProject = await repository.Get();
            return View(getProject);
        }

        public async Task<List<GanttChartVM>> GanttChartView(int ProjectId)
        {
            var result = await repository.GanttChartView(ProjectId);
            return result;
        }

    }
}
