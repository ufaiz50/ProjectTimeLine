using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectTimeLine.BaseController;
using ProjectTimeLine.Context;
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
    /*[Authorize]*/
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;
        public IConfiguration _configuration;
        private readonly MyContext myContext;
        public AccountsController(AccountRepository repository, IConfiguration configuration, MyContext myContext) : base(repository)
        {
            this.repository = repository;
            _configuration = configuration;
            this.myContext = myContext;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM login)
        {
            /*try
            {*/
                var isCheck = repository.Login(login);
                switch (isCheck)
                {
                    case 1:
                    var getData = repository.GetDataLogin(login);
                    var jwt = new Util.JwToken(_configuration).GenerateJWT(getData);
                    return Ok(new { status = HttpStatusCode.OK, token = jwt, message = "Login Sukses" });
                    case 2:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, result = isCheck, message = "NIK/ Email tidak sesuai dengan data didatabase" });
                    case 3:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, result = isCheck, message = "Password tidak sesuai dengan data didatabase" });
                    default:
                        return BadRequest(new { status = HttpStatusCode.BadRequest, result = isCheck, message = "NIK dan Password tidak sesuai dengan data didatabase" });
                }
            /*}
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Login gagal Exception di terima" });
            }*/
        }

        /*[HttpPost("Added")]
        public ActionResult Added(Account entity)
        {
            try
            {
                var insert = repository.Added(entity);
                if (insert > 0)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = insert, message = "Berhasil Insert" });
                }
                else
                {
                    return BadRequest(new { status = HttpStatusCode.OK, result = insert, message = "Insert Gagal" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = HttpStatusCode.OK, result = 0, message = "Insert gagal" });
            }

        }*/
    }
}
