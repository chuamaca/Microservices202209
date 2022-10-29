using Microservice.CQRS.Premium.Client.ViewModels;
using Microservice.CQRS.Premium.Read.Facade;

namespace Microservice.CQRS.Premium.Client.Application
{
    public class HomeApplicationService
    {
        private MatchFacade _matchFacade;
        public HomeApplicationService(MatchFacade matchFacade)
        {
            _matchFacade = matchFacade;
        }
        public IndexViewModel GetIndexViewModel()
        {
            var model = new IndexViewModel { ScheduledMatches = _matchFacade.FindScheduled() };
            return model;
        }
    }
}