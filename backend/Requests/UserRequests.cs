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

public record RefreshRequest(string RefreshToken);

public record TokenResponse(string AccessToken, string RefreshToken);
public record AccessTokenResponse(string AccessToken);
public record LogoutRequest(string RefreshToken);