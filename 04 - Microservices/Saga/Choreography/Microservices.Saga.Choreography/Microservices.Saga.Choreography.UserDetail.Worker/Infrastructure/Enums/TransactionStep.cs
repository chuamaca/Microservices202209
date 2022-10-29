using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Enums
{
    public enum TransactionStep
    {
        User = 0,
        Product = 1,
        UserDetail = 2
    }
}
