using Microservice.CQRS.Premium.Client.Application.SignalR;
using Microservice.CQRS.Premium.Client.Common.Actions;
using Microservice.CQRS.Premium.Command.Model;
using Microservice.CQRS.Premium.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.CQRS.Premium.Client.Application
{
    public class AdminApplicationService
    {
        private MiscRepository _miscRepository;
        private MatchApplicationService _matchApplicationService;
        private IHubContext<LiveScoreHub, ITypedHubClient> _liveScoreHubContext;
        public AdminApplicationService(
            MiscRepository miscRepository,
            MatchApplicationService matchApplicationService,
            IHubContext<LiveScoreHub, ITypedHubClient> liveScoreHubContext)
        {
            _miscRepository = miscRepository;
            _matchApplicationService = matchApplicationService;
            _liveScoreHubContext = liveScoreHubContext;
        }
        public void ProcessAction(AdminAction action)
        {
            switch (action)
            {
                case AdminAction.ResetDb:
                    _miscRepository.ResetDb();
                    _matchApplicationService.ProcessAction("WP0001", EventType.Created, "Frogs", "Sharks");
                    _matchApplicationService.ProcessAction("WP0002", EventType.Created, "Sharks", "Eels");
                    break;
            }
            _liveScoreHubContext.Clients.All.RefreshPage().GetAwaiter();
        }
    }
}