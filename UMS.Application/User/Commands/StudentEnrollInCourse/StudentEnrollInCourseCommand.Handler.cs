using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.User.Commands.StudentEnrollInCourse;

public class StudentEnrollInCourseCommandHandler : IRequestHandler<StudentEnrollInCourseCommand,StudentEnrollInCourseDto >
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    private readonly IMailService1 _mailService;
    public StudentEnrollInCourseCommandHandler(UmsContext db, IMapper mapper, IMailService1 mailservice)
    {
        _umsContext = db;
        _mapper = mapper;
        _mailService = mailservice;
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

        
        var alreadyEnrolled = _umsContext.ClassEnrollments.FirstOrDefault(x => x.StudentId == request.StudentId & x.ClassId == request.ClassId);

        if (alreadyEnrolled != null)
        {
            throw new AlreadyEnrolledException("You have already enrolled in this course");
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

        
        var studentInvolved = _umsContext.Users.FirstOrDefault(x => x.Id == res.Entity.StudentId & x.RoleId == 3);
    
        if (studentInvolved != null & studentInvolved.EnableNotifications == 1)
        {

            var courseInvolved = _umsContext.Courses.FirstOrDefault(x => x.Id == classAndTeacherFound.CourseId);
            if ( courseInvolved != null)
            {
                MailRequest c = new MailRequest()
                {
                    ToEmail = studentInvolved.Email,
                    Subject = courseInvolved.Name,
                    Body = "Welcome to the class!!",
                };
                await _mailService.SendEmailAsync(c);
            }
        }


        StudentEnrollInCourseDto response = new StudentEnrollInCourseDto()
        {
            StudentId = res.Entity.StudentId,
            ClassId = res.Entity.ClassId,
            TimeEnrolledAt = dateAndTimeRegistered
        };
       return response;
    }
}