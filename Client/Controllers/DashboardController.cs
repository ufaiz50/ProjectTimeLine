using Client.BaseController;
using Client.Models;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class DashboardController : BaseController<Employee, UserdataRepository, string>
    {
        private readonly UserdataRepository repository;
        private readonly DashboardRepository dashboardRepository;

        public DashboardController(UserdataRepository repository, DashboardRepository dashboardRepository) : base(repository)
        {
            this.repository = repository;
            this.dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var Role = await repository.getJwt();
            ViewBag.Roles = Role.AllRole;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Userdata()
        {
            var Role = await repository.getJwt();
            ViewBag.Roles = Role.AllRole;
            return View();
        }
        
        public async Task<IActionResult> Assigntask()
        {
            var Role = await repository.getJwt();
            ViewBag.Roles = Role.AllRole;
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Manager()
        {
            return View();
        }

        public async Task<JsonResult> GetRegistrasiView()
        {
            var result = await repository.GetRegistrasiView();
            return Json(result);
        }

        public async Task<JsonResult> GetEmployeeView()
        {
            var result = await repository.GetEmployeeView();
            return Json(result);
        }

        public async Task<JsonResult> GetUserDataView()
        {

            var result = await repository.GetUserDataView("E0001");
            return Json(result);
        }

        public async Task<string> InsertEmployee(UserVM userVM)
        {
            var result = await repository.InsertEmployee(userVM);
            return result;
        }

        /*public async Task<string> UpdateEmployee(AccountRole userVM)
        {
            //var result = await repository.UpdateEmployee(userVM);
            //var result = await repository.UpdateUserData(userVM);
            return "asiap";
        }*/

        public async Task<string> DeleteEmployee(string NIK)
        {
            var result = await repository.DeleteEmployee(NIK);
            return result;
        }

        public async Task<JsonResult> Userdataview()
        {

            var result = await repository.UserData();
            var re = result.OrderBy(x => x.NIK).ToList();

            for (int i = 0; i < re.Count; i++)
            {
                for (int j = 0; j < re.Count; j++)
                {
                    if( re[i].NIK == re[j].NIK) 
                    {
                        re[i].AllRoleName.Add(re[j].RoleName);
                    }
                }
            }
            var final = re.GroupBy(p => p.NIK).Select(grp => grp.First()).ToArray();
            return Json(final);
        }

        public async Task<UserDataVM> Userdataviewnik(string nik)
        {
            var result = await repository.UserData(nik);
            var re = result.OrderBy(x => x.NIK).ToList();

            for (int i = 0; i < re.Count; i++)
            {
                for (int j = 0; j < re.Count; j++)
                {
                    if (re[i].NIK == re[j].NIK)
                    {
                        re[i].AllRoleName.Add(re[j].RoleName);
                    }
                }
            }
            var final = re.GroupBy(p => p.NIK).Select(grp => grp.First()).ToArray();
            return final[0];
        }

        public async Task<string> UpdateUserData(AccountRole userVM)
        {
            var result = await repository.UpdateUserData(userVM);
            return result;
        }
        
        public async Task<string> DeleteUserData(AccountRole userVM)
        {
            var result = await repository.deleteUserData(userVM);
            return result;
        }
        
        public async Task<JsonResult> GetJWT()
        {
            var result = await repository.getJwt();
            return Json(result);
        }

        public ActionResult LogOut()
        {
            _ = repository.LogOut();
            return RedirectToAction("Index", "Home");
        }


        /* DashBoard*/
        public async Task<List<GanttChartVM>> GanttChartView(int ProjectId)
        {
            var result = await dashboardRepository.GanttChartView(ProjectId);
            return result;
        }

    }
}
