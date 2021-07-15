
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.BaseController;
using ProjectTimeLine.Model;
using ProjectTimeLine.Repositories.Data;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectTimeLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                var insert = employeeRepository.Register(registerVM);
                if (insert == 2)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
                }
                else if (insert == 3)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email sudah terdafftar" });
                }
                else if (insert == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "NIK sudah terdafftar" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Insert Gagal" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Insert gagal" });
            }
        }

        [HttpGet("ViewRegistrasi")]
        public ActionResult ViewRegistrasi()
        {
            try
            {
                var view = employeeRepository.ViewRegistrasi();
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

        [HttpGet("ViewRegistrasi/{key}")]
        public ActionResult ViewRegistrasiNIK(string key)
        {
            try
            {
                var view = employeeRepository.ViewRegistrasiNIK(key);
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

        [AllowAnonymous]
        [HttpPost("RegisterAdmin")]
        public ActionResult RegisterAdmin(UserVM userVM)
        {
            try
            {
                var insert = employeeRepository.InsertUser(userVM);
                if (insert == 3)
                {
                    return Ok("Berhasil Insert");
                }
                else if (insert == 1)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email sudah terdafftar" });
                }
                else if (insert == 0)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "NIK sudah terdafftar" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Insert Gagal" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Insert gagal" });
            }
        }

        [HttpDelete("DeleteAdmin/{NIK}")]
        public ActionResult DeleteAdmin(string NIK)
        {
            var respone = employeeRepository.DeleteUser(NIK);
            if (respone > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = respone, message = "Berhasil Delete" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = respone, message = "Delete gagal" });
            }
        }

        [HttpPut("UpdateAdmin")]
        public ActionResult UpdateAdmin(UserVM entity, string NIK)
        {
            var response = employeeRepository.UpdateUser(entity, NIK);
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
