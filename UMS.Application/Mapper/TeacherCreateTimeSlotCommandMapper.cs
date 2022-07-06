using Application.Models;
using Application.User.Commands.TeacherCreateTimeSlot;
using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class TeacherCreateTimeSlotCommandMapper : Profile
{
    public TeacherCreateTimeSlotCommandMapper()
    {
        CreateMap<TeacherCreateTimeSlotCommand, SessionTime>();

    }
}