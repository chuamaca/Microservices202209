using Microservice.CQRS.Premium.Command.Model;
using Microservice.CQRS.Premium.Infrastructure.Entities;

namespace Microservice.CQRS.Premium.Command
{
    public class EventBuilder
    {
        public static MatchEvent New(string matchId, EventType eventOccurred, TeamId teamIndex, string team1, string team2, int? playerId)
        {
            var matchEvent = new MatchEvent
            {
                MatchId = matchId,
                Action = eventOccurred.ToString(),
                TeamId = (int) teamIndex,
                Team1 = team1,
                Team2 = team2,
                PlayerId = playerId
            };
            return matchEvent;
        }
    }
}