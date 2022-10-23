using API.Source.Modules.ChatMessage.Dto;

namespace API.Source.SignalR;

public interface IMainHubService
{
    // public Task SendMessage(string toUsername, string message, long userId);
    public Task SendMessage(string toUsername, ChatMessageDto chatMessageDto);
}