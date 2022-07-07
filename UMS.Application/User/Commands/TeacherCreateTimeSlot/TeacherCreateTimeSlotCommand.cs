using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherCreateTimeSlot;

public class TeacherCreateTimeSlotCommand : IRequest<TeacherSessionDto>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
}