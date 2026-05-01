using LibraryPlus.Models.User;

namespace LibraryPlus.Endpoints.User.Dto;

public record MeResponseShortDto(string Email, string? Name, string? AvatarUrl);