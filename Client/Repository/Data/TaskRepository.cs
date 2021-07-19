using Client.BaseController;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class TaskRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public TaskRepository(Address address, string request = "AccountTasks/") : base(address, request)
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

        public async Task<List<TaskProjectVM>> GetProjectTask(string NIK)
        {
            List<TaskProjectVM> entities = new List<TaskProjectVM>();

            using (var response = await httpClient.GetAsync(request + "ProjectTask/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entities = JsonConvert.DeserializeObject<List<TaskProjectVM>>(apiResponse);
            }
            return entities;

        }

        public async Task<List<TaskModulVM>> GetModulTask(string NIK, int ModulId)
        {
            List<TaskModulVM> entities = new List<TaskModulVM>();

            using (var response = await httpClient.GetAsync(request + "ModulTask/" + NIK + "/" + ModulId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entities = JsonConvert.DeserializeObject<List<TaskModulVM>>(apiResponse);
            }
            return entities;

        }

        public async Task<DataLoginVM> GetJwt()
        {
            var content = new DataLoginVM();
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var result = new JwtSecurityTokenHandler().ReadJwtToken(token);

            content.NIK = result.Claims.First(claim => claim.Type == "NIK").Value;
            content.Name = result.Claims.First(claim => claim.Type == "Name").Value;
            content.Email = result.Claims.First(claim => claim.Type == "Email").Value;
            var getAllRole = result.Claims.Where(x => x.Type == "Roles").Select(data => data.Value);
            foreach (var item in getAllRole)
            {
                content.AllRole.Add(item);
            }

            return content;
        }

        public async Task<string> DeleteTaskMember(int id)
        {
            var res = "";
            var result = await httpClient.DeleteAsync("AccountTasks/DeleteTaskMember/" + id);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }
        
        // Update Status
        public async Task<string> UpdateStatus(TaskModul taskModul)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(taskModul), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync("TaskModul/UpdateStatus", content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

            
    }
}
