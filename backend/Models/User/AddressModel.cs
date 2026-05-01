using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models.User;

[BsonIgnoreExtraElements]
public class AddressModel
{
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string? Country { get; set; } = null;
    public string? State { get; set; } = null;
    public string? City { get; set; } = null;
    public string? Street { get; set; } = null;
    public string? PostalCode { get; set; } = null;
    public string? BuildingNumber { get; set; } = null;
}
