using System.Linq.Expressions;
using Beneficios.Domain.Repositories;
using MongoDB.Driver;

namespace Beneficios.MongoDB.Repositories;

/// <summary>
/// Implementação genérica do repositório para operações com MongoDB.
/// </summary>
/// <typeparam name="Entity">O tipo da entidade.</typeparam>
public class Repository<Entity> : IRepository<Entity> where Entity : class
{
    private readonly IMongoCollection<Entity> _collection;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Repository{Entity}"/>.
    /// </summary>
    /// <param name="database">Instância do banco de dados MongoDB.</param>
    /// <param name="collectionName">Nome da coleção.</param>
    public Repository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<Entity>(collectionName);
    }

    public async Task<IEnumerable<Entity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<Entity?> FindByIdAsync(object id, CancellationToken cancellationToken)
    {
        var filter = Builders<Entity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Entity?> SingleOrDefaultAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertOnAsync(Entity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task ReplaceOneAsync(Entity entity, CancellationToken cancellationToken)
    {
        var filter = Builders<Entity>.Filter.Eq("_id", GetEntityId(entity));
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteOnAsync(object id, CancellationToken cancellationToken)
    {
        var filter = Builders<Entity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<Entity?> FindOneUpdateAsync(Expression<Func<Entity, bool>> filter, Expression<Func<Entity, Entity>> update, CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<Entity>.Update.Combine(update.Body as UpdateDefinition<Entity>);
        return await _collection.FindOneAndUpdateAsync(filter, updateDefinition, cancellationToken: cancellationToken);
    }

    private object GetEntityId(Entity entity)
    {
        var property = entity.GetType().GetProperty("Id");
        if (property == null)
        {
            throw new InvalidOperationException("A entidade não possui uma propriedade 'Id'.");
        }
        return property.GetValue(entity) ?? throw new InvalidOperationException("O valor da propriedade 'Id' é nulo.");
    }
}

