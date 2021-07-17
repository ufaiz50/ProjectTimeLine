using Client.BaseController;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using ProjectTimeLine.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class AccountRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JwT"));
        }

        public async Task<DataLoginVM> GetJWTNIK()
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

        public async Task<string> UpdateEmployee(Employee employee)
        {
            var res = "";

            StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(request, content);
            if (result.IsSuccessStatusCode)
            {
                var apiResponse = await result.Content.ReadAsStringAsync();
                res = apiResponse;
            }
            return res;
        }

    }
}
