using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("refreshToken")]
    public string? RefreshToken { get; set; }
    [BsonElement("refreshTokenExpiryTime")]
    public DateTime? RefreshTokenExpiryTime { get; set; }
    [BsonElement("email")]
    public string Email { get; set; } = null!;
    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; } = null!;
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;
    [BsonElement("name")]
    public string Name { get; set; } = null!;
    [BsonElement("avatarUrl")]
    public string AvatarUrl { get; set; } = null!;
    [BsonElement("deliveryAddressId")]
    public long DeliveryAddressId { get; set; }
    [BsonElement("joinedAt")]
    public DateTime JoinedAt { get; set; }
    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; }
}
