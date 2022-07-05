using Application.User.Commands.AdminCreateCourse;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Persistence.Models;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : BaseController
{

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }




    [HttpGet("GetAllUsers")]
    public async Task<string> GetAllUsers()
    {
        return "hello";
    }

    
    [HttpPost("AdminCreateCourse")]
    public async Task<IActionResult> Add([FromBody] AdminCreateCourseCommand request)
    {
        //no need to use automappers here since so sensitive info
        
        
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



    
}