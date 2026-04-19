using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraryPlus.Services;

namespace LibraryPlus.Filters;

public class ActiveUserFilter(UserService userService) : IEndpointFilter
{
    private readonly UserService _userService = userService;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Results.Unauthorized();
        }

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null || user.IsDeleted)
        {
            return Results.Unauthorized();
        }
        return await next(context);
    }
}