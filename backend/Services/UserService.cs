using MongoDB.Driver;
using LibraryPlus.Models;
using LibraryPlus.UserRequests;

namespace LibraryPlus.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IMongoClient mongoClient, IConfiguration config)
    {
        var databaseName = config["MongoDbSettings:DatabaseName"];
        var database = mongoClient.GetDatabase(databaseName);
        _users = database.GetCollection<User>("users");
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var existingUser = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        return existingUser != null;
    }

    public async Task CreateUser(SignupRequest request)
    {
        var newUser = new User
        {
            Email = request.Email,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            AvatarUrl = request.AvatarURL,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            JoinedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await _users.InsertOneAsync(newUser);
    }

}
