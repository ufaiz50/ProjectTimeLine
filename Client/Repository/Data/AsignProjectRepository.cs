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
    }
}

