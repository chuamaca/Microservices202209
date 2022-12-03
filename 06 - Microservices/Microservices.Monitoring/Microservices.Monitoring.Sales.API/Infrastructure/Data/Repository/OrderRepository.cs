using Microservices.Monitoring.Sales.API.Infrastructure.Data.Contexts;
using Microservices.Sales.API.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Monitoring.Sales.API.Infrastructure.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MicroservicesMonitoringContext _microservicesMonitoringContext;
        public OrderRepository(MicroservicesMonitoringContext microservicesMonitoringContext)
        {
            _microservicesMonitoringContext = microservicesMonitoringContext;
        }
        public List<OrderDto> List()
        {
            var query = from o in _microservicesMonitoringContext.Orders
                        join c in _microservicesMonitoringContext.Customers on o.CustomerId equals c.CustomerId
                        join s in _microservicesMonitoringContext.staff on o.StaffId equals s.StaffId
                        join st in _microservicesMonitoringContext.Stores on o.StoreId equals st.StoreId
                        select new OrderDto
                        {
                            OrderId = o.OrderId,
                            OrderStatus = o.OrderStatus,
                            OrderDate = o.OrderDate,
                            CustomerId = o.CustomerId,
                            Customer = new CustomerDto
                            {
                                CustomerId = c.CustomerId,
                                FirstName = c.FirstName,
                                LastName = c.LastName
                            },
                            StaffId = o.StaffId,
                            Staff = new StaffDto
                            {
                                StaffId = s.StaffId,
                                FirstName = s.FirstName,
                                LastName = s.LastName
                            },
                            StoreId = o.StoreId,
                            Store = new StoreDto
                            {
                                StoreId = st.StoreId,
                                StoreName = st.StoreName
                            }
                        };

            List<OrderDto> orders = query.ToList();

            return orders;
        }
    }
}
