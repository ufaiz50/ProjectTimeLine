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

        [HttpGet("ViewTaskId/{key}")]
        public ActionResult ViewTaskId(int key)
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
        
        [HttpGet("ViewModulTask/{key}")]
        public ActionResult ViewModulTask(int key)
        {
            try
            {
                var view = taskModulRepository.ViewModulTask(key);
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
        public ActionResult UpdateStatus(TaskModul taskModul)
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
        [HttpPut("UpdateDescription")]
        public ActionResult UpdateDescription(TaskModul taskModul)
        {
            var response = taskModulRepository.UpdateDescription(taskModul);
            if (response > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Berhasil Update" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Update Gagal" });
            }
        }

        [HttpPut("UpdateDate")]
        public ActionResult UpdateDate(TaskModul taskModul)
        {
            var response = taskModulRepository.UpdateDate(taskModul);
            if (response > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = response, message = "Berhasil Update" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = response, message = "Update Gagal" });
            }
        }

        [HttpGet("GetTaskAccount/{key}")]
        public ActionResult GetTaskAccount(int key)
        {
            try
            {
                var view = taskModulRepository.GetTaskAccount(key);
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
