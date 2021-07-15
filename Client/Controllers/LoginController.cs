using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jWToken = await loginRepository.Auth(login);
            if (jWToken == null)
            {
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.SetString("JWT", jWToken.Token);
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> EmployeesView(RegisterVM registerVm)
        {
            var message = await userdata.EmployeesView(registerVm);
            if (message == null)
            {
                return RedirectToAction("Register");
            }
            return RedirectToAction("Index");
        }
    }
}
