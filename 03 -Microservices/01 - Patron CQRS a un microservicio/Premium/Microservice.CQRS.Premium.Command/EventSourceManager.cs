using System;
using Microservice.CQRS.Premium.Command.Model;
using Microservice.CQRS.Premium.Command.Services;
using Microservice.CQRS.Premium.Infrastructure.Repositories;

namespace Microservice.CQRS.Premium.Command
{
    public class EventSourceManager
    {
        private EventRepository _eventRepository;
        private MatchSynchronizer _matchSynchronizer;
        public EventSourceManager(EventRepository eventRepository,MatchSynchronizer matchSynchronizer)
        {
            _eventRepository = eventRepository;
            _matchSynchronizer = matchSynchronizer;
        }

        public void Log(string matchId, EventType eventOccurred)
        {
            Log(matchId, eventOccurred, TeamId.Unknown, null, null);
        }

        public void Log(string matchId, EventType eventOccurred, TeamId teamIndex, int playerId)
        {
            Log(matchId, eventOccurred, teamIndex, null, null, playerId);
        }

        public void Log(string matchId, EventType eventOccurred, TeamId teamIndex, String team1 = null, String team2 = null, int? playerId = null)
        {
            var matchEvent = EventBuilder.New(matchId, eventOccurred, teamIndex, team1, team2, playerId);
            _eventRepository.Store(matchEvent);

            // Sync up with read model
            // 1: read all events
            // 2: play all events using Match class
            // 3: save state to relational DB for reads/live
            var match = Replay(matchId);
            _matchSynchronizer.Save(match);

            // or 

            // 1: do nothing  here
            // 2: expect a scheduled job do previous sync steps 

            // or 

            // 1: do nothing here
            // 2: expect read stack replays events via Match class when requested
        }

        public Match Replay(string matchId)
        {
            var allEvents = _eventRepository.All(matchId);
            if (allEvents.Count == 0)
                return null;

            var match = new Match();
            foreach (var e in allEvents)
            {
                var whatHappened = (EventType) Enum.Parse(typeof (EventType), e.Action);
                switch (whatHappened)
                {
                    case EventType.Created:
                        match = new Match(e.MatchId, e.Team1, e.Team2);
                        break;
                    case EventType.Start:
                        match.Start();
                        break;
                    case EventType.End:
                        match.Finish();
                        break;
                    case EventType.NewPeriod:
                        match.StartPeriod();
                        break;
                    case EventType.EndPeriod:
                        match.EndPeriod();
                        break;
                    case EventType.Goal1:
                        match.Goal(TeamId.Home);        // Should add player reference too
                        break;
                    case EventType.Goal2:
                        match.Goal(TeamId.Visitors);    // Should add player reference too
                        break;
                    case EventType.Timeout1:
                        match.Timeout(TeamId.Home);
                        break;
                    case EventType.Timeout2:
                        match.Timeout(TeamId.Visitors);
                        break;
                    case EventType.Resume:
                        match.Resume();
                        break;
                }
            }
            return match;
        }

        public void RemoveLast(string matchId)
        {
            // Remove
            _eventRepository.RemoveMostRecent(matchId);

            // Sync up with read model
            var match = Replay(matchId);
            if (match == null)
                _matchSynchronizer.Clear(matchId);
            else
                _matchSynchronizer.Save(match);

            // Further compensation logic here ...
            // ...
        }
    }
}