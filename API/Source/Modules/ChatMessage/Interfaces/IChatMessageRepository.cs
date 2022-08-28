namespace API.Source.Modules.ChatMessage.Interfaces;

public interface IChatMessageRepository
{
    Task<Model.Entity.ChatMessage> CreateChatMessage(long userId, long receiverUserId, string message);
}