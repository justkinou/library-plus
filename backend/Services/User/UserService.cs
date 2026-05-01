using MongoDB.Driver;
using UserModels = LibraryPlus.Models.User;
using LibraryPlus.Requests;

namespace LibraryPlus.Services.User;

public class UserService
{
    private readonly IMongoCollection<UserModels.UserModel> _users;

    public UserService(IMongoClient mongoClient, IConfiguration config)
    {
        var databaseName = config["MongoDbSettings:DatabaseName"];
        var database = mongoClient.GetDatabase(databaseName);
        _users = database.GetCollection<UserModels.UserModel>("users");
    }

    public async Task<UserModels.UserModel?> GetUserByIdAsync(string id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var existingUser = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        return existingUser != null;
    }

    public async Task CreateUser(SignupRequest request)
    {
        var newUser = new UserModels.UserModel
        {
            Email = request.Email,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            AvatarUrl = request.AvatarUrl,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            DeliveryAddress = new(),
            JoinedAt = DateTime.UtcNow,
            IsDeleted = false,
        };

        await _users.InsertOneAsync(newUser);
    }

    public async Task<UserModels.UserModel?> VerifyUserLogin(string email, string password)
    {
        var user = await _users.Find(u => u.Email == email && !u.IsDeleted).FirstOrDefaultAsync();

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }

}
