using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.User.Commands.SendEmail;

public class SendEmailCommand : IRequest<string>
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
 
    
}