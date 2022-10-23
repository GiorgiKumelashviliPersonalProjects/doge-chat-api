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
        var username = Context.User.GetUsername();
        var userId = Context.User.GetUserId();

        // track connected username by connection id
        await _tracker.UserConnected(username, Context.ConnectionId);

        //todo track grouping
        await Groups.AddToGroupAsync(Context.ConnectionId, username);

        // send to all other socket that user has connected
        await Clients.Others.SendAsync(SignalREventsEnum.UserIsOnline.ToString(), new
        {
            receiverId = userId
        });
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(System.Exception? exception)
    {
        var username = Context.User.GetUsername();
        var userId = Context.User.GetUserId();

        // track connected username by connection id
        await _tracker.UserDisConnected(username, Context.ConnectionId);

        //todo track grouping
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);

        // send to all other socket that user has disconnected
        await Clients.Others.SendAsync(SignalREventsEnum.UserIsOffline.ToString(), new
        {
            receiverId = userId
        });
        await base.OnDisconnectedAsync(exception);
    }
}