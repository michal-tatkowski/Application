using MongoDB.Driver;

namespace Core.Application.Interfaces;

public interface IMongoDbContext<T>
{
    Task InsertAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task<ReplaceOneResult> UpdateAsync(T entity);
    Task<DeleteResult> DeleteAsync(string id);
}


