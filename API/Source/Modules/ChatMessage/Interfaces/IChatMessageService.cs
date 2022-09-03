using API.Source.Modules.ChatMessage.Dto;

namespace API.Source.Modules.ChatMessage.Interfaces;

public interface IChatMessageService
{
    Task<ChatMessageService.SaveMessageResponse> SaveMessage(long userId, SendChatMessageDto sendChatMessageDto);
    Task<List<Model.Entity.ChatMessage>> GetMessages(long userId, long id);
}