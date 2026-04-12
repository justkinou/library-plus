using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryPlus.Models;
using LibraryPlus.UserRequests;
using Microsoft.IdentityModel.Tokens;

namespace LibraryPlus.Services;

public class AuthService(UserService userService, IConfiguration config)
{
    private readonly UserService _userService = userService;
    private readonly IConfiguration _config = config;

    public async Task<bool> RegisterUserAsync(SignupRequest request)
    {
        if (await _userService.IsEmailTaken(request.Email))
        {
            return false;
        }

        await _userService.CreateUser(request);
        return true;
    }

    public async Task<TokenResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userService.VerifyUserLogin(request.Email, request.Password);
        if (user == null) return null;

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userService.UpdateUser(user);

        return new TokenResponse(accessToken, refreshToken);
    }

    public async Task<AccessTokenResponse?> RefreshTokenAsync(RefreshRequest request)
    {
        var user = await _userService.GetUserByRefreshToken(request.RefreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        var newAccessToken = GenerateJwtToken(user);

        return new AccessTokenResponse(newAccessToken);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var user = await _userService.GetUserByRefreshToken(refreshToken);

        if (user == null)
        {
            return true;
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

        await _userService.UpdateUser(user);

        return true;
    }
}
