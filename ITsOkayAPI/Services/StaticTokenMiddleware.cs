using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class StaticTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public StaticTokenMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var extractedToken))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token no provisto.");
            return;
        }

        var staticToken = _configuration["Jwt:StaticToken"];
        if (!staticToken.Equals(extractedToken.ToString().Replace("Bearer ", "")))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token no válido.");
            return;
        }

        await _next(context);
    }
}
