using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;

namespace Application.User.Commands.StudentEnableNotification;

public class StudentEnableNotificationCommand: IRequest<string>
{
    [Required(ErrorMessage = "Student ID is required")]
    public long StudentId { get; set; }
}