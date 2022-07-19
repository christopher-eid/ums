using AutoMapper;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.Services.PushNotification;

public class PushNotificationCommandHandler : IRequestHandler<PushNotificationCommand,string >
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    private readonly IChatHub _chatHub;
    public PushNotificationCommandHandler(UmsContext db, IMapper mapper, IChatHub chatHub)
    {
        _umsContext = db;
        _mapper = mapper;
        _chatHub = chatHub;
    }
   

    public async Task<string> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
    {
        Task t = _chatHub.SendMessage(request.AppSectionThatSentTheNotification, request.Message);
        return t.ToString();
    }
}