using LibraryPlus.Models;
using LibraryPlus.Services;
using LibraryPlus.UserRequests;

namespace LibraryPlus.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/users");

        group.MapPost("/signup", async (SignupRequest request, AuthService authService) =>
        {
            var success = await authService.RegisterUserAsync(request);

            if (!success)
            {
                return Results.BadRequest(new { Message = "Email is already taken" });
            }

            return Results.Ok(new { Message = "User created successfully" });
        });

        group.MapPost("/login", async (LoginRequest request, AuthService authService) =>
        {
            var token = await authService.LoginAsync(request);

            if (token == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { Token = token });
        });
    }
}
