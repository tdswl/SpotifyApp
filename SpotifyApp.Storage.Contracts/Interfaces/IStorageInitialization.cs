namespace SpotifyApp.Storage.Contracts.Interfaces;

public interface IStorageInitialization
{
    Task InitializeStorageAsync(CancellationToken cancellationToken);
    
    void InitializeStorage();
}