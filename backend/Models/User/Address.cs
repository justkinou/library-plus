using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryPlus.Models.User;

[BsonIgnoreExtraElements]
public class Address
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string? Country { get; set; } = null;
    public string? State { get; set; } = null;
    public string? City { get; set; } = null;
    public string? Street { get; set; } = null;
    public string? PostalCode { get; set; } = null;
    public string? BuildingNumber { get; set; } = null;
}
