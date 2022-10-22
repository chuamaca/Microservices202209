using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Entities;
using Microservice.Persistence.NoSQL.Cosmo.EFCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Persistence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersNoSqlController : ControllerBase
    {
        private IOrderNoSqlRepository _ordenRepository;
        public OrdersNoSqlController(IOrderNoSqlRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        [HttpGet]
        public IList<Order> List()
        {
            return _ordenRepository.List();
        }
    }
}
