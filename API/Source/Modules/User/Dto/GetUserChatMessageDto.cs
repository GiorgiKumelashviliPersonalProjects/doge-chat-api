namespace API.Source.Modules.User.Dto;

public class GetUserChatMessageDto
{
    public long Id { get; set; }
    public string Message { get; set; }
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public DateTime CreatedAt { get; set; }
}