﻿using Client.Repository.Data;
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

        public LoginController(LoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
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
            return RedirectToAction("Privacy", "Home");
        }
    }
}
