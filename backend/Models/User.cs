using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models;

[BsonIgnoreExtraElements]
public class User
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public long DeliveryAddressId { get; set; }
    public DateTime JoinedAt { get; set; }
    public bool IsDeleted { get; set; }
}
