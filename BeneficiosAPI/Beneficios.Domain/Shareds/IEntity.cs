using MongoDB.Bson;

namespace Beneficios.Domain.Shareds;

public interface IEntity
{
    ObjectId Id { get; } 
}
