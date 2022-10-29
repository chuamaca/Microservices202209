using Microservice.CQRS.Premium.Read.Dto;
using System.Collections.Generic;
namespace Microservice.CQRS.Premium.Client.ViewModels
{
    public class IndexViewModel : ViewModelBase
    {
        public IList<MatchListItem> ScheduledMatches { get; set; }
    }
}
