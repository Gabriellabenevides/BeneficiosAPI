using System.Linq.Expressions;

namespace Beneficios.Domain.Repositories;

/// <summary>
/// Interface genérica para repositórios, fornecendo métodos para operações CRUD.
/// </summary>
/// <typeparam name="Entity">O tipo da entidade.</typeparam>
public interface IRepository<Entity> where Entity : class
{
    /// <summary>
    /// Obtém todas as entidades.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma lista de entidades.</returns>
    Task<IEnumerable<Entity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Encontra entidades com base em um filtro.
    /// </summary>
    /// <param name="filter">Expressão de filtro.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma lista de entidades que correspondem ao filtro.</returns>
    Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken);

    /// <summary>
    /// Encontra uma entidade pelo ID.
    /// </summary>
    /// <param name="id">O ID da entidade.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A entidade encontrada ou null.</returns>
    Task<Entity?> FindByIdAsync(object id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtém uma única entidade que corresponde ao filtro ou null.
    /// </summary>
    /// <param name="filter">Expressão de filtro.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A entidade encontrada ou null.</returns>
    Task<Entity?> SingleOrDefaultAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken);

    /// <summary>
    /// Obtém a primeira entidade que corresponde ao filtro ou null.
    /// </summary>
    /// <param name="filter">Expressão de filtro.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A primeira entidade encontrada ou null.</returns>
    Task<Entity?> FirstOrDefaultAsync(Expression<Func<Entity, bool>> filter, CancellationToken cancellationToken);

    /// <summary>
    /// Insere uma nova entidade.
    /// </summary>
    /// <param name="entity">A entidade a ser inserida.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma tarefa representando a operação.</returns>
    Task InsertOnAsync(Entity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Substitui uma entidade existente.
    /// </summary>
    /// <param name="entity">A entidade a ser substituída.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma tarefa representando a operação.</returns>
    Task ReplaceOneAsync(Entity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Exclui uma entidade.
    /// </summary>
    /// <param name="id">O ID da entidade a ser excluída.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Uma tarefa representando a operação.</returns>
    Task DeleteOnAsync(object id, CancellationToken cancellationToken);

    /// <summary>
    /// Encontra e atualiza uma entidade.
    /// </summary>
    /// <param name="filter">Expressão de filtro para encontrar a entidade.</param>
    /// <param name="update">Expressão de atualização.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>A entidade atualizada ou null.</returns>
    Task<Entity?> FindOneUpdateAsync(Expression<Func<Entity, bool>> filter, Expression<Func<Entity, Entity>> update, CancellationToken cancellationToken);
}
