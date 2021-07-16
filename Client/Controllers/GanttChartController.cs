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
    public class GanttChartController : BaseController<Account, TaskRepository, string>
    {
        private readonly TaskRepository repository;

        public GanttChartController(TaskRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
