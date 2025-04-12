using System.Linq.Expressions;
using Beneficios.Domain.Repositories;
using Beneficios.Domain.Shareds;
using MongoDB.Driver;

namespace Beneficios.MongoDB.Repositories;

/// <summary>
/// Implementação genérica do repositório para operações com MongoDB.
/// </summary>
/// <typeparam name="TEntity">O tipo da entidade.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IEntity
{
    private readonly IMongoCollection<TEntity> _collection;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Repository{TEntity}"/>.
    /// </summary>
    /// <param name="database">Instância do banco de dados MongoDB.</param>
    public Repository(IMongoDatabase database)
    {
        // Define o nome da coleção dinamicamente com base no tipo da entidade
        var collectionName = typeof(TEntity).Name;
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertOnAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task ReplaceOneAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", GetEntityId(entity));
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteOnAsync(object id, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    private object GetEntityId(TEntity entity)
    {
        var property = entity.GetType().GetProperty("Id");
        if (property == null)
        {
            throw new InvalidOperationException("A entidade não possui uma propriedade 'Id'.");
        }
        return property.GetValue(entity) ?? throw new InvalidOperationException("O valor da propriedade 'Id' é nulo.");
    }

    public async Task<TEntity?> FindOneUpdateAsync(
        Expression<Func<TEntity, bool>> filter,
        Expression<Func<TEntity, TEntity>> update,
        CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<TEntity>.Update.Combine(
            update.Body.Type.GetProperties().Select(property =>
                Builders<TEntity>.Update.Set(property.Name, property.GetValue(update.Compile().Invoke(default!))))
        );

        var options = new FindOneAndUpdateOptions<TEntity, TEntity>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _collection.FindOneAndUpdateAsync(filter, updateDefinition, options, cancellationToken);
    }
}
