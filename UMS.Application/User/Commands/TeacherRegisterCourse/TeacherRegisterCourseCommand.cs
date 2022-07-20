using System.ComponentModel.DataAnnotations;
using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherRegisterCourse;

public class TeacherRegisterCourseCommand : IRequest<TeacherCourseDto>
{
    [Required(ErrorMessage = "Teacher ID is required")]
    public long? TeacherId { get; set; }
    
    [Required(ErrorMessage = "Course ID is required")]
    public long? CourseId { get; set; }
    
}