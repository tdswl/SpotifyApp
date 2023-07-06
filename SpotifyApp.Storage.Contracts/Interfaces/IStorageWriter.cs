namespace SpotifyApp.Storage.Contracts.Interfaces;

public interface IStorageWriter
{
    void Add<TEntity>(TEntity entity) where TEntity : class, IStorageEntity;

    void Update<TEntity>(TEntity entity) where TEntity : class, IStorageEntity;

    void Delete<TEntity>(TEntity entity) where TEntity : class, IStorageEntity;

    public Task<int> SaveChangesAsync(CancellationToken token);
}