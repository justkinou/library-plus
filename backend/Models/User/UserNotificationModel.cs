using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models.User;

[BsonIgnoreExtraElements]
public class UserNotificationModel
{
    public string UserId { get; set; } = null!;
    public string NotificationId { get; set; } = null!;
    public bool IsRead { get; set; } = false;
}