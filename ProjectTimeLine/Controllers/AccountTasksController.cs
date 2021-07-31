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
    public class AccountTasksController : BaseController<AccountTask, AccountTaskRepository, string>
    {
        private readonly AccountTaskRepository repository;
        public AccountTasksController(AccountTaskRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpDelete("DeleteTaskMember/{id}")]
        public ActionResult DeleteTaskMember(int id)
        {
            var respone = repository.DeleteTaskMember(id);
            if (respone > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = respone, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = respone, message = "Delete gagal" });
            }
        }

        [HttpGet("ProjectTask/{NIK}")]
        public ActionResult GetProjectTask(string NIK)
        {
            try
            {
                var view = repository.GetProjectTask(NIK);
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

        [HttpGet("ModulTask/{NIK}/{ProjectId}")]
        public ActionResult GetModulTask(string NIK, int ProjectId)
        {
            try
            {
                var view = repository.GetModulTask(NIK, ProjectId);
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

        [HttpGet("Task/{NIK}")]
        public ActionResult GetTask(string NIK)
        {
            try
            {
                var view = repository.GetTask(NIK);
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

        [HttpDelete("DeleteMember/{NIK}/{TaskModulId}")]
        public ActionResult DeleteMember(string NIK, int TaskModulId)
        {
            var respone = repository.DeleteMember(NIK, TaskModulId);
            if (respone > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = respone, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = respone, message = "Delete gagal" });
            }
        }

    }
}
