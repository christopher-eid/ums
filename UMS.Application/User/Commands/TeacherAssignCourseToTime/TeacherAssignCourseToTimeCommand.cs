using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherAssignCourseToTime;

public class TeacherAssignCourseToTimeCommand : IRequest<CourseSessionDto>
{
    public long TeacherPerCourseId { get; set; }
    public long SessionTimeId { get; set; }
    
}