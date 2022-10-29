namespace Microservices.Saga.Choreography.User.Worker.Models
{
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Data.Entities;
    using Microservices.Saga.Choreography.User.Worker.Infrastructure.Enums;

    public class TransactionHistory
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public TransactionState State { get; set; }
        public TransactionStep Step { get; set; }
        public TransactionType Type { get; set; }
    }
}