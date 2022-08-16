namespace API.Source.Model.Entity;

public class RefreshToken
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Value { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
}