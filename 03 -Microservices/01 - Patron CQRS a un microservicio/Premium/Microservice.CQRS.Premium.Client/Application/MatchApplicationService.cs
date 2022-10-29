using System;
using Microservice.CQRS.Premium.Client.Application.SignalR;
using Microservice.CQRS.Premium.Client.ViewModels;
using Microservice.CQRS.Premium.Command;
using Microservice.CQRS.Premium.Command.Model;
using Microservice.CQRS.Premium.Read.Facade;
using Microservice.CQRS.Premium.Shared;
using Microservice.CQRS.Premium.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace Microservice.CQRS.Premium.Client.Application
{
    public class MatchApplicationService
    {
        private MatchFacade _matchFacade;
        private IHubContext<LiveScoreHub, ITypedHubClient> _liveScoreHubContext;
        private EventSourceManager _eventSourceManager;
        // Call into the read stack to read state

        public MatchApplicationService(
            MatchFacade matchFacade,
            IHubContext<LiveScoreHub, ITypedHubClient> liveScoreHubContext,
            EventSourceManager eventSourceManager)
        {
            _matchFacade = matchFacade;
            _liveScoreHubContext = liveScoreHubContext;
            _eventSourceManager = eventSourceManager;
        }
        public MatchViewModel GetMatchDetails(string id)
        {
            var model = new MatchViewModel { Current = _matchFacade.FindById(id) };

            // Handle timeouts for #1
            var canCallTimeout1 = HandleTimeoutFor(model.Current.TimeoutSummary1);
            var canCallTimeout2 = HandleTimeoutFor(model.Current.TimeoutSummary2);

            switch (model.Current.State)
            {
                case MatchState.ToBePlayed:
                    model.Actions.CanStart = true;
                    break;
                case MatchState.Warmup:
                case MatchState.Interval:
                    model.Actions.CanEnd = true;
                    model.Actions.CanStartPeriod = true;
                    model.Actions.CanUndo = true;
                    break;
                case MatchState.PlayInProgress:
                    model.Actions.CanEnd = true;
                    model.Actions.CanEndPeriod = true;
                    model.Actions.CanScoreGoal = true;
                    model.Actions.CanCallTimeout1 = canCallTimeout1;
                    model.Actions.CanCallTimeout2 = canCallTimeout2;
                    model.Actions.CanUndo = true;
                    break;
                case MatchState.Timeout:
                    model.Actions.CanEnd = true;
                    model.Actions.CanResume = true;
                    model.Actions.CanUndo = true;
                    break;
            }
            return model;
        }

        // Log the event and sync up
        public void ProcessAction(string id, EventType whatHappened, string team1 = null, string team2 = null)
        {
            switch (whatHappened)
            {
                case EventType.Undo:
                    _eventSourceManager.RemoveLast(id);
                    break;
                case EventType.Created:
                    _eventSourceManager.Log(id, whatHappened, TeamId.Unknown, team1, team2);
                    break;
                case EventType.Start:
                    _eventSourceManager.Log(id, whatHappened);
                    break;
                case EventType.End:
                    _eventSourceManager.Log(id, whatHappened);
                    break;
                case EventType.Resume:
                    _eventSourceManager.Log(id, whatHappened);
                    break;
                case EventType.NewPeriod:
                    _eventSourceManager.Log(id, whatHappened);
                    break;
                case EventType.EndPeriod:
                    _eventSourceManager.Log(id, whatHappened);
                    break;
                case EventType.Goal1:
                    _eventSourceManager.Log(id, whatHappened, TeamId.Home, GetRandomPlayerId());
                    break;
                case EventType.Goal2:
                    _eventSourceManager.Log(id, whatHappened, TeamId.Visitors, GetRandomPlayerId());
                    break;
                case EventType.Timeout1:
                    _eventSourceManager.Log(id, whatHappened, TeamId.Home);
                    break;
                case EventType.Timeout2:
                    _eventSourceManager.Log(id, whatHappened, TeamId.Visitors);
                    break;
            }

            // Notify the live module of changes
            _liveScoreHubContext.Clients.All.RefreshPage().GetAwaiter();
        }

        private static int GetRandomPlayerId()
        {
            var rnd = new Random();
            return rnd.Next(2, 15);
        }

        private bool HandleTimeoutFor(string timeoutSummary)
        {
            var canCallTimeout = false;
            if (!String.IsNullOrWhiteSpace(timeoutSummary))
            {
                var tokens = timeoutSummary.Split('/');
                if (tokens.Length == 2)
                {
                    canCallTimeout = tokens[0].ToInt() < tokens[1].ToInt();
                }
            }
            return canCallTimeout;
        }
    }
}