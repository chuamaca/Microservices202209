using Microservice.CQRS.Regular.ReadStack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.CQRS.Regular.Client.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Id = "";
            Team1 = "";
            Team2 = "";
            Matches = new List<Match>();
        }

        public IList<Match> Matches { get; set; }
        public String Team1 { get; set; }
        public String Team2 { get; set; }
        public String Id { get; set; }
    }
}
