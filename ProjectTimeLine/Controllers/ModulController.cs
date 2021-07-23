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
    public class ModulController : BaseController<Modul, ModulRepository, int>
    {
        private readonly ModulRepository modulRepository;
        public ModulController(ModulRepository modulRepository) : base(modulRepository)
        {
            this.modulRepository = modulRepository;
        }
        [HttpGet("ViewModul")]
        public ActionResult ViewTask()
        {
            try
            {
                var view = modulRepository.ViewModul();
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

        [HttpGet("ViewModul/{key}")]
        public ActionResult ViewTaskNIK(int key)
        {
            try
            {
                var view = modulRepository.ViewModul(key);
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
        
        [HttpGet("ViewProjectModul/{key}")]
        public ActionResult ViewProjectModul(int key)
        {
            try
            {
                var view = modulRepository.ViewProjectModul(key);
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
