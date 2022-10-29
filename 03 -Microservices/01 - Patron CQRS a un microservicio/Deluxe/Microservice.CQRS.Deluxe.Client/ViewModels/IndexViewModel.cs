using System.Collections.Generic;
using Microservice.CQRS.Deluxe.QueryStack.Model;

namespace Microservice.CQRS.Deluxe.Client.ViewModels
{
    public class IndexViewModel : ViewModelBase
    {
        public IList<CourtSchedule> CourtSchedules { get; set; }
    }
}