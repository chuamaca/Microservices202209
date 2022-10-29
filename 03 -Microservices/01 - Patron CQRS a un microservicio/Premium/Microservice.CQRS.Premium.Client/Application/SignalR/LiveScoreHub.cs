using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Microservice.CQRS.Premium.Client.Application.SignalR
{
    public interface ITypedHubClient
    {
        Task RefreshPage();
    }

    public class LiveScoreHub : Hub<ITypedHubClient>
    {
        public async Task Refresh()
        {
            await Clients.All.RefreshPage();
        }
    }
}