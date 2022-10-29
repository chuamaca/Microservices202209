using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Saga.Choreography.Client.API.Application;
using Microservices.Saga.Choreography.Client.API.Infrastructure.Data.Entities;
using Microservices.Saga.Choreography.Client.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Saga.Choreography.Client.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserApplicationService _userApplicationService;

        public UsersController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        public List<User> Get()
        {
            return _userApplicationService.ListUsers();
        }

        [HttpPost]
        public string InsertUser([FromBody] UserShop userShop)
        {
            return _userApplicationService.InsertUser(userShop);
        }
    }
}
