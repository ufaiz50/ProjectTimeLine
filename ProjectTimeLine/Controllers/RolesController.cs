using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimeLine.BaseController;
using ProjectTimeLine.Model;
using ProjectTimeLine.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        public RolesController(RoleRepository repository) : base(repository)
        {
        }
    }
}
