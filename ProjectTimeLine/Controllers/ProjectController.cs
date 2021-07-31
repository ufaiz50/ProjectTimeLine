using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.BaseController;
using ProjectTimeLine.Model;
using ProjectTimeLine.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectTimeLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController<Project, ProjectRepository, int>
    {
        private readonly ProjectRepository repository;
        public ProjectController(ProjectRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GanttChart/{Id}")]
        public ActionResult GanttChartView(int Id)
        {
            try
            {
                var view = repository.GanttChartView(Id);
                if (view != null)
                {
                    return Ok(view);
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = view, message = "Data tidak ditemukan" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Data tidak ditemukan" });
            }
        }

        [HttpGet("GetAllTask/{id}")]
        public ActionResult GetAllTask(int id)
        {
            try
            {
                var view = repository.GetAllTask(id);
                if (view != null)
                {
                    return Ok(view);
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = view, message = "Data Registrasi tidak ditemukan" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Data Registrasi tidak ditemukan" });
            }
        }
    }
}
