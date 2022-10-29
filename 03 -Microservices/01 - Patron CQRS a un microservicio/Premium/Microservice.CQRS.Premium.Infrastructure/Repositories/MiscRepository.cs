using Microservice.CQRS.Premium.Infrastructure.Context;

namespace Microservice.CQRS.Premium.Infrastructure.Repositories
{
    public class MiscRepository
    {
        private readonly MicroserviceCQRSContext _microserviceCQRSContext;

        public MiscRepository(MicroserviceCQRSContext microserviceCQRSContext)
        {
            _microserviceCQRSContext = microserviceCQRSContext;
        }
        public void ResetDb()
        {
            // Empty both DBs
            foreach (var m in _microserviceCQRSContext.Matches)
            {
                _microserviceCQRSContext.Matches.Remove(m);
            }
            foreach (var mev in _microserviceCQRSContext.MatchEvents)
            {
                _microserviceCQRSContext.MatchEvents.Remove(mev);
            }
            _microserviceCQRSContext.SaveChanges();
        }
    }
}