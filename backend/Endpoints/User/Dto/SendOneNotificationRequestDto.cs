namespace LibraryPlus.Endpoints.User.Dto;

public record SendOneNotificationRequestDto(
    string UserId,
    string Text
);