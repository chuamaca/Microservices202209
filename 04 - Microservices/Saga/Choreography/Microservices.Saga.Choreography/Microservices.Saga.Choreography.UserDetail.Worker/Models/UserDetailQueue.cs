using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Models
{
    using Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Data.Entities;
    public class UserDetailQueue : UserDetail
    {
        public List<TransactionHistory> TransactionList { get; set; }
    }
}
