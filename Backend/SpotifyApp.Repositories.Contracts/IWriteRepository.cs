using SpotifyApp.Storage.Contracts.Interfaces;

namespace SpotifyApp.Repositories.Contracts;

public interface IWriteRepository<in T> where T : IStorageEntity
{
    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);
    
    public Task<int> SaveChangesAsync(CancellationToken token);
}