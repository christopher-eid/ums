using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;
    
}