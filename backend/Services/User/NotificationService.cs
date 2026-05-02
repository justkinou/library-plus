using LibraryPlus.DTO;
using LibraryPlus.Models.User;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
            CreatedAt = DateTime.UtcNow,
        });
    }

    public async Task SendAllUsersNotification(IEnumerable<string> userIds, string text)
    {
        var notification = await CreateNotification(text);
        var userNotifications = userIds.Select(id => new UserNotificationModel
        {
            UserId = id,
            NotificationId = notification.Id,
            CreatedAt = DateTime.UtcNow,
        });
        await _userNotifications.InsertManyAsync(userNotifications);
    }

    public async Task<IList<UserNotificationDTO>> GetUserNotifications(string userId, int page)
    {
        Console.WriteLine($"userId = {userId}");
        var a = _userNotifications.AsQueryable()
            .Where(n => n.UserId == userId);
        Console.WriteLine($"len(a) = {a.Count()}");
        var b = a.OrderByDescending(n => n.CreatedAt);
        Console.WriteLine($"len(b) = {b.Count()}");
        var c = b.Skip(4 * (page - 1));
        Console.WriteLine($"len(c) = {c.Count()}");
        var d = c.Take(4);
        Console.WriteLine($"len(d) = {d.Count()}");
        foreach (var n in d.ToList())
        {
            Console.WriteLine($"{n.Id} {n.UserId} {n.NotificationId}");
        }
        foreach (var n in _notifications.AsQueryable().ToList())
        {
            Console.WriteLine($"notification: {n.Id}");
        }
        var e = d.Join(
                _notifications.AsQueryable(),
                un => un.NotificationId,
                n => n.Id,
                (un, n) => new UserNotificationDTO(un.Id, n.Text, un.IsRead)
            );
        Console.WriteLine($"len(e) = {e.Count()}");
        
        return await _userNotifications.AsQueryable()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Skip(4 * (page - 1))
            .Take(4)
            .Join(
                _notifications.AsQueryable(),
                un => un.NotificationId,
                n => n.Id,
                (un, n) => new UserNotificationDTO(un.Id, n.Text, un.IsRead)
            )
            .ToListAsync();
    }
    
}