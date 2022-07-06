using Application.User.Commands.StudentEnrollInCourse;
using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class StudentEnrollCommandToClassEnrollmentMapper : Profile
{
    public StudentEnrollCommandToClassEnrollmentMapper()
    {
        CreateMap<StudentEnrollInCourseCommand, ClassEnrollment>();

    }
}