using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ITsOkayAPI.DataAccess;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppDbContext _context;

    public TokenValidationMiddleware(RequestDelegate next, AppDbContext context)
    {
        _next = next;
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var username = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                var user = _context.Usuarios.FirstOrDefault(u => u.Nombre == username);

                if (user != null && user.LastToken != token)
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("Token inválido o ha sido reemplazado.");
                    return;
                }
            }
        }

        await _next(context);
    }
}
