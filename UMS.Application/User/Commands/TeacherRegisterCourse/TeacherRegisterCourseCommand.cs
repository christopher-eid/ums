using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherRegisterCourse;

public class TeacherRegisterCourseCommand : IRequest<TeacherCourseDto>
{
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
    
}