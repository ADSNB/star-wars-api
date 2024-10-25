using article_jwt_auth.Models;
using article_jwt_auth.Services;
using Microsoft.AspNetCore.Authorization;

namespace star_wars_api.Routes
{
    public static class AuthenticateRoute
    {
        public static void AuthenticateRoutes(this WebApplication app)
        {
            app.MapPost("/v1/authenticate/", [AllowAnonymous] (User user) =>
            {
                if (user.Email == "yoda@gmail.com" && user.Password == "maytheforcebewithyou")
                    return Results.Ok(JwtBearerService.GenerateToken(user));

                return Results.Unauthorized();
            });
        }
    }
}
