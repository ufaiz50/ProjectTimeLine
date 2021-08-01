using Client.BaseController;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class AsignProjectRepository : GeneralRepository<Project, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AsignProjectRepository(Address address, string request = "Project/") : base(address, request)
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

        public async Task<List<Project>> GetProjectView()
        {
            List<Project> entities = new List<Project>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Project>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertProject(Project project)
        {
            var entities = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync(request, content);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = apiResponse;
            }
            return entities;
        }

        public async Task<List<TaskModulVM>> GetAllTask(int id)
        {
            List<TaskModulVM> entities = new List<TaskModulVM>();

            using (var response = await httpClient.GetAsync(request + "GetAllTask/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entities = JsonConvert.DeserializeObject<List<TaskModulVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertModul(Modul modul)
        {
            var entities = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(modul), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("Modul", content);

            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = apiResponse;
            }
            return entities;
        }

        public async Task<List<ModulVM>> GetModul(int id)
        {
            List<ModulVM> entities = new List<ModulVM>();

            using (var response = await httpClient.GetAsync("Modul/ViewProjectModul/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ModulVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertTask(TaskModul task)
        {
            var message = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("TaskModul", content);

            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }

        public async Task<string> UpdateDescription(TaskModul taskModul)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(taskModul), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync("TaskModul/UpdateDescription", content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<string> UpdateDate(TaskModul taskModul)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(taskModul), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync("TaskModul/UpdateDate", content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<List<UserDataVM>> GetAccounts()
        {
            List<UserDataVM> entities = new List<UserDataVM>();
            var result = await httpClient.GetAsync("Employees/userdata/");
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<UserDataVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<TaskAccountVM>> GetTaskAccount(int id)
        {
            List<TaskAccountVM> entities = new List<TaskAccountVM>();
            var result = await httpClient.GetAsync("TaskModul/GetTaskAccount/"+id);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TaskAccountVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertMember(AccountTask accountTask)
        {
            var message = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(accountTask), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("AccountTasks", content);

            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }

        public async Task<string> DeleteMember(string NIK, int TaskModulId)
        {
            var message = "";
            var result = await httpClient.DeleteAsync("AccountTasks/DeleteMember/"+NIK+"/"+TaskModulId);
            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }
    }
}

