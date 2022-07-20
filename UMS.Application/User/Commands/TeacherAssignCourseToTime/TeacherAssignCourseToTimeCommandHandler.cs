using Application.Exceptions;
using Application.Models;
using Application.User.Commands.TeacherRegisterCourse;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.TeacherAssignCourseToTime;

public class TeacherAssignCourseToTimeCommandHandler : IRequestHandler<TeacherAssignCourseToTimeCommand, CourseSessionDto>
{
    private readonly UmsContext _umsContext;

    private readonly IMapper _mapper;

    public TeacherAssignCourseToTimeCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;
    }

    public async Task<CourseSessionDto> Handle(TeacherAssignCourseToTimeCommand request, CancellationToken cancellationToken)
    {
        
        
        
        //teacher authorization based on provided ID
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.TeacherId & x.RoleId == 2);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("Provided ID is not valid");
        }

        
        
       
        long newTeacherPerCourseId = (long)request.TeacherPerCourseId;  //i had to put the command attributes as nullable in order to use required with decorators
                                                                         //so here i switched it back to long instead of long?
        long newSessionTimeId = (long)request.SessionTimeId;
        
        
        var existingCourseAndTeacher = _umsContext.TeacherPerCourses.FirstOrDefault(x => x.Id == request.TeacherPerCourseId);
        if (existingCourseAndTeacher == null)
        {
            throw new NotPossibleException("You did not register this course");
        }
        
        var existingTimeSlot = _umsContext.SessionTimes.FirstOrDefault(x => x.Id == request.SessionTimeId);
        if (existingTimeSlot == null)
        {
            throw new NotPossibleException("You did not register this time slot");
        }
        
        
        TeacherPerCoursePerSessionTime newCourseToTimeSlot = _mapper.Map<TeacherPerCoursePerSessionTime>(request);


        var response = await _umsContext.TeacherPerCoursePerSessionTimes.AddAsync(newCourseToTimeSlot);
        
        
        _umsContext.SaveChanges();
        
        
        CourseSessionDto detailsToReturn = _mapper.Map<CourseSessionDto>(response.Entity);


        return detailsToReturn;

    }
}