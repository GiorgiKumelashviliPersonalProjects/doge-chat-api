using API.Source.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Source.SignalR;

[Authorize]
public class MainHub : Hub
{
    private readonly UserConnectionTracker _tracker;

    public MainHub(UserConnectionTracker tracker)
    {
        _tracker = tracker;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("================================================================");
        Console.WriteLine("User connected");
        
        var username = Context.User.GetUsername();

        // track connected username by connection id
        await _tracker.UserConnected(username, Context.ConnectionId);

        //todo track grouping
        await AddToGroup(username);

        // send to all other socket that user has connected
        await Clients.Others.SendAsync(SignalREventsEnum.UserIsOnline.ToString(), username);
        
        // await Clients
        //     .Group(username)
        //     .SendAsync(SignalREventsEnum.SendMessage.ToString(), "testing");
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(System.Exception? exception)
    {
        var username = Context.User.GetUsername();

        // track connected username by connection id
        await _tracker.UserDisConnected(username, Context.ConnectionId);

        //todo track grouping
        await RemoveFromGroup(username);

        // send to all other socket that user has disconnected
        await Clients.Others.SendAsync(SignalREventsEnum.UserIsOffline.ToString(), username);
        await base.OnDisconnectedAsync(exception);
    }

    private async Task AddToGroup(string username)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, username);
    }

    private async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}