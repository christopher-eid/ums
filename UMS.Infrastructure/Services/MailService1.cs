
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
    private readonly MailSettings1 _mailSettings;
    public MailService1(IOptions<MailSettings1> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
       email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
       //email.Sender = MailboxAddress.Parse("carlos54alberto@outlook.com");
        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        var builder = new BodyBuilder();
        
        builder.HtmlBody = mailRequest.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        //smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
        //smtp.Authenticate("carlos54alberto@outlook.com", "Carlos@54");
        smtp.Connect(_mailSettings.Host, _mailSettings.Port,SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
    
}