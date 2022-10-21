namespace API.Source.Modules.ChatMessage.Dto;

public class ChatMessageDto
{
    public long Id { get; set; }
    public string Message { get; set; }
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public DateTime CreatedAt { get; set; }
}