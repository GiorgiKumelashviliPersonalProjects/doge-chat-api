using API.Source.Modules.Chat.Dto;

namespace API.Source.Modules.ChatMessage.Interfaces;

public interface IChatMessageService
{
    Task SaveMessage(long userId, SendChatMessageDto sendChatMessageDto);
}