using System.ComponentModel.DataAnnotations;
using Application.Models;
using Domain.Models;
using MediatR;
using NpgsqlTypes;

namespace Application.User.Commands.AdminCreateCourse;

public class AdminCreateCourseCommand : IRequest<CourseDto>
{
    [Required(ErrorMessage = "ID is required")]
    
    public long AdminId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Number of students is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Number of students cannot be negative")]
    public int? MaxStudentsNumber { get; set; }
    
    
    [Required(ErrorMessage = "Lowerbound datetime is required")]
    public DateTime LowerBound { get; set; } //use DateTime instead of DateOnly 
    
    [Required(ErrorMessage = "Upperbound datetime is required")]
    public DateTime UpperBound { get; set; }
}