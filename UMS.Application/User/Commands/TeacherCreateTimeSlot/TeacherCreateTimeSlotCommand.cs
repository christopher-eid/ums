using System.ComponentModel.DataAnnotations;
using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.User.Commands.TeacherCreateTimeSlot;

public class TeacherCreateTimeSlotCommand : IRequest<TeacherSessionDto>
{
    
    [Required(ErrorMessage = "Teacher ID is required")]
    public long? TeacherId { get; set; }
    
    [Required(ErrorMessage = "Start time is required")]
    public DateTime? StartTime { get; set; }
    
    [Required(ErrorMessage = "End time is required")]
    public DateTime? EndTime { get; set; }
    
}