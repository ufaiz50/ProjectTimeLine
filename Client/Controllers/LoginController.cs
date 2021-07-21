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
                return RedirectToAction("Index", "Login");
            }
            HttpContext.Session.SetString("JWT", jWToken.Token);
            HttpContext.Session.SetString("role", loginRepository.JwtRole(jWToken.Token));

            var role = HttpContext.Session.GetString("role");
            if (role == "Admin")
            {
                return RedirectToAction("Admin", "Dashboard");
            }
            else if (role == "Manager")
            {
                return RedirectToAction("Manager", "Dashboard");
            }
            else if (role == "SA")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (role == "BA")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (role == "Developer")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (role == "QA")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
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

        public async Task<JsonResult> GetEmployeeView()
        {
            var result = await userdata.GetEmployeeView();
            return Json(result);
        }
    }
}
