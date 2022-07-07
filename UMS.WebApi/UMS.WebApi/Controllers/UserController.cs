
using Application.User.Commands.AdminCreateCourse;
using Application.User.Commands.SendEmail;
using Application.User.Commands.StudentEnrollInCourse;
using Application.User.Commands.TeacherAssignCourseToTime;
using Application.User.Commands.TeacherCreateTimeSlot;
using Application.User.Commands.TeacherRegisterCourse;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Infrastructure.Abstraction.Interfaces;


namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{

    public UserController(IMediator mediator, IMailService1 mailService1)
    {
        _mediator = mediator;
        _mailService1 = mailService1;
    }




    [HttpGet("TestConnection")]
    public async Task<string> TestConnection()
    {
        return "hello";
    }

    
    [HttpPost("AdminCreateCourse")]
    public async Task<IActionResult> AdminCreateCourse([FromBody] AdminCreateCourseCommand request)
    {
       
        
        
        var result = await _mediator.Send(new AdminCreateCourseCommand
        {
           Name = request.Name, 
           MaxStudentsNumber = request.MaxStudentsNumber,
           LowerBound = request.LowerBound,
           UpperBound = request.UpperBound
        });

        return Ok(result);
    }

/*  JSON example for testing adminCreateCourse on swagger
  
    {
  "name": "string",
  "maxStudentsNumber": 0,
  "lowerBound":"2019-01-06",
  "upperBound": "2022-01-06"
  
  
}*/

    [HttpPost("TeacherRegisterCourse")]
    public async Task<IActionResult> TeacherRegisterCourse([FromBody] TeacherRegisterCourseCommand request)
    {
        var result = await _mediator.Send(new TeacherRegisterCourseCommand()
        {
            TeacherId = request.TeacherId,
            CourseId = request.CourseId
        });

        return Ok(result);
    }

    
    
    [HttpPost("TeacherCreateTimeSlot")]
    public async Task<IActionResult> TeacherCreateTimeSlot([FromBody] TeacherCreateTimeSlotCommand request)
    {
        var result = await _mediator.Send(new TeacherCreateTimeSlotCommand()
        {
            StartTime = request.StartTime,
            EndTime = request.EndTime
        });

        return Ok(result);
    }
    
    
    [HttpPost("TeacherAssignCourseToTime")]
    public async Task<IActionResult> TeacherAssignCourseToTime([FromBody] TeacherAssignCourseToTimeCommand request)
    {
        var result = await _mediator.Send(new TeacherAssignCourseToTimeCommand()
        {
            TeacherPerCourseId = request.TeacherPerCourseId,
            SessionTimeId = request.SessionTimeId
        });

        return Ok(result);
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
    
    
    
    
    /*
    [HttpPost("UserSignUp")]
    public async Task<IActionResult> UserSignUp(StudentEnrollInCourseCommand request)
    {
       Key
    }
    */
    
    
    
    [HttpPost("send")]
    public async Task<string> SendMail([FromForm] SendEmailCommand request)
    {
        
        var result = await _mediator.Send(new SendEmailCommand()
        {
           ToEmail = request.ToEmail,
           Subject = request.Subject,
           Body = request.Body
        });

        return result;
    }
    
    
    /* JSON example for testing teacherRegisterCourse on swagger
{
  "teacherId": 1,
  "courseId": 4,
  "startTime": "2022-07-05T11:50:39",
  "endTime": "2022-07-05T12:50:39",
  "duration": 1
} 
*/

}