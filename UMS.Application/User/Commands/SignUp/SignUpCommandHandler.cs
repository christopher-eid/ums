using Application.Exceptions;
using Application.Models;
using AutoMapper;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpReturnInfoDto>
{
    private readonly UmsContext _umsContext;

    private readonly IMapper _mapper;

    public SignUpCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;
    }

    public async Task<SignUpReturnInfoDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        
        var existingUser = _umsContext.Users.FirstOrDefault(x => x.Email == request.Email);

        if (existingUser != null)
        {
            throw new AlreadyExistingException("User already exists");
        }

        Domain.Models.User newUser = new Domain.Models.User()
        {
            Name = request.Name,
            Email = request.Email,
            RoleId = request.RoleId,
            KeycloakId =  new Random().Next(0, 1501251).ToString(),
            EnableNotifications = 0
        };

        var res = await _umsContext.Users.AddAsync(newUser);
        _umsContext.SaveChanges();

        SignUpReturnInfoDto s = new SignUpReturnInfoDto()
        {
            Id = res.Entity.Id,
            Name = res.Entity.Name,
            Email = res.Entity.Email
        };

        return s;

    }
}