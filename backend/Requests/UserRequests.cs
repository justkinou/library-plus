namespace LibraryPlus.UserRequests;

public record SignupRequest(
    string Email,
    string Password,
    string Name,
    string PhoneNumber,
    string AvatarURL);

public record LoginRequest(
string Email,
string Password);