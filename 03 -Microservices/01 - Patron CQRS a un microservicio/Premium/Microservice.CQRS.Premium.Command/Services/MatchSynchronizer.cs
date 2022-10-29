using Microservice.CQRS.Premium.Command.Model;
using Microservice.CQRS.Premium.Infrastructure.Repositories;
using Infrastructure=Microservice.CQRS.Premium.Infrastructure;

namespace Microservice.CQRS.Premium.Command.Services
{
    public class MatchSynchronizer
    {
        private MatchRepository _matchRepository;
        public MatchSynchronizer(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public void Save(Match match)
        {
            if (match == null)
                return;

            var persistedMatch = CopyFrom(match);
            _matchRepository.Save(persistedMatch);
        }

        public void Clear(string matchId)
        {
            if (matchId == null)
                return;

            // Remove record from snapshot table    
            _matchRepository.DeleteById(matchId);
        }


        private Infrastructure.Entities.Match CopyFrom(Match match)
        {
            var persistedMatch = new Infrastructure.Entities.Match()
            {
                Id = match.Id,
                Period = match.CurrentPeriod,
                Score1 = match.CurrentScore.TotalGoals1,
                Score2 = match.CurrentScore.TotalGoals2,
                State = (int) match.State,
                Team1 = match.Team1,
                Team2 = match.Team2,
                Timeouts1 = match.TimeoutSummary(TeamId.Home),
                Timeouts2 = match.TimeoutSummary(TeamId.Visitors)
            };
            return persistedMatch;
        }
    }
}