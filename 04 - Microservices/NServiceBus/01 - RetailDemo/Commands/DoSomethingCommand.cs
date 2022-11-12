using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DoSomethingCommand : ICommand
    {
        public string SomeProperty { get; set; }
    }
}
