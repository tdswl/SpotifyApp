using System.Collections.Concurrent;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SpotifyApp.Repositories.Contracts;
using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Browser.Repositories;

internal sealed class OAuthTokenLocalStorageWriteRepository : IOAuthTokenWriteRepository
{
    private static readonly ConcurrentBag<OAuthToken> ToAdd = [];
    private static readonly ConcurrentBag<OAuthToken> ToUpdate = [];
    private static readonly ConcurrentBag<OAuthToken> ToDelete = [];

    public void Add(OAuthToken entity)
    {
        ToAdd.Add(entity);
    }

    public void Update(OAuthToken entity)
    {
        ToUpdate.Add(entity);
    }

    public void Delete(OAuthToken entity)
    {
        ToDelete.Add(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        try
        {
            using (await JSHost.ImportAsync("LocalStorageScript", "../localStorage.js", token))
            {
                foreach (var item in ToAdd)
                {
                    Interop.LocalStorageInterop.Add($"{nameof(OAuthToken)}-{item.Id}", JsonSerializer.Serialize(item));
                }
                
                ToAdd.Clear();
                
                foreach (var item in ToUpdate)
                {
                    Interop.LocalStorageInterop.Add($"{nameof(OAuthToken)}-{item.Id}", JsonSerializer.Serialize(item));
                }
                    
                ToAdd.Clear();
                
                foreach (var item in ToDelete)
                {
                    Interop.LocalStorageInterop.Remove($"{nameof(OAuthToken)}-{item.Id}");
                }
                
                ToAdd.Clear();
                
                return 1;
            }
        }
        catch
        {
            return 0;
        }
    }
}