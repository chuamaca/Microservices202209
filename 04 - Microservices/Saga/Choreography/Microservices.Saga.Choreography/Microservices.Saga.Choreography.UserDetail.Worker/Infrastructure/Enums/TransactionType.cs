using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Saga.Choreography.UserDetail.Worker.Infrastructure.Enums
{
    public enum TransactionType
    {
        SqlDB = 1,
        OracleDB = 2,
        Akamai = 3,
        Redis = 4
    }
}
