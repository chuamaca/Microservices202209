using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.TimeOutSample
{
    public class Message1: ICommand
    {
        public string SomeId { get; set; }
    }
}
