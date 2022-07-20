﻿using Application.User.Commands.AdminCreateCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : BaseController
{

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;

    }


    [HttpPost("AdminCreateCourse")]
    public async Task<IActionResult> AdminCreateCourse([FromBody] AdminCreateCourseCommand request)
    {
       
        
        
        var result = await _mediator.Send(new AdminCreateCourseCommand
        {
            AdminId = request.AdminId,
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