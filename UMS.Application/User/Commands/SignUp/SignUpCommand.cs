using System.ComponentModel.DataAnnotations;
using Application.Models;
using MediatR;

namespace Application.User.Commands.SignUp;

public class SignUpCommand: IRequest<SignUpReturnInfoDto>
{
    [Required(ErrorMessage = "Name field is required")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Email field is required")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Role ID is required")]
    public long RoleId { get; set; }
}