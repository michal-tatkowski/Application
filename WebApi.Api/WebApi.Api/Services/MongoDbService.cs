using Core.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WebApi.Api.Services;

public class MongoDbService<T> : IMongoDbService<T> where T : class
{
    private readonly IMongoCollection<T> collection;
    
    public MongoDbService(IMongoDatabase database, string collectionName)
    {
        collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(T item)
    {
        await collection.InsertOneAsync(item);
    }

    public async Task<bool> UpdateAsync(string id, T item)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await collection.ReplaceOneAsync(filter, item);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await collection.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}