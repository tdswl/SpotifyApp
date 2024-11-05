namespace SpotifyApp.Storage.Contracts.Interfaces;

public interface IStorageReader
{
    IQueryable<TEntity> Read<TEntity>() where TEntity : class, IStorageEntity;
}