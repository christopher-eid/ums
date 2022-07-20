using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingConnectionController : BaseController
{

    public TestingConnectionController(){
       

    }

    [HttpGet("TestConnection")]
    public async Task<string> TestConnection()
    {
        return "hello";
    }


    
    
}