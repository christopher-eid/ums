using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;

namespace Application.User.Commands.StudentDisableNotification;

public class StudentDisableNotificationCommand: IRequest<string>
{
    [Required]
    public long StudentId { get; set; }
}