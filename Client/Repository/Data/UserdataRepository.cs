using Client.BaseController;
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
    public class UserdataRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public UserdataRepository(Address address, string request = "Employees/") : base(address, request)
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

        public async Task<List<RegisterVM>> GetRegistrasiView()
        {
            List<RegisterVM> entities = new List<RegisterVM>();

            using (var response = await httpClient.GetAsync(request + "ViewRegistrasi/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<Employee>> GetEmployeeView()
        {
            List<Employee> entities = new List<Employee>();

            using (var response = await httpClient.GetAsync(request + ""))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
            }
            return entities;
        }

        public async Task<String> EmployeesView(RegisterVM register)
        {
            string apiResponse = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "register", content);
            if (result.IsSuccessStatusCode)
            {
                apiResponse = await result.Content.ReadAsStringAsync();
            }
            return apiResponse;
        }
    }
}
