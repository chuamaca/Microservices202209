using System;
using Microservices.Infrastructure.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Sales.ReadModels.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public ActionResult Get(Guid id)
        {
            var view = ServiceLocator.BrandView;
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
            var view = ServiceLocator.BrandView;
            var result = view.GetAll();
            return Ok(result);
        }
    }
}