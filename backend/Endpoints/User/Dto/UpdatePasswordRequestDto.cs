namespace LibraryPlus.Endpoints.User.Dto;

public record UpdatePasswordRequestDto(string OldPassword, string NewPassword);