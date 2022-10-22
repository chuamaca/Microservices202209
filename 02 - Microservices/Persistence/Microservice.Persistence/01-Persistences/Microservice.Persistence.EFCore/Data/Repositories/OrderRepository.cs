using Microservice.Persistence.EFCore.Data.Contexts;
using Microservice.Persistence.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microservice.Persistence.EFCore.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private MicroservicePersistenceEFcoreContext _context;
        public OrderRepository(MicroservicePersistenceEFcoreContext context)
        {
            _context = context;
        }

        public IList<OrderDto> List()
        {
            var queryOrden = from o in _context.Orders
                             select new OrderDto
                             {
                                 OrderId = o.OrderId,
                                 Description = o.Description
                             };

            var queryItems = from oi in _context.OrderItems
                             join p in _context.Products
                                on oi.ProductId equals p.ProductId
                             select new OrderItemDto
                             {
                                 OrderItemId = oi.OrderItemId,
                                 OrderId = oi.OrderId,
                                 Description = p.Description,
                                 ProductId = p.ProductId,
                                 Price = p.Price
                             };

            List<OrderDto> orders = queryOrden.ToList();
            List<OrderItemDto> items = queryItems.ToList();

            orders.ForEach(orden =>
            {
                orden.Items = items.Where(w => w.OrderId == orden.OrderId).ToList();
            });

            return orders;
        }
    }
}
