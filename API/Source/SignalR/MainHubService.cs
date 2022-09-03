using Microsoft.AspNetCore.SignalR;

namespace API.Source.SignalR;

public class MainHubService: IMainHubService
{
    private readonly IHubContext<MainHub> _hubContext;

    public MainHubService(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(string toUsername, string message)
    {
        await _hubContext.Clients
            .Group(toUsername)
            .SendAsync(SignalREventsEnum.SendMessage.ToString(), message);
    }
}