using Application.User.Commands.StudentDisableNotification;
using Application.User.Commands.StudentEnableNotification;
using Application.User.Commands.StudentEnrollInCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : BaseController
{

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;

    }

    
    
    
    [HttpPost("StudentEnrollInCourse")]
    public async Task<IActionResult> StudentEnrollInCourse(StudentEnrollInCourseCommand request)
    {
        var result = await _mediator.Send(new StudentEnrollInCourseCommand()
        {
            ClassId = request.ClassId,
            StudentId = request.StudentId
        });

        return Ok(result);
    }
    
    
    
    
    [HttpPost("EnableNotification")]
    public async Task<IActionResult> StudentEnableNotification(StudentEnableNotificationCommand request)
    {
        var result = await _mediator.Send(new StudentEnableNotificationCommand()
        {
            StudentId = request.StudentId
        });

        return Ok(result);
    }
    
    
    
    [HttpPost("DisableNotification")]
    public async Task<IActionResult> StudentEnableNotification(StudentDisableNotificationCommand request)
    {
        var result = await _mediator.Send(new StudentDisableNotificationCommand()
        {
            StudentId = request.StudentId
        });

        return Ok(result);
    }
    


}