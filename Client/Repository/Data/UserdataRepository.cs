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

        public async Task<List<UserDataVM>> GetRegistrasiView()
        {
            List<UserDataVM> entities = new List<UserDataVM>();

            using (var response = await httpClient.GetAsync(request + "ViewRegistrasi/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<UserDataVM>>(apiResponse);
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
        
        public async Task<string> InsertEmployee(UserVM userDataVM)
        {
            var message = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(userDataVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "RegisterAdmin", content);

            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
            }
            return message;

        }

        public async Task<List<UserDataVM>> GetUserDataView(string NIK)
        {
           List<UserDataVM> entities = new List<UserDataVM>();

            using (var response = await httpClient.GetAsync(request+ "ViewRegistrasi/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
               
                entities = JsonConvert.DeserializeObject<List<UserDataVM>>(apiResponse);
            }
            return entities;

        }
        public async Task<string> UpdateEmployee(UserDataVM userDataVM)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(userDataVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(request + "UpdateAdmin/", content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<string> DeleteEmployee(string NIK)
        {
            var res = "";
            var result = await httpClient.DeleteAsync(request + "DeleteAdmin/" + NIK);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<List<UserDataVM>> UserData()
        {
            List<UserDataVM> entities = new List<UserDataVM>();
            var result = await httpClient.GetAsync(request + "userdata/");
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<UserDataVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<List<UserDataVM>> UserData(string nik)
        {
            List<UserDataVM> entities = new List<UserDataVM>();
            var result = await httpClient.GetAsync(request + "userdata/" + nik);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<UserDataVM>>(apiResponse);
            }
            return entities;
        }


        public async Task<string> UpdateUserData(AccountRole userDataVM)
        {
            var res = "";
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(userDataVM), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("AccountRoles", content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<string> deleteUserData(AccountRole ar)
        {
            var res = "";

            var result = await httpClient.DeleteAsync("AccountRoles/userdata/" + ar.NIK + "/" + ar.RoleID);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

    }
}

