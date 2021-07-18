using Client.BaseController;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class AccountTaskRepository : GeneralRepository<AccountTask, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountTaskRepository(Address address, string request = "AccountTasks/") : base(address, request)
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

        public async Task<List<AccountTask>> GetTaskView()
        {
            List<AccountTask> entities = new List<AccountTask>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<AccountTask>>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertAccountTask(AccountTask task)
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

        public async Task<string> UpdateAccountTask(AccountTask task)
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

        public async Task<string> DeleteAccountTask(string id)
        {
            var res = "";
            var result = await httpClient.DeleteAsync(request + id);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }
    }
}
