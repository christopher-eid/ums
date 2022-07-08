using Domain.Models;
using MediatR;

namespace Application.Models;

public class TeacherSessionDto : IRequest<TeacherCourseDto>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}