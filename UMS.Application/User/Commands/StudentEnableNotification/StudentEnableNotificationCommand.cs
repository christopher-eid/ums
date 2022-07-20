using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;

namespace Application.User.Commands.StudentEnableNotification;

public class StudentEnableNotificationCommand: IRequest<string>
{
    [Required]
    public long StudentId { get; set; }
}