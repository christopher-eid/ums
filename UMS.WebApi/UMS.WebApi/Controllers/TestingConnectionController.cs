using Microsoft.AspNetCore.Mvc;
using Npgsql;


namespace UMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingConnectionController : BaseController
{

    public TestingConnectionController(){
       

    }

    [HttpGet("TestConnection")]
    public async Task<string> TestConnection([FromHeader] long tenant)
    {
  
        return "hello";
    }


    
    
}