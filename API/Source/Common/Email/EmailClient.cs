using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace API.Source.Common.Email;

public class EmailClient : IEmailClient
{
    private readonly IConfiguration _configuration;

    public EmailClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var mailMessage = new MimeMessage();

        var fromName = _configuration["Smtp:Host"] ?? "FROM_NAME";

        mailMessage.From.Add(new MailboxAddress(fromName, _configuration["Smtp:Username"]));
        mailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        mailMessage.Subject = subject;
        mailMessage.Body = new TextPart("plain") { Text = message };

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(
            _configuration["Smtp:Host"],
            int.Parse(_configuration["Smtp:Port"]),
            SecureSocketOptions.StartTlsWhenAvailable
        );
        await smtpClient.AuthenticateAsync(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
        await smtpClient.SendAsync(mailMessage);
        await smtpClient.DisconnectAsync(true);
    }
}