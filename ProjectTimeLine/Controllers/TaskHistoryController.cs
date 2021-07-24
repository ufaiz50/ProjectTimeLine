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
    public class TaskHistoryController : BaseController<TaskHistory, TaskHistoryRepository, int>
    {
        private readonly TaskHistoryRepository repository;
        public TaskHistoryController(TaskHistoryRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("LogStatus/{key}")]
        public ActionResult ViewTaskId(int key)
        {
            try
            {
                var view = repository.LogStatus(key);
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
