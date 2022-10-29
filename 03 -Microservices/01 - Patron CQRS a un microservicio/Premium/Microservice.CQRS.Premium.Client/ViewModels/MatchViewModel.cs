using Microservice.CQRS.Premium.Read.Dto;

namespace Microservice.CQRS.Premium.Client.ViewModels
{
    public class MatchViewModel : ViewModelBase
    {
        public MatchViewModel()
        {
            Current = new MatchInProgress();
            Actions = new MatchAllowedActions();
        }

        public MatchAllowedActions Actions { get; set; }
        public MatchInProgress Current { get; set; }
    }
}
