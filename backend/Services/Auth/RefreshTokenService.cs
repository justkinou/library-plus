using System.Security.Cryptography;
using System.Text;
using LibraryPlus.Models;
using MongoDB.Driver;

namespace LibraryPlus.Services.Auth;

public class RefreshTokenService
{
    private readonly IMongoCollection<RefreshToken> _refreshTokens;
    private readonly UserService _userService;

    public RefreshTokenService(IMongoClient mongoClient, IConfiguration config, UserService userService)
    {
        var databaseName = config["MongoDbSettings:DatabaseName"];
        var database = mongoClient.GetDatabase(databaseName);
        _userService = userService;
        _refreshTokens = database.GetCollection<RefreshToken>("refreshTokens");

        var indexKeys = Builders<RefreshToken>.IndexKeys.Ascending(t => t.ExpiryDate);
        var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.Zero };
        _refreshTokens.Indexes.CreateOne(new CreateIndexModel<RefreshToken>(indexKeys, indexOptions));
    }


    public async Task<User?> GetUserByRefreshToken(string refreshTokenPlain)
    {
        string refreshTokenHash = HashToken(refreshTokenPlain);
        RefreshToken? refreshToken = await _refreshTokens
            .Find(t => t.RefreshTokenHash == refreshTokenHash)
            .FirstOrDefaultAsync();
        if (refreshToken == null)
        {
            return null;
        }
        return await _userService.GetUserByIdAsync(refreshToken.UserId);
    }

    public async Task<string> AddRefreshToken(string userId)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        string refreshTokenPlain = Convert.ToBase64String(randomNumber);
        string refreshTokenHash = HashToken(refreshTokenPlain);

        RefreshToken refreshToken = new()
        {
            RefreshTokenHash = refreshTokenHash,
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
        };

        await _refreshTokens.InsertOneAsync(refreshToken);
        return refreshTokenPlain;
    }

    public async Task<bool> RemoveRefreshToken(string refreshTokenPlain)
    {
        string refreshTokenHash = HashToken(refreshTokenPlain);
        var result = await _refreshTokens.DeleteOneAsync(t => t.RefreshTokenHash == refreshTokenHash);
        return result.DeletedCount == 1;
    }

    private static string HashToken(string token)
    {
        var bytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }

}