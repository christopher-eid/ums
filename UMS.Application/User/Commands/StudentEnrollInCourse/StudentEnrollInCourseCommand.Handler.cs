using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.StudentEnrollInCourse;

public class StudentEnrollInCourseCommandHandler : IRequestHandler<StudentEnrollInCourseCommand,StudentEnrollInCourseDto >
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;

    public StudentEnrollInCourseCommandHandler(UmsContext db, IMapper mapper)
    {
        _umsContext = db;
        _mapper = mapper;
    }
   

    public async Task<StudentEnrollInCourseDto> Handle(StudentEnrollInCourseCommand request, CancellationToken cancellationToken)
    {

        var classAndTeacherFound = _umsContext.TeacherPerCourses.FirstOrDefault(x => x.Id == request.ClassId);

        if (classAndTeacherFound == null)
        {
            throw new NotFoundException("Requested Course was not Found");
        }

        var studentIdFound = _umsContext.Users.FirstOrDefault(x => x.Id == request.StudentId & x.RoleId == 3);
        
        if (studentIdFound == null)
        {
            throw new InvalidIdentifierException("Invalid ID");
        }

        DateTime dateAndTimeRegistered = DateTime.Now;
        DateOnly dateRegistered = DateOnly.FromDateTime(dateAndTimeRegistered);
        //find if the course we want has a valid enrollment date
        var validEnrollmentDate = _umsContext.Courses.FirstOrDefault(x => x.Id == classAndTeacherFound.CourseId & 
                                                                          dateRegistered >= x.EnrolmentDateRange.Value.LowerBound &
                                                                          dateRegistered <= x.EnrolmentDateRange.Value.UpperBound );

        if (validEnrollmentDate == null)
        {
            throw new NotPossibleException("Enrolment date is over");
        }
        
        //finally if everything went fine, the student registration goes well
        ClassEnrollment newEnrollment = _mapper.Map<ClassEnrollment>(request);
        var res = await _umsContext.ClassEnrollments.AddAsync(newEnrollment);
        
        _umsContext.SaveChanges();

        StudentEnrollInCourseDto response = new StudentEnrollInCourseDto()
        {
            StudentId = res.Entity.StudentId,
            ClassId = res.Entity.ClassId,
            TimeEnrolledAt = dateAndTimeRegistered
        };
       return response;
    }
}