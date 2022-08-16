namespace API.Source.Common.Email;

public interface IEmailClient
{
    public Task SendEmailAsync(string toEmail, string subject, string message);
}