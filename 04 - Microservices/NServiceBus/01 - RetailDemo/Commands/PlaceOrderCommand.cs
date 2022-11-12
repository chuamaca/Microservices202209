using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class PlaceOrderCommand: ICommand
    {
        public string OrderId { get; set; }
    }
}
