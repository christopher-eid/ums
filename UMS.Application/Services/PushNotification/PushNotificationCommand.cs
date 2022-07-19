using MediatR;

namespace Application.Services.PushNotification;

public class PushNotificationCommand : IRequest<string>
{
    public string AppSectionThatSentTheNotification { get; set; }
    public string Message { get; set; }
   
 
    
}