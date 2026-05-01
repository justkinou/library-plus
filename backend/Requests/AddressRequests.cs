namespace LibraryPlus.Requests;

public record UpdateAddressRequest(
    string? Country,
    string? State,
    string? City,
    string? Street,
    string? PostalCode,
    string? BuildingNumber
);