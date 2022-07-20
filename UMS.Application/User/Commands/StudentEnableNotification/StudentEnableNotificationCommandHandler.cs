using Application.Exceptions;
using Application.Models;
using AutoMapper;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.StudentEnableNotification;

public class StudentEnableNotificationCommandHandler : IRequestHandler<StudentEnableNotificationCommand, string>
{
    private readonly UmsContext _umsContext;

    private readonly IMapper _mapper;

    public StudentEnableNotificationCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;
    }

    public async Task<string> Handle(StudentEnableNotificationCommand request, CancellationToken cancellationToken)
    {
        

        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.StudentId & x.RoleId == 3);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("This student ID was not found");
        }

        existingTeacher.EnableNotifications = 1;
        //var response =  _umsContext.Users.Update(existingCourseAndTeacher);
        _umsContext.SaveChanges();
        

        return "Notifications are enabled";

    }
}