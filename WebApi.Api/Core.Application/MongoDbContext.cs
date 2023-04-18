using Core.Domain.Models;
using MongoDB.Driver;

namespace Core.Application;

public class MongoDbContext
{
    private readonly IMongoDatabase database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        var client = new MongoClient(settings);
        database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<User> Users => database.GetCollection<User>("users");
}