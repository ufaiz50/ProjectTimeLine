using Client.BaseController;
using Client.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

namespace Client.Repository
{
    public class GeneralRepository<TEntity, TId> : IRepository<TEntity, TId>
         where TEntity : class
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public GeneralRepository(Address address, string request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //JWT
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWT"));
        }

        public HttpStatusCode Delete(TId id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }

        public async Task<List<TEntity>> Get()
        {
            List<TEntity> entities = new List<TEntity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            }
            return entities;
        }

        public async Task<TEntity> Get(TId id)
        {
            TEntity entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode Put(TId id, TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Post(TEntity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request, content).Result;
            return result.StatusCode;
        }

        // GetJWT
        public async Task<DataLoginVM> GetJwt()
        {
            var content = new DataLoginVM();
            var token = _contextAccessor.HttpContext.Session.GetString("JWT");
            var result = new JwtSecurityTokenHandler().ReadJwtToken(token);

            content.NIK = result.Claims.First(claim => claim.Type == "NIK").Value;
            content.Name = result.Claims.First(claim => claim.Type == "Name").Value;
            content.Email = result.Claims.First(claim => claim.Type == "Email").Value;
            var getAllRole = result.Claims.Where(x => x.Type == "role").Select(data => data.Value);
            foreach (var item in getAllRole)
            {
                content.AllRole.Add(item);
            }

            return content;
        }
    }
}
