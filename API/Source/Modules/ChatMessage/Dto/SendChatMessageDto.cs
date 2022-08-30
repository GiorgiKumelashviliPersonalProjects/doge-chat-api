using System.ComponentModel.DataAnnotations;

namespace API.Source.Modules.ChatMessage.Dto;

public class SendChatMessageDto
{
    [Required, MinLength(1)] public string Message { get; set; }

    [Required, Range(1, long.MaxValue)] public long UserId { get; set; }
}