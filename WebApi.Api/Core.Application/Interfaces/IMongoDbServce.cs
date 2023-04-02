using MongoDB.Driver;

namespace Core.Application.Interfaces;

public interface IMongoDbService<T>
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T item);
    Task<bool> UpdateAsync(string id, T item);
    Task<bool> DeleteAsync(string id);
}

