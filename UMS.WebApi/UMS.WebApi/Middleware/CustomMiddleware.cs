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
        Console.WriteLine("Date: " + DateTime.Now.ToLongDateString());
        return _next(httpContext);
    }
}

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseDateLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}