using System.ComponentModel.DataAnnotations;
using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherAssignCourseToTime;

public class TeacherAssignCourseToTimeCommand : IRequest<CourseSessionDto>
{
    
    
    [Required(ErrorMessage = "Teacher ID is required")]
    public long? TeacherId { get; set; }
    
    [Required(ErrorMessage = "Teacher per course ID is required")]
    public long? TeacherPerCourseId { get; set; }
    
    [Required(ErrorMessage = "Session time ID is required")]
    public long? SessionTimeId { get; set; }
    
}