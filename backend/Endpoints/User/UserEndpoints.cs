using System.Security.Claims;
using LibraryPlus.Endpoints.User.Dto;
using LibraryPlus.Filters;
using LibraryPlus.Services.User;
using Microsoft.AspNetCore.Authorization;

namespace LibraryPlus.Endpoints.User;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/user");
        group.AddEndpointFilter<ActiveUserFilter>();
        
        group.MapGet("/me", [Authorize] async (ClaimsPrincipal claims, UserService userService) =>
        {
            var id = claims.FindFirstValue("sub")!;
            var user = await userService.GetUserByIdAsync(id);
            return new MeResponseDto(user!.Email, user.Name, user.AvatarUrl);
        });
    }
}