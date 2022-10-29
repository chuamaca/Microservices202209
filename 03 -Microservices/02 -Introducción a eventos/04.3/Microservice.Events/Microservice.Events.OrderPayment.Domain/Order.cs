using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Events.OrderPayment.Domain
{
    public class Order
    {
        public Order(int orderNo, string customerUserName)
        {
            OrderNo = orderNo;
            CustomerUserName = customerUserName;
        }

        public int OrderNo { get; set; }
        public string CustomerUserName { get; set; }
    }
}
