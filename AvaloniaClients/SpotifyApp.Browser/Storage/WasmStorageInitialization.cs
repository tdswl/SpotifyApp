using System.Threading;
using System.Threading.Tasks;
using SpotifyApp.Storage.Contracts.Interfaces;

namespace SpotifyApp.Browser.Storage;

internal sealed class WasmStorageInitialization : IStorageInitialization
{
    Task IStorageInitialization.InitializeStorageAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    void IStorageInitialization.InitializeStorage()
    {
    }
}