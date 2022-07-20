using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;

namespace Application.User.Commands.StudentEnrollInCourse;

public class StudentEnrollInCourseCommand: IRequest<StudentEnrollInCourseDto>
{
    
    [Required(ErrorMessage = "Student ID is required")]
    public long StudentId { get; set; }
    
    [Required(ErrorMessage = "Class ID is required")]
    public long ClassId { get; set; }
}