using MediatR;

namespace Application.Services.SendEmail;

public class SendEmailCommand : IRequest<string>
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
 
    
}