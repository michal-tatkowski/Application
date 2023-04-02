using Core.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WebApi.Api.Services;

public class MongoDbContext<T> : IMongoDbContext<T>
{
    private readonly IMongoCollection<T> collection;

    public MongoDbContext(IMongoDatabase database, string collectionName)
    {
        collection = database.GetCollection<T>(collectionName);
    }

    public async Task InsertAsync(T entity)
    {
        await collection.InsertOneAsync(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await collection.Find(x => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ReplaceOneResult> UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        return await collection.ReplaceOneAsync(filter, entity);
    }

    public async Task<DeleteResult> DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        return await collection.DeleteOneAsync(filter);
    }
}