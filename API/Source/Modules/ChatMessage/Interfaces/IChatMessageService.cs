using API.Source.Modules.ChatMessage.Dto;

namespace API.Source.Modules.ChatMessage.Interfaces;

public interface IChatMessageService
{
    Task SaveMessage(long userId, SendChatMessageDto sendChatMessageDto);
}