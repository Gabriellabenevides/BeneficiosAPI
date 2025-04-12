using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Beneficios.Domain.Shareds;

public abstract class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public ObjectId Id { get; protected set; } 
}
