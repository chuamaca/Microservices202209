using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.CQRS.Regular.Client.Models
{
    public class ViewModelBase
    {
        public ViewModelBase()
        {
            Title = "";
        }

        public string Title { get; set; }
    }
}
