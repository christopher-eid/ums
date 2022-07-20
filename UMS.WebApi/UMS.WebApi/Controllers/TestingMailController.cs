using Application.Services.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingMailController : BaseController
{

    public TestingMailController(IMediator mediator)
    {
        _mediator = mediator;

    }


    [HttpPost("TestMail")]
    public async Task<IActionResult> TestMailAndPushNotification([FromForm] SendEmailCommand request)
    {
        
        var result = await _mediator.Send(new SendEmailCommand()
        {
            ToEmail = request.ToEmail,
            Subject = request.Subject,
            Body = request.Body
        });


        return Ok(result);
        
    }
}