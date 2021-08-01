using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository loginRepository;
        private readonly UserdataRepository userdata;

        public LoginController(LoginRepository loginRepository, UserdataRepository userdata)
        {
            this.loginRepository = loginRepository;
            this.userdata = userdata;
        }

        [HttpPost]
        public async Task<JWTokenVM> Auth(LoginVM login)
        {
            var jWToken = await loginRepository.Auth(login);
            HttpContext.Session.SetString("JWT", jWToken.Token);
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken result = tokenHandler.ReadJwtToken(jWToken.Token);
            return jWToken;

        }

        public async Task<JWTokenVM> EmployeesView(RegisterVM registerVm)
        {
            var message = await userdata.EmployeesView(registerVm);
            return message;
        }

        public async Task<JsonResult> GetEmployeeView()
        {
            var result = await userdata.GetEmployeeView();
            return Json(result);
        }
    }
}
