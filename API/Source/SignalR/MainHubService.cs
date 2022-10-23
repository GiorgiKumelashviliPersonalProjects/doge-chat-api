using API.Source.Modules.ChatMessage.Dto;
using Microsoft.AspNetCore.SignalR;

namespace API.Source.SignalR;

public class MainHubService : IMainHubService
{
    private readonly IHubContext<MainHub> _hubContext;

    public MainHubService(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(string toUsername, ChatMessageDto chatMessageDto)
    {
        await _hubContext.Clients
            .Group(toUsername)
            .SendAsync(SignalREventsEnum.SendMessage.ToString(), chatMessageDto);
    }
    // public async Task SendMessage(string toUsername, string message, long userId)
    // {
    //     await _hubContext.Clients
    //         .Group(toUsername)
    //         .SendAsync(SignalREventsEnum.SendMessage.ToString(), new
    //         {
    //             message,
    //             receiverId = userId
    //         });
    // }
}