using LibraryPlus.Models.User;

namespace LibraryPlus.Endpoints.User.Dto;

public record AddressDto(
    string? Country,
    string? State,
    string? City,
    string? Street,
    string? PostalCode,
    string? BuildingNumber
)
{
    public static AddressDto FromModel(AddressModel address)
    {
        return new AddressDto(
            address.Country,
            address.State,
            address.City,
            address.Street,
            address.PostalCode,
            address.BuildingNumber
        );
    }
};

public record MeResponseDto(
    string Email,
    string? PhoneNumber,
    string? AvatarUrl,
    AddressDto Address,
    DateTime JoinedAt
)
{
    public static MeResponseDto FromModel(UserModel user)
    {
        return new MeResponseDto(
            user.Email,
            user.PhoneNumber,
            user.AvatarUrl,
            AddressDto.FromModel(user.DeliveryAddress),
            user.JoinedAt
        );
    }
};