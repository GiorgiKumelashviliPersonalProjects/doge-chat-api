using Microsoft.AspNetCore.SignalR;

namespace API.Source.SignalR;

public interface IMainHubService
{
    public Task SendMessage(string toUsername, string message);
}