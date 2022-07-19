using AutoMapper;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.Services.SendEmail;

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand,string >
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    private readonly IMailService1 _mailService;
    public SendEmailCommandHandler(UmsContext db, IMapper mapper, IMailService1 mailService)
    {
        _umsContext = db;
        _mapper = mapper;
        _mailService = mailService;
    }
   

    public async Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        MailRequest c = new MailRequest()
        {
            ToEmail = request.ToEmail,
            Subject = request.Subject,
            Body = request.Body,
        };
    await _mailService.SendEmailAsync(c);
        return "done";
    }
}