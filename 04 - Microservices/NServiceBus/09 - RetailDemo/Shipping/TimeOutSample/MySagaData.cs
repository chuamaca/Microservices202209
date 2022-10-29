using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.TimeOutSample
{
    public class MySagaData: ContainSagaData
    {
        public string SomeId { get; set; }
        public bool Message2Arrived { get; set; }
    }
}
