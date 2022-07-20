using Application.Services.PushNotification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingSignalRController : BaseController
{

    public TestingSignalRController(IMediator mediator)
    {
        _mediator = mediator;

    }


    [HttpPost("TestPushNotification")]
    public async Task<IActionResult> TestMailAndPushNotification(string notificationMessage)
    {
        

        var result1 = await _mediator.Send(new PushNotificationCommand()
        {
            AppSectionThatSentTheNotification = "Testing Notification",
            Message = notificationMessage
        });
        
        
        return Ok(result1);
        
    }
}