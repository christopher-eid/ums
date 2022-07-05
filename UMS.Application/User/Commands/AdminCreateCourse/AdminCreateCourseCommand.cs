using Domain.Models;
using MediatR;
using NpgsqlTypes;

namespace Application.User.Commands.AdminCreateCourse;

public class AdminCreateCourseCommand : IRequest<CourseDto>
{
    public string? Name { get; set; }
    public int? MaxStudentsNumber { get; set; }
    
    public DateOnly LowerBound { get; set; }
    
    public DateOnly UpperBound { get; set; }
}