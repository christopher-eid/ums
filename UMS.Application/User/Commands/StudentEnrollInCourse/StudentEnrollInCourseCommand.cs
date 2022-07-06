using Application.Models;
using MediatR;

namespace Application.User.Commands.StudentEnrollInCourse;

public class StudentEnrollInCourseCommand: IRequest<StudentEnrollInCourseDto>
{
    public long StudentId { get; set; }
    public long ClassId { get; set; }
}