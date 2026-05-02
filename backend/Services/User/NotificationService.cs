using LibraryPlus.Models.User;
using MongoDB.Driver;

namespace LibraryPlus.Services.User;

public class NotificationService(IMongoDatabase db)
{
    private readonly IMongoCollection<NotificationModel> _notifications = db.GetCollection<NotificationModel>("notifications");
    private readonly IMongoCollection<UserNotificationModel> _userNotifications = db.GetCollection<UserNotificationModel>("userNotifications");

    public async Task<NotificationModel> CreateNotification(string text)
    {
        var notification = new NotificationModel
        {
            Text = text
        };
        await _notifications.InsertOneAsync(notification);
        return notification;
    }

    public async Task SendOneUserNotification(string userId, string text)
    {
        var notification = await CreateNotification(text);
        await _userNotifications.InsertOneAsync(new UserNotificationModel
        {
            UserId = userId,
            NotificationId = notification.Id,
        });
    }

    public async Task SendAllUsersNotification(IEnumerable<string> userIds, string text)
    {
        var notification = await CreateNotification(text);
        var userNotifications = userIds.Select(id => new UserNotificationModel
        {
            UserId = id,
            NotificationId = notification.Id,
        });
        await _userNotifications.InsertManyAsync(userNotifications);
    }
    
}