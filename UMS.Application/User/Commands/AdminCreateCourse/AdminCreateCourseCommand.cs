using Application.Models;
using Domain.Models;
using MediatR;
using NpgsqlTypes;

namespace Application.User.Commands.AdminCreateCourse;

public class AdminCreateCourseCommand : IRequest<CourseDto>
{
    public string? Name { get; set; }
    public int? MaxStudentsNumber { get; set; }
    
    public DateTime LowerBound { get; set; } //use DateTime instead of DateOnly 
    
    public DateTime UpperBound { get; set; }
}