using API.Source.Model.Enum;

namespace API.Source.Modules.User.Dto;

public class GetUserDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Username { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public bool IsOnline { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<GetUserChatMessageDto> SentChatMessages { get; set; }
    public ICollection<GetUserChatMessageDto> ReceivedChatMessages { get; set; }
}