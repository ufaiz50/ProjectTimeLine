using Client.BaseController;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class DashboardRepository : GeneralRepository<Project, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public DashboardRepository(Address address, string request = "Project/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JWT"));
        }

        public async Task<List<GanttChartVM>> GanttChartView(int ProjectId)
        {
            List<GanttChartVM> entities = new List<GanttChartVM>();

            using (var response = await httpClient.GetAsync(request + "GanttChart/"+ProjectId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GanttChartVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<LatesTaskVM>> GetTask(string NIK)
        {
            List<LatesTaskVM> entities = new List<LatesTaskVM>();

            using (var response = await httpClient.GetAsync("AccountTasks/Task/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entities = JsonConvert.DeserializeObject<List<LatesTaskVM>>(apiResponse);
            }
            return entities;

        }

        public async Task<List<LogStatusVM>> LastActivity(string NIK)
        {
            List<LogStatusVM> entities = new List<LogStatusVM>();

            using (var response = await httpClient.GetAsync("TaskHistory/LastActivity/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entities = JsonConvert.DeserializeObject<List<LogStatusVM>>(apiResponse);
            }
            return entities;

        }
    }
}
