using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.TimeOutSample
{
    public class MyCustomTimeout:ICommand
    {
        public string SomeId { get; set; }
    }
}
