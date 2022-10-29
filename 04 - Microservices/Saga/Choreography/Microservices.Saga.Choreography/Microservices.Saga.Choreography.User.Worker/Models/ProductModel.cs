using System.Collections.Generic;

namespace Microservices.Saga.Choreography.User.Worker.Models
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    public class ProductModel : Product
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public List<TransactionHistory> TransactionList { get; set; }
    }
}