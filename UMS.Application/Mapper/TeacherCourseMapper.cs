using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class TeacherCourseMapper : Profile
{
    public TeacherCourseMapper()
    {
        CreateMap<TeacherPerCourse, TeacherCourseDto>();

    }
}