using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

public class BaseController : ControllerBase
{
    public IMediator _mediator;
    
}