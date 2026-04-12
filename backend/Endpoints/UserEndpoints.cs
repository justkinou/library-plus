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

        group.MapPost("/login", async (LoginRequest request, UserService userService, IConfiguration config) =>
        {
            var user = await userService.VerifyUserLogin(request.Email, request.Password);

            if (user == null)
            {
                return Results.Unauthorized();
            }

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"]!);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Email),

    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = config["Jwt:Issuer"],
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
            new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
            Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Results.Ok(new { Token = tokenHandler.WriteToken(token) });
        });
    }
}
