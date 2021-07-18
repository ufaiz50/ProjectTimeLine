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
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private readonly AccountRoleRepository repository;
        public AccountRolesController(AccountRoleRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpDelete("userdata/{NIK}/{id}")]
        public ActionResult DeleteAdmin(string NIK, int id)
        {
            var respone = repository.Userdata(NIK, id);
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
