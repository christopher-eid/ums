using Domain.Models;
using MediatR;

namespace Application.Models;

public class StudentEnrollInCourseDto
{
    public long StudentId { get; set; }
    public long ClassId { get; set; }
    public DateTime TimeEnrolledAt { get; set; }
}