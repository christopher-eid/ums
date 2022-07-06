using Application.Models;
using Application.User.Commands.TeacherAssignCourseToTime;
using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class TeacherAssignCourseCommandMapper : Profile
{
    public TeacherAssignCourseCommandMapper()
    {
        CreateMap<TeacherAssignCourseToTimeCommand, TeacherPerCoursePerSessionTime>();

    }
}