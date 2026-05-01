using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models.User;

[BsonIgnoreExtraElements]
public class RefreshToken
{
    public string RefreshTokenHash { get; set; } = null!;
    public string UserId { get; set; } = null!;
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime ExpiryDate { get; set; }
}