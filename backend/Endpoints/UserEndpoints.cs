using System.Security.Claims;
using LibraryPlus.Services;
using LibraryPlus.UserRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

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
            var tokens = await authService.LoginAsync(request);

            if (tokens == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(tokens);
        });

        group.MapPost("/refresh", async (RefreshRequest request, AuthService authService) =>
        {
            var response = await authService.RefreshTokenAsync(request);

            if (response == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(response);
        });

        group.MapGet("/welcome", [Authorize] (ClaimsPrincipal user) =>
        {
            var userEmail = user.Identity?.Name;

            return Results.Ok(new { Message = $"Welcome, {userEmail}!" });
        });
    }
}
