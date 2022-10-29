using System.Linq;
using Microservice.CQRS.Premium.Infrastructure.Common;
using Microservice.CQRS.Premium.Infrastructure.Context;
using Microservice.CQRS.Premium.Infrastructure.Entities;

namespace Microservice.CQRS.Premium.Infrastructure.Repositories
{
    public class MatchRepository
    {
        private readonly MicroserviceCQRSContext _microserviceCQRSContext;

        public MatchRepository(MicroserviceCQRSContext microserviceCQRSContext)
        {
            _microserviceCQRSContext = microserviceCQRSContext;
        }
        public Match FindById(string id)
        {
            var match = (from m in _microserviceCQRSContext.Matches where m.Id == id select m).FirstOrDefault();
            return match;
        }

        public void DeleteById(string id)
        {
            var match = FindById(id);
            if (match == null)
                return;

            _microserviceCQRSContext.Matches.Remove(match);
            _microserviceCQRSContext.SaveChanges();
        }

        public void Save(Match match)
        {
            var existing = FindById(match.Id);
            if (existing == null)
            {
                _microserviceCQRSContext.Matches.Add(match);
            }
            else
            {
                match.CopyPropertiesTo(existing);
            }
            _microserviceCQRSContext.SaveChanges();
        }

        public IQueryable<Match> Find()
        {
            var list = (from m in _microserviceCQRSContext.Matches select m);
            return list;
        }
    }
}