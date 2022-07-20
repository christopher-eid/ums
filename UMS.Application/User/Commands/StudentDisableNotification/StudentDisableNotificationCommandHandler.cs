using Application.Exceptions;
using Application.Models;
using Application.User.Commands.TeacherAssignCourseToTime;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.StudentDisableNotification;

public class StudentDisableNotificationCommandHandler : IRequestHandler<StudentDisableNotificationCommand, string>
{
    private readonly UmsContext _umsContext;

    private readonly IMapper _mapper;

    public StudentDisableNotificationCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;
    }

    public async Task<string> Handle(StudentDisableNotificationCommand request, CancellationToken cancellationToken)
    {
        
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.StudentId & x.RoleId == 3);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("This student ID was not found");
        }

        existingTeacher.EnableNotifications = 0;
        //var response =  _umsContext.Users.Update(existingCourseAndTeacher);
        _umsContext.SaveChanges();
        

        return "Notifications are disabled";

    }
}