using System.Security.Claims;
using LibraryPlus.Services;
using LibraryPlus.UserRequests;
using Microsoft.AspNetCore.Authorization;

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

        group.MapPost("/login", async (LoginRequest request, AuthService authService, HttpContext context) =>
        {
            var tokens = await authService.LoginAsync(request);

            if (tokens == null)
            {
                return Results.Unauthorized();
            }

            context.Response.Cookies.Append("accessToken", tokens.AccessToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = DateTime.UtcNow.AddMinutes(15) });

            context.Response.Cookies.Append("refreshToken", tokens.RefreshToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = DateTime.UtcNow.AddDays(7) });

            return Results.Ok(new { Message = "Logged in successfully" });
        });

        group.MapPost("/refresh", async (AuthService authService, HttpContext context) =>
        {
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken)) return Results.Unauthorized();

            var response = await authService.RefreshTokenAsync(new RefreshRequest(refreshToken));

            if (response == null) return Results.Unauthorized();

            context.Response.Cookies.Append("accessToken", response.AccessToken,
                new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = DateTime.UtcNow.AddMinutes(15) });

            return Results.Ok(new { Message = "Token refreshed successfully" });
        });

        group.MapGet("/welcome", [Authorize] (ClaimsPrincipal user) =>
        {
            var userEmail = user.Identity?.Name;

            return Results.Ok(new { Message = $"Welcome, {userEmail}!" });
        });

        group.MapPost("/logout", async (AuthService authService, HttpContext context) =>
        {
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                await authService.LogoutAsync(refreshToken);
            }

            context.Response.Cookies.Delete("accessToken");
            context.Response.Cookies.Delete("refreshToken");

            return Results.Ok(new { Message = "Logged out successfully" });
        });
    }
}
