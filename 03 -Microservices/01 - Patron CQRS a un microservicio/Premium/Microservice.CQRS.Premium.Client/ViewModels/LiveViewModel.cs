using Microservice.CQRS.Premium.Read.Dto;
using System.Collections.Generic;

namespace Microservice.CQRS.Premium.Client.ViewModels
{
    public class LiveViewModel : ViewModelBase
    {
        public LiveViewModel()
        {
            LiveMatches = new List<MatchInProgress>();
        }

        public IList<MatchInProgress> LiveMatches { get; set; }
    }
}
