using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Sales.API.DataTransferObjects.Commands.Orders;
using Microservices.Sales.API.MicroServices.Orders.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post(PlaceOrderCommand cmd)
        {
            if (Guid.Empty.Equals(cmd.Id))
            {
                return Problem(
                   title: "Missing Order Id",
                   detail: "order information must be supplied in the POST body",
                   statusCode: StatusCodes.Status400BadRequest
               );
            }

            var command = new StartNewOrder(cmd.Id, cmd.ProductId, cmd.Quantity);

            try
            {
                ServiceLocator.OrderCommands.Handle(command);

                var link = new Uri(string.Format("https://localhost:44359/api/orders/{0}", command.Id));
                return Created(link, command);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
        }
    }
}