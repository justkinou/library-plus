using System.Security.Claims;
using LibraryPlus.Endpoints.User.Dto;
using LibraryPlus.Filters;
using LibraryPlus.Requests;
using LibraryPlus.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryPlus.Endpoints.User;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/user");
        group.AddEndpointFilter<ActiveUserFilter>();
        
        group.MapGet("/meShort", [Authorize] async (ClaimsPrincipal claims, UserService userService) =>
        {
            var userId = claims.FindFirstValue("sub")!;
            var user = (await userService.GetUserByIdAsync(userId))!;
            return new MeResponseShortDto(user.Email, user.Name, user.AvatarUrl);
        });

        group.MapGet("/me", [Authorize] async (ClaimsPrincipal claims, UserService userService) =>
        {
            var userId = claims.FindFirstValue("sub")!;
            var user = (await userService.GetUserByIdAsync(userId))!;
            return MeResponseDto.FromModel(user);
        });

        group.MapPatch("/updateAddress", [Authorize] async (
            ClaimsPrincipal claims,
            UserService userService,
            [FromBody] UpdateAddressRequest updateAddressRequest
        ) => {
            var userId = claims.FindFirstValue("sub")!;
            await userService.UpdateAddress(userId, updateAddressRequest);
        });

        group.MapPatch("/updatePhoneNumber", [Authorize] async (
            ClaimsPrincipal claims,
            UserService userService,
            [FromBody] UpdatePhoneNumberRequestDto updatePhoneNumberDto) =>
        {
            var userId = claims.FindFirstValue("sub")!;
            await userService.UpdatePhoneNumber(userId, updatePhoneNumberDto.NewPhoneNumber);
        });
    }
}