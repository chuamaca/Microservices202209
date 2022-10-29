using Microservices.Saga.Choreography.Product.Worker.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.Product.Worker.Models
{
    public class UserDetailQueue : UserDetail
    {
        public List<TransactionHistory> TransactionList { get; set; }
    }
}
