using LibraryPlus.Models;
using LibraryPlus.Services;
using LibraryPlus.UserRequests;

namespace LibraryPlus.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/users");

        group.MapPost("/signup", async (SignupRequest request, UserService userService) =>
        {
            if (await userService.IsEmailTaken(request.Email))
            {
                return Results.BadRequest(new { Message = "Email is already taken" });
            }

            await userService.CreateUser(request);
            return Results.Ok(new { Message = "User created successfully" });
        });
    }
}
