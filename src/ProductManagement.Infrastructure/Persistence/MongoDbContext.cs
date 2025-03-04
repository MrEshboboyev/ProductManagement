using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductManagement.Infrastructure.Persistence.Documents;

namespace ProductManagement.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<ProductDocument> Products =>
        _database.GetCollection<ProductDocument>("Products");
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}