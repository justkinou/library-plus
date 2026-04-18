using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibraryPlus.Models;
using LibraryPlus.UserRequests;
using Microsoft.IdentityModel.Tokens;

namespace LibraryPlus.Services;

public class AuthService(UserService userService, IConfiguration config)
{
    private readonly UserService _userService = userService;
    private readonly IConfiguration _config = config;

    private static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hashBytes);
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

    public async Task<TokenResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userService.VerifyUserLogin(request.Email, request.Password);
        if (user == null) return null;

        var accessToken = GenerateJwtToken(user);
        var plainTextRefreshToken = GenerateRefreshToken();

        user.RefreshToken = HashToken(plainTextRefreshToken);
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userService.UpdateUser(user);

        return new TokenResponse(accessToken, plainTextRefreshToken);
    }

    public async Task<AccessTokenResponse?> RefreshTokenAsync(RefreshRequest request)
    {
        var hashedToken = HashToken(request.RefreshToken);
        var user = await _userService.GetUserByRefreshToken(hashedToken);

        if (user == null ||
            string.IsNullOrEmpty(user.RefreshToken) ||
            !user.RefreshTokenExpiryTime.HasValue ||
            user.RefreshTokenExpiryTime.Value <= DateTime.UtcNow)
        {
            return null;
        }

        var newAccessToken = GenerateJwtToken(user);

        return new AccessTokenResponse(newAccessToken);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
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
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("avatarUrl", user.AvatarUrl ?? ""),
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
        var hashedToken = HashToken(refreshToken);
        var user = await _userService.GetUserByRefreshToken(hashedToken);

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
