namespace API.Source.Model.Entity;

public class ChatMessage
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Message { get; set; }

    public long SenderId { get; set; }

    public User Sender { get; set; }
    
    public long ReceiverId { get; set; }

    public User Receiver { get; set; }
}