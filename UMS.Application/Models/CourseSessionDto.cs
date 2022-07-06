using Domain.Models;
using MediatR;

namespace Application.Models;

public class CourseSessionDto
{
    public long Id { get; set; }
    public long TeacherPerCourseId { get; set; }
    public long SessionTimeId { get; set; }
    
}