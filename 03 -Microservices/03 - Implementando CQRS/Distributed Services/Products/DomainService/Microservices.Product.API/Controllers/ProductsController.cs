using System;
using Microservices.Infrastructure.Crosscutting.Exceptions;
using Microservices.Products.API.DataTransferObjects.Commands.Products;
using Microservices.Products.API.MicroServices.Products.Commands;
using Microservices.Products.API.MicroServices.Products.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCommandHandlers handler;

        //public ProductsController()
        //    : this(ServiceLocator.ProductCommands)
        //{ }

        public ProductsController(ProductCommandHandlers handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public ActionResult Post(CreateProductCommand cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd.Name))
            {
                return Problem(
                    title : "Missing product code",
                    detail : "code must be supplied in the body",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            try
            {
                var command = new CreateProduct(Guid.NewGuid(), cmd.Name, cmd.Description, cmd.Price);
                handler.Handle(command);

                var link = new Uri(string.Format("http://localhost:44339/api/products/{0}", command.Id));
                return Created(link, command);
            }
            catch (AggregateNotFoundException)
            {
                return NotFound();
            }
            catch (AggregateDeletedException)
            {
                return Conflict();
            }
        }

        [HttpPut("{id:guid}")]        
        public ActionResult Put(Guid id, AlterProductCommand cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd.Name))
            {
                return Problem(
                    title: "Missing product code",
                    detail: "code must be supplied in the body",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            try
            {
                var command = new AlterProduct(id, cmd.Version, cmd.Name, cmd.Description, cmd.Price);
                handler.Handle(command);

                return Ok(command);
            }
            catch (AggregateNotFoundException)
            {
                return NotFound();
            }
            catch (AggregateDeletedException)
            {
                return Conflict();
            }
            catch(Exception ex)
            {
                return Problem(
                    title: "Error product",
                    detail: ex.Message,
                    statusCode:StatusCodes.Status400BadRequest
                );
            }
        }
    }
}