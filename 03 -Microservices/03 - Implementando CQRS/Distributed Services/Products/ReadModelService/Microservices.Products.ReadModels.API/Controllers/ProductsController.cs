using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Infrastructure.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Products.ReadModels.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public ActionResult Get(Guid id)
        {
            var view = ServiceLocator.ProductView;
            try
            {
                var dto = view.GetById(id);
                return Ok(dto);
            }
            catch (ReadModelNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            var view = ServiceLocator.ProductView;
            var result = view.GetAll();
            return Ok(result);
        }
    }
}