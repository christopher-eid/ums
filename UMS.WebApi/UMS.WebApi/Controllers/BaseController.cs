using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS.Infrastructure.Abstraction.Interfaces;

namespace UMS.WebApi.Controllers;

public class BaseController : ControllerBase
{
    public IMediator _mediator;
}