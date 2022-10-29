using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.Product.Worker.Infrastructure.Enums
{
    public enum TransactionState
    {
        Pending = 0,
        Completed = 1,
        WaitingForAction = 2,
        WaitingForRollback = 3,
        RollbackDone = 4,
        ActionDone = 5
    }
}
