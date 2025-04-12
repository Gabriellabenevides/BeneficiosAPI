using MongoDB.Driver;

namespace Beneficios.MongoDB.MongoDB;

/// <summary>
/// Classe responsável por gerenciar a conexão com o MongoDB.
/// </summary>
public class MongoDbConnection
{
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="MongoDbConnection"/>.
    /// </summary>
    /// <param name="databaseName">Nome do banco de dados no MongoDB.</param>
    public MongoDbConnection(string databaseName)
    {
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    /// <summary>
    /// Obtém uma coleção do banco de dados.
    /// </summary>
    /// <typeparam name="TDocument">Tipo dos documentos na coleção.</typeparam>
    /// <param name="collectionName">Nome da coleção.</param>
    /// <returns>Uma instância de <see cref="IMongoCollection{TDocument}"/>.</returns>
    public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
    {
        return _database.GetCollection<TDocument>(collectionName);
    }
}