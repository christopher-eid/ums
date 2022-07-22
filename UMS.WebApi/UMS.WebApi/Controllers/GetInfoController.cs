using Application.Models;
using Application.User.Commands.AdminCreateCourse;
using Application.User.Commands.SignUp;
using Application.User.Queries.GetAllStudentsInCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GetInfoController : BaseController
{

    public GetInfoController(IMediator mediator)
    {
        _mediator = mediator;

    }

    
    [HttpGet("GetAllStudentsInCourse")]
    public async Task<IActionResult> GetAllStudentsInCourse([FromQuery] GetAllStudentsInCourseQuery request)
    {
       
        
        
        var result = await _mediator.Send(new GetAllStudentsInCourseQuery()
        {
            CourseId = request.CourseId
        });

        return Ok(result);
    }
    

}