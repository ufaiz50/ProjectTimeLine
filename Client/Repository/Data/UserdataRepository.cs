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

        public async Task<UserDataVM> GetUserDataView(string NIK)
        {
            UserDataVM entities = new UserDataVM();

            using (var response = await httpClient.GetAsync(request + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<UserDataVM>(apiResponse);
            }
            return entities;

        }
        public async Task<string> UpdateEmployee(UserVM userDataVM)
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
    }
}
