using LibraryPlus.Requests;
using LibraryPlus.Filters;
using LibraryPlus.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryPlus.Endpoints.User;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("/api/v1/notification")
            .AddEndpointFilter<ActiveUserFilter>()
            .AddEndpointFilter<AdminUserFilter>();

        group.MapPost("/sendOne", [Authorize] async (
            NotificationService notificationService,
            [FromBody] SendOneNotificationRequest sendOneNotificationRequest
        ) =>
        {
            await notificationService.SendOneUserNotification(sendOneNotificationRequest.UserId, sendOneNotificationRequest.Text);
        });

        group.MapPost("/sendAll", [Authorize] async (
            UserService userService,
            [FromBody] SendAllNotificationRequest sendOneNotificationRequest
        ) =>
        {
            await userService.SendAllUsersNotification(sendOneNotificationRequest.Text);
        });

    }

}