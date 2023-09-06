using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MyOwnBank.Features.Users.Options;
using MyOwnBank.Features.Users.Domain;

namespace MyOwnBank.Features.Users.Services;

public class EmailService
{
    private readonly MailOptions _mailOptions;
    private readonly PasswordHashingService _passwordHashingService;
    private readonly string _subject;

    public EmailService(IOptions<MailOptions> mailOptions, PasswordHashingService passwordHashingService)
    {
        _mailOptions = mailOptions.Value;
        _passwordHashingService = passwordHashingService;
        _subject = "Confirm your registration";
    }
    public Task SendEmailAsync(User user, string token) 
    {
        var mail = new MimeMessage();
        mail.From.Add(MailboxAddress.Parse(user.EmailAddress));
        mail.To.Add(MailboxAddress.Parse(user.EmailAddress));
        mail.Subject = _subject;
        string message = $"localhost/// {token}";
        mail.Body = new TextPart(TextFormat.Html)
        {
            //todo endpoint with api confirm
            Text = message
        };
        using var smtp = new SmtpClient();
        smtp.Connect(_mailOptions.EmailHost, _mailOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(user.EmailAddress, user.EmailPasswordHash);
        smtp.SendAsync(mail);
        smtp.DisconnectAsync(true);

        return Task.CompletedTask;
    }
}
