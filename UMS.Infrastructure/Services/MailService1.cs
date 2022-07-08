
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Infrastructure.Abstraction.Settings;

namespace UMS.Infrastructure.Services;

public class MailService1 : IMailService1
{
    private readonly MailSettings _mailSettings;
    public MailService1(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
       // email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
       email.Sender = MailboxAddress.Parse("donna.turcotte42@ethereal.email");
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("donna.turcotte42@ethereal.email", "7bwHEf7MEZrMD3TjTV");
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
    
}