using Application.Exceptions;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.TeacherRegisterCourse;

public class TeacherRegisterCourseCommandHandler : IRequestHandler<TeacherRegisterCourseCommand, TeacherCourseDto>
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    public TeacherRegisterCourseCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;

    }

    public async Task<TeacherCourseDto> Handle(TeacherRegisterCourseCommand request, CancellationToken cancellationToken)
    {
        var existingCourse = _umsContext.Courses.FirstOrDefault(x => x.Id == request.CourseId);
        if (existingCourse == null)
        {
            throw new AlreadyExistingException("Incorrect Course ID");
        }
        
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.TeacherId & x.RoleId == 2);
        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("Incorrect ID");
        }

        var alreadyReg =
            _umsContext.TeacherPerCourses.FirstOrDefault(x =>
                x.CourseId == request.CourseId & x.TeacherId == request.CourseId);

        if (alreadyReg != null)
        {
            throw new AlreadyExistingException("You already registered this class");
        }
        
        TeacherPerCourse newTeacherPerCourse = new TeacherPerCourse()
        {
            TeacherId = request.TeacherId,
            CourseId = request.CourseId
        };

        var newTeacherPerCourseResponse = await _umsContext.TeacherPerCourses.AddAsync(newTeacherPerCourse);

        
        
        _umsContext.SaveChanges();
       
        TeacherCourseDto teacherCourseResult = _mapper.Map<TeacherCourseDto>(newTeacherPerCourseResponse.Entity);

        
        return teacherCourseResult;
    }
}