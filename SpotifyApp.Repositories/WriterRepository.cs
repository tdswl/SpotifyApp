using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Interfaces;

namespace SpotifyApp.Repositories;

internal class WriterRepository<T> : IWriteRepository<T> where T : class, IStorageEntity 
{
    private readonly IStorageWriter _writer;

    public WriterRepository(IStorageWriter writer)
    {
        _writer = writer;
    }
    
    void IWriteRepository<T>.Add(T entity)
    {
        _writer.Add(entity);
    }

    void IWriteRepository<T>.Update(T entity)
    {
        _writer.Update(entity);
    }

    void IWriteRepository<T>.Delete(T entity)
    {
        _writer.Delete(entity);
    }

    Task<int> IWriteRepository<T>.SaveChangesAsync(CancellationToken token)
    {
        return _writer.SaveChangesAsync(token);
    }
}