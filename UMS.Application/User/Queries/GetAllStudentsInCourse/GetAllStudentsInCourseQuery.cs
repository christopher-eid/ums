using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.User.Queries.GetAllStudentsInCourse;

public class GetAllStudentsInCourseQuery: IRequest<List<Domain.Models.User>>
{
    [Required(ErrorMessage = "Student ID is required")]
    public long CourseId { get; set; }
}