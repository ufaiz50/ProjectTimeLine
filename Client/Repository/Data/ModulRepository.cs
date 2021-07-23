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
    public class ModulRepository : GeneralRepository<Modul, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public ModulRepository(Address address, string request = "Modul/") : base(address, request)
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

        public async Task<List<Modul>> GetModulView()
        {
            List<Modul> entities = new List<Modul>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Modul>>(apiResponse);
            }
            return entities;
        }
        
        public async Task<Modul> GetModulIdView(int id)
        {
            Modul entities = new Modul();

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<Modul>(apiResponse);
            }
            return entities;
        }

        public async Task<string> InsertModul(Modul modul)
        {
            var message = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(modul), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request , content);

            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }

        public async Task<string> UpdateModul(Modul modul)
        {
            var res = "";
            StringContent content = new StringContent(JsonConvert.SerializeObject(modul), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(request , content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<string> DeleteModul(int id)
        {
            var res = "";
            var result = await httpClient.DeleteAsync(request+"?key=" + id);
            if (result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

        public async Task<List<ModulVM>> GetModulTable()
        {
            List<ModulVM> entities = new List<ModulVM>();

            using (var response = await httpClient.GetAsync(request + "viewmodul"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ModulVM>>(apiResponse);
            }
            return entities;
        }
        
        public async Task<List<ModulVM>> ViewProjectModul(int id)
        {
            List<ModulVM> entities = new List<ModulVM>();

            using (var response = await httpClient.GetAsync(request + "ViewProjectModul/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ModulVM>>(apiResponse);
            }
            return entities;
        }
    }
}
