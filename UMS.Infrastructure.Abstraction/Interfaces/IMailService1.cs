

using UMS.Infrastructure.Abstraction.Models;

namespace UMS.Infrastructure.Abstraction.Interfaces;

public interface IMailService1
{
    Task SendEmailAsync(MailRequest mailRequest);
    }

