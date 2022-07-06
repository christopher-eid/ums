using Application.Models;
using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class TeacherSessionMapper : Profile
{
    public TeacherSessionMapper()
    {
        CreateMap<SessionTime, TeacherSessionDto>();

    }
}