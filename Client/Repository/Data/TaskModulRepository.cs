using Client.BaseController;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class TaskModulRepository : GeneralRepository<TaskModul, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public TaskModulRepository(Address address, string request = "TaskModul/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JwToken"));
        }

        public async Task<List<TaskModul>> GetTaskModelView()
        {
            List<TaskModul> entities = new List<TaskModul>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TaskModul>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertTaskModul(TaskModul task)
        {
            var message = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request , content);   

            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }

        public async Task<string> UpdateTaskModul(TaskModul task)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(request , content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<string> DeleteTaskModul(int id)
        {
            var res = "";
            var result = await httpClient.DeleteAsync(request +"?key="+ id);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<List<TaskMemberVM>> GetTaskAccView()
        {
            List<TaskMemberVM> entities = new List<TaskMemberVM>();

            using (var response = await httpClient.GetAsync(request + "viewtask"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TaskMemberVM>>(apiResponse);
            }
            return entities;
        }
    }
}
