using Microservice.CQRS.Premium.Client.ViewModels;
using Microservice.CQRS.Premium.Read.Facade;

namespace Microservice.CQRS.Premium.Client.Application
{
    public class LiveScoreApplicationService
    {
        private MatchFacade _matchFacade;
        public LiveScoreApplicationService(MatchFacade matchFacade)
        {
            _matchFacade = matchFacade;
        }
        public LiveViewModel GetLiveViewModel()
        {
            var model = new LiveViewModel { LiveMatches = _matchFacade.FindInProgress() };
            return model;
        }
    }
}