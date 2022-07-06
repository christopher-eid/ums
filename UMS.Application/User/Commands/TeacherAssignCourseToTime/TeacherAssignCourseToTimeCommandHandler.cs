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
        
        long newTeacherPerCourseId = request.TeacherPerCourseId;
        
        long newSessionTimeId = request.SessionTimeId;
        
        
        
        
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
        
        
        /*var config1 = new MapperConfiguration(cfg =>
            cfg.CreateMap<TeacherAssignCourseToTimeCommand, TeacherPerCoursePerSessionTime>()
        );*/
        
        
        /*var mapper1 = new AutoMapper.Mapper(config1);*/
        TeacherPerCoursePerSessionTime newCourseToTimeSlot = _mapper.Map<TeacherPerCoursePerSessionTime>(request);


        var response = await _umsContext.TeacherPerCoursePerSessionTimes.AddAsync(newCourseToTimeSlot);
        
        
        _umsContext.SaveChanges();
        
        /*var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TeacherPerCoursePerSessionTime, CourseSessionDto>()
        );
        
        
        var mapper = new AutoMapper.Mapper(config);*/
        
        CourseSessionDto detailsToReturn = _mapper.Map<CourseSessionDto>(response.Entity);


        return detailsToReturn;

    }
}