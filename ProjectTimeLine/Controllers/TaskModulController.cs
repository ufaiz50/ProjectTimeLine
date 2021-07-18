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
    public class TaskModulController : BaseController<TaskModul, TaskModulRepository, int>
    {
        private readonly TaskModulRepository taskModulRepository;

        public TaskModulController(TaskModulRepository taskModulRepository) : base(taskModulRepository)
        {
            this.taskModulRepository = taskModulRepository;
        }

        [HttpGet("ViewTask")]
        public ActionResult ViewTask()
        {
            try
            {
                var view = taskModulRepository.ViewTask();
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

        [HttpGet("ViewTask/{key}")]
        public ActionResult ViewTaskNIK(int key)
        {
            try
            {
                var view = taskModulRepository.ViewTaskId(key);
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

        [HttpPut("UpdateStatus")]
        public ActionResult Update(TaskModul taskModul)
        {
            var response = taskModulRepository.UpdateStatus(taskModul);
            if (response > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Berhasil Update" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Update Gagal" });
            }
        }
    }
}
