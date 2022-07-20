using System.ComponentModel.DataAnnotations;
using Domain.Models;
using MediatR;

namespace Application.Models;

public class SignUpInfoDto
{

    [Required(ErrorMessage = "Name field is required")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Email field is required")]
    [EmailAddress]
    public string? Email { get; set; }
}