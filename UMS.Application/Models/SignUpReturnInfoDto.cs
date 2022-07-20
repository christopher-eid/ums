using System.ComponentModel.DataAnnotations;
using Domain.Models;
using MediatR;

namespace Application.Models;

public class SignUpReturnInfoDto
{

    public long Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Email { get; set; }
}