﻿using Client.BaseController;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class TaskHistoryRepository : GeneralRepository<TaskHistory, int>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public TaskHistoryRepository(Address address, string request = "TaskHistory/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JWT"));
        }

    }
}
