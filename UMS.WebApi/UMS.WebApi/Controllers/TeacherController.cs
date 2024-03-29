﻿using Application.Models;
using Application.User.Commands.SignUp;
using Application.User.Commands.TeacherAssignCourseToTime;
using Application.User.Commands.TeacherCreateTimeSlot;
using Application.User.Commands.TeacherRegisterCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : BaseController
{

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;

    }
    
    
    
    
    [HttpPost("SignUp")]
    public async Task<IActionResult> AdminSignUp([FromBody] SignUpInfoDto request)
    {
       
        var result = await _mediator.Send(new SignUpCommand()
        {
            Name = request.Name,
            Email = request.Email,
            RoleId = 2
        });

        return Ok(result);
    }

    
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
            TeacherId = request.TeacherId,
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
            TeacherId = request.TeacherId,
            TeacherPerCourseId = request.TeacherPerCourseId,
            SessionTimeId = request.SessionTimeId
        });

        return Ok(result);
    }


}