namespace UMS.WebApi.Middleware;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        Console.WriteLine("THIS IS MY MIDDLEWARE -- I AM ALWAYS BEFORE EACH REQUEST U SEND!");
        return _next(httpContext);
    }
}
