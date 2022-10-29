using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public class DoSomethingComplexCommand:ICommand
    {
        public int SomeId { get; set; }
        public ChildClass ChildStuff { get; set; }
        public List<ChildClass> ListOfStuff { get; set; } = new List<ChildClass>();
    }
}
