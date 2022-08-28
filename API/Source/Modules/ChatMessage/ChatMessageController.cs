using API.Source.Extension;
using API.Source.Modules.Chat.Dto;
using API.Source.Modules.ChatMessage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Source.Modules.ChatMessage;

[ApiController]
[Route("[controller]")]
public class ChatMessageController : ControllerBase
{
    private readonly IChatMessageService _messageService;

    public ChatMessageController(IChatMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("send")]
    public async Task<dynamic> SendMessage([FromBody] SendChatMessageDto sendChatMessageDto)
    {
        var userId = User.GetUserId();
        
        // validate and save in database
        await _messageService.SaveMessage(userId, sendChatMessageDto);
        
        return Ok(new { message = "Message sent successfully" });
    }
}