using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Beneficios.Domain.Shareds;

public abstract class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    [JsonIgnore] // Ignora o campo durante a serialização para JSON
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
}
