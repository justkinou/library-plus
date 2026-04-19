using System.Security.Claims;
using LibraryPlus.Filters;
using LibraryPlus.Services.Auth;
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

        group.MapPost("/login", async (HttpContext context, LoginRequest request, AuthService authService) =>
        {
            var tokens = await authService.LoginAsync(request);
            if (tokens == null)
            {
                return Results.Unauthorized();
            }
            
            context.Response.Cookies.Append(
                "accessToken",
                tokens.AccessToken,
                new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                }
            );

            context.Response.Cookies.Append(
                "refreshToken",
                tokens.RefreshToken,
                new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            return Results.Ok(new { Message = "Logged in successfully" });
        });

        group.MapPost("/refresh", async (HttpContext context, AuthService authService) =>
        {
            var refreshTokenPlain = context.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshTokenPlain))
            {
                return Results.Unauthorized();
            }

            var tokenResponse = await authService.RefreshTokenAsync(refreshTokenPlain);
            if (tokenResponse == null)
            {
                return Results.Unauthorized();
            }

            context.Response.Cookies.Append(
                "accessToken",
                tokenResponse.AccessToken,
                new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                }
            );

            context.Response.Cookies.Append(
                "refreshToken",
                tokenResponse.RefreshToken,
                new CookieOptions {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                }
            );

            return Results.Ok(new { Message = "Token refreshed successfully" });
        });

        group.MapGet("/welcome", [Authorize] (ClaimsPrincipal user) =>
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            var userName = user.FindFirstValue(ClaimTypes.Name);
            return Results.Ok(new { Message = $"Welcome, {userName} {userEmail}!" });
        }).AddEndpointFilter<ActiveUserFilter>();

        group.MapPost("/logout", async (HttpContext context, AuthService authService) =>
        {
            var refreshToken = context.Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await authService.LogoutAsync(refreshToken);
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            context.Response.Cookies.Delete("accessToken", cookieOptions);
            context.Response.Cookies.Delete("refreshToken", cookieOptions);

            return Results.Ok(new { Message = "Logged out successfully" });
        });
    }
}
