using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Core.Application;

public class MongoDbContext
{
    public static void CreateContext()
    {
        var client = new MongoClient("mongodb://localhost:27017", builder);
        var database = client.GetDatabase("myDatabase");
        var service = new MongoDbService<MyType>(database, "myCollection");
    }