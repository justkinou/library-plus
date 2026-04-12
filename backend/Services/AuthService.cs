

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryPlus.Models;
using LibraryPlus.UserRequests;
using Microsoft.IdentityModel.Tokens;

namespace LibraryPlus.Services;

public class AuthService
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;

    public AuthService(UserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    public async Task<bool> RegisterUserAsync(SignupRequest request)
    {
        if (await _userService.IsEmailTaken(request.Email))
        {
            return false;
        }

        await _userService.CreateUser(request);
        return true;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await _userService.VerifyUserLogin(request.Email, request.Password);

        if (user == null)
        {
            return null;
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _config["Jwt:Issuer"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
