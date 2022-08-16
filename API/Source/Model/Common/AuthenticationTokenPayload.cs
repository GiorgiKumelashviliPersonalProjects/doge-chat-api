namespace API.Source.Model.Common;

public class AuthenticationTokenPayload
{
    public string Email { get; set; }

    public long UserId { get; set; }

    public AuthenticationTokenPayload(string email, long userId)
    {
        Email = email;
        UserId = userId;
    }
}