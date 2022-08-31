namespace API.Source.Model.Common;

public class AuthenticationTokenPayload
{
    public string Email { get; set; }
    public long UserId { get; set; }
    public string Username { get; set; }

    public AuthenticationTokenPayload(string email, long userId, string username)
    {
        Email = email;
        UserId = userId;
        Username = username;
    }
}