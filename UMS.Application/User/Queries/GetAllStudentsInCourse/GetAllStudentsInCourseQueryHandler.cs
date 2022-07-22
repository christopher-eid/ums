using Application.Exceptions;
using Application.User.Commands.StudentDisableNotification;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Queries.GetAllStudentsInCourse;

public class GetAllStudentsInCourseQueryHandler : IRequestHandler<GetAllStudentsInCourseQuery, List<Domain.Models.User>>
{
    private readonly UmsContext _umsContext;

 

    public GetAllStudentsInCourseQueryHandler( UmsContext umsContext)
    {
        _umsContext = umsContext;
        
    }

    public async Task<List<Domain.Models.User>> Handle(GetAllStudentsInCourseQuery request, CancellationToken cancellationToken)
    {
        
        var data = _umsContext.ClassEnrollments
            .Join(
                _umsContext.TeacherPerCourses,
                classEnrollment => classEnrollment.ClassId,
                teacherPerCourse => teacherPerCourse.Id,
                (classEnrollment, teacherPerCourse) => new
                {
                    studentEnrolled = classEnrollment.StudentId,
                    courseId = teacherPerCourse.CourseId
                }
            ).ToList();

        List<long> studentIds = new List<long>();
        foreach (var variable in data)
        {
            if (variable.courseId == request.CourseId)
            {
                studentIds.Add(variable.studentEnrolled);
            }
        }


        var fullInfo = _umsContext.Users.Where(x => studentIds.Contains(x.Id)).ToList();

        

        
        

      
        
        
        
        
        /*
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.StudentId & x.RoleId == 3);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("This student ID was not found");
        }

        existingTeacher.EnableNotifications = 0;
        //var response =  _umsContext.Users.Update(existingCourseAndTeacher);
        _umsContext.SaveChanges();*/
        

        return fullInfo;

    }
}